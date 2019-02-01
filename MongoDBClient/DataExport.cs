using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MongoDB.Bson.IO;
using MongoDB.Driver;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;

namespace ApexMongoMonitor
{
    public partial class DataExport : Form
    {
        private readonly object _dataToBeExported = null;
        private readonly bool _dataForApex = false;

        readonly Regex _rxRemoveObjId = new Regex(@"""_id""\s*:\s*ObjectId\(\"".{24}\""\),");

        private void init()
        {
            cboExportType.DataSource = new string[] { "JSON"};//, "XML" };
            //saveFileDialog1.Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog1.Filter = "JSON Files (*.json)|*.json";

            cboExportType.SelectedIndex = 0;
        }


        //public DataExport(BsonDocument document, bool dataForApex)
        public DataExport(BsonValue document, bool dataForApex)
        {
            InitializeComponent();

            _dataToBeExported = document;
            _dataForApex = dataForApex;
            init();
        }

        public DataExport(MongoCollection collection, bool dataForApex)
        {
            InitializeComponent();

            _dataToBeExported = collection;
            _dataForApex = dataForApex;
            init();
        }


        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            ShowWfTrackingFolderSelectDialog();
        }


        /// <summary>
        /// Displays Tracking folder path selection dialog
        /// </summary>
        private void ShowWfTrackingFolderSelectDialog()
        {
            //WFTrackingFolderBrowserDialog.Description = "Select WF tracking path\n" +
            //"Monitor Path: " + _monitorSettingsValue.WFTrackingFolderPath;

            //if (!string.IsNullOrEmpty(_monitorSettingsValue.WFTrackingFolderPath))
            //    WFTrackingFolderBrowserDialog.SelectedPath = _monitorSettingsValue.WFTrackingFolderPath;

            DialogResult result = saveFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                lblExportPath.Text = saveFileDialog1.FileName.Trim();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lblExportPath.Text.Length < 1)
                return;

            if (_dataToBeExported is MongoCollection)
            {
                var collection = (MongoCollection)_dataToBeExported;

                var bsonDocumentBindable = collection.FindAllAs<BsonDocument>();
                ExportData(bsonDocumentBindable);
                //File.WriteAllText(lblExportPath.Text, bsonDocumentBindable.ToJson());
                //MessageBox.Show("Data export has been finished.");
            }
            else if (_dataToBeExported is BsonDocument)
            {
                ExportData(_dataToBeExported);
                //File.WriteAllText(lblExportPath.Text, _dataToBeExported.ToJson());
                //MessageBox.Show("Data export has been finished.");
            }
        }

        private bool ExportData(object dataToBeExported)
        {
            try
            {
                if (cboExportType.SelectedValue.Equals("JSON"))
                {
                    string jsonStr = dataToBeExported.ToJson();

                    if(_dataForApex)
                        jsonStr = _rxRemoveObjId.Replace(dataToBeExported.ToJson(), string.Empty);

                    //File.WriteAllText(lblExportPath.Text, dataToBeExported.ToJson());
                    //var setting = new JsonWriterSettings();
                    //setting.Indent = true;
                    //setting.OutputMode = JsonOutputMode.Strict;
                    //File.WriteAllText(lblExportPath.Text, dataToBeExported.ToJson(setting));
                    File.WriteAllText(lblExportPath.Text, jsonStr);//dataToBeExported.ToJson());
                }
                else if (cboExportType.SelectedValue.Equals("XML"))
                {
                    File.WriteAllText(lblExportPath.Text, JsonConvert.DeserializeXNode(dataToBeExported.ToJson()).ToString());
                }
                MessageBox.Show("Data export has been finished.");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
