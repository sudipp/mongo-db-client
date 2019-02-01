using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MongoDB.Driver;

namespace ApexMongoMonitor
{
    internal enum CRUDOpType
    {
        NoOp,
        Add,
        Edit
    }

    public partial class SelectConnection : Form
    {
        private readonly Regex _rx = new Regex(@"([^/:]+)");
        private readonly ApplicationSettings _appSettingvalue;
        private CRUDOpType _crudOp=CRUDOpType.NoOp;
        private bool _showCloseWin = true;
        private int _selectedConnection = -1;

        
        public SelectConnection(ApplicationSettings appSettingvalue)
        {
            InitializeComponent();

            //Initializing Settings
            _appSettingvalue = appSettingvalue;


            LoadAvailableConnections();
        }
        
        private void btnAddConn_Click(object sender, EventArgs e)
        {
            _crudOp = CRUDOpType.Add;
            ClearFileds();
            
            tabControl1.TabPages[1].Selected = true;
        }

        private void SelectConnection_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Selected = true;
        }

        private void btnEditConn_Click(object sender, EventArgs e)
        {
            if (lstViewAvailableConnections.SelectedIndices.Count < 1)
                return;
            
            //Set Value
            if(_rx.IsMatch(lstViewAvailableConnections.SelectedItems[0].Text))
            {
                MatchCollection mc = _rx.Matches(lstViewAvailableConnections.SelectedItems[0].Text);
                txtHost.Text = mc[1].Value;
                txtPort.Text = mc[2].Value;
                txtDBname.Text = mc[3].Value;
            }
            
            _crudOp = CRUDOpType.Edit;
            _selectedConnection = lstViewAvailableConnections.SelectedIndices[0];

            tabControl1.TabPages[1].Selected = true;
        }

        private void tabControl1_SelectionChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab==tabControl1.TabPages[1])
            {
                if (_crudOp == CRUDOpType.NoOp)
                    btnSave.Enabled = false;
                else
                    btnSave.Enabled = true;

                //disable below buttons
                EnableDisableConnCRUDButtons(false);
            }
            else
            {
                //enable below buttons
                EnableDisableConnCRUDButtons(true);
            }
        }

        void EnableDisableConnCRUDButtons(bool enable)
        {
            btnAddConn.Enabled = enable;
            btnEditConn.Enabled = enable;
            btnDelConn.Enabled = enable;
            btnConn.Enabled = enable;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_crudOp == CRUDOpType.NoOp)
                return;

            if(ValidateConnectionSettings())
            {
                if (_crudOp == CRUDOpType.Add)
                {
                    _appSettingvalue.AvailableConnections.Add(string.Format("mongodb://{0}:{1}/{2}", txtHost.Text.Trim(),
                                                                            txtPort.Text.Trim(), txtDBname.Text.Trim()));
                }
                else
                {
                    _appSettingvalue.AvailableConnections.RemoveAt(_selectedConnection);
                    _appSettingvalue.AvailableConnections.Insert(_selectedConnection,
                                                                 string.Format("mongodb://{0}:{1}/{2}",
                                                                               txtHost.Text.Trim(), txtPort.Text.Trim(),
                                                                               txtDBname.Text.Trim()));
                }

                _appSettingvalue.SaveSettings(Application.LocalUserAppDataPath + @"\apexMongoMonitor.config");

                _crudOp = CRUDOpType.NoOp;
                LoadAvailableConnections();
                tabControl1.TabPages[0].Selected = true;
            }
        }

        private void LoadAvailableConnections()
        {
            lstViewAvailableConnections.Items.Clear();
            foreach (var conn in (_appSettingvalue.AvailableConnections))
            {
                lstViewAvailableConnections.Items.Add(
                        new ListViewItem(new string[] { conn })
                );
            }
        }

        private void ClearFileds()
        {
            txtHost.Text = "127.0.0.1";
            txtPort.Text = "27017";
            txtDBname.Text = "admin";
            txtDBname.Items.Clear();
        }

        private bool ValidateConnectionSettings()
        {
            bool status = false;

            if (txtHost.Text.Trim().Length < 1)
            {
                txtHost.Focus();
                MessageBox.Show("Please add atleast one host address to connect", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return status;
            }

            int port;
            if (txtPort.Text.Trim().Length < 1 || !Int32.TryParse(txtPort.Text.Trim(), out port))
            {
                txtPort.Text = string.Empty;
                txtPort.Focus();
                MessageBox.Show("Please add port to connect","Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return status;
            }
            if (txtDBname.Text.Trim().Length < 1)
            {
                txtDBname.Focus();
                MessageBox.Show("Please add database to connect", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return status;
            }
            
            return true;
        }

        private void btnDelConn_Click(object sender, EventArgs e)
        {
            if(lstViewAvailableConnections.SelectedIndices.Count<1)
                return;

            _appSettingvalue.AvailableConnections.RemoveAt(lstViewAvailableConnections.SelectedIndices[0]);
            _appSettingvalue.SaveSettings(Application.LocalUserAppDataPath + @"\apexMongoMonitor.config");

            LoadAvailableConnections();
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            if (lstViewAvailableConnections.SelectedIndices.Count < 1)
            {
                _showCloseWin = false;
                return;
            }
            _showCloseWin = true;

            ((MainForm)this.Owner).SelectedConnectionString =
                lstViewAvailableConnections.Items[lstViewAvailableConnections.SelectedIndices[0]].Text;
        }

        private void SelectConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_showCloseWin)
                e.Cancel = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _showCloseWin = true;
        }
       
        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            int port;
            if (txtHost.Text.Trim().Length < 1 || txtPort.Text.Trim().Length < 1 || !Int32.TryParse(txtPort.Text.Trim(), out port))
            {
                return;
            }

            try
            {
                MongoClient client = new MongoClient(string.Format("mongodb://{0}:{1}", txtHost.Text.Trim(),
                                                                   txtPort.Text.Trim()));
                IEnumerable<string> dbNames = client.GetServer().GetDatabaseNames();
                txtDBname.Items.Clear();
                foreach (var dbname in dbNames)
                {
                    txtDBname.Items.Add(dbname);
                }

                if (txtDBname.Items.Count > 0)
                    txtDBname.Text = txtDBname.Items[0].ToString();
            }
            catch
            {}
        }

        private void lstViewAvailableConnections_DoubleClick(object sender, EventArgs e)
        {
            btnConn.PerformClick();
        }
    }
}
