using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MongoDB.Driver;
using System.Reflection;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;
using WeifenLuo.WinFormsUI.Docking;

namespace ApexMongoMonitor
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// application settings
        /// </summary>
        private readonly ApplicationSettings _appSettingvalue;
        private MongoServer _selectedServer;

        public MongoDatabase SelectedDB { set; get; }
        public string SelectedCollection { set; get; }
        public string SelectedConnectionString { set; get; }

        public MainForm()
        {
            InitializeComponent();

            //Initializing Settings
            _appSettingvalue = new ApplicationSettings();
            _appSettingvalue.LoadAppSettings(Application.LocalUserAppDataPath + @"\apexMongoMonitor.config");


            
            DockContent dcMainDoc = new DockContent();
            tabControl1.Dock = DockStyle.Fill;
            dcMainDoc.Controls.Add(tabControl1);
            dcMainDoc.Show(dockPanelMainForm, DockState.Document);

            DockContent dpServerExp = new DockContent();
            dpServerExp.Text = "Server Explorer";
            dpServerExp.TabText = "Server Explorer";
            dpServerExp.CloseButtonVisible = false;
            treeViewDBExplorer.Dock = DockStyle.Fill;
            panelDBExplorer.Dock = DockStyle.Fill;
            dpServerExp.Controls.Add(panelDBExplorer);
            dpServerExp.Show(dockPanelMainForm, DockState.DockLeft);
            
//************************
            
            DockContent dc = new DockContent();
            treeListViewData.Dock = DockStyle.Fill;
            dc.Controls.Add(treeListViewData);
            dc.Show(dockPanelDataTab, DockState.Document);

            DockContent prop = new DockContent();
            prop.Text = "Text Visualizer";
            prop.TabText = "Text Visualizer";
            prop.CloseButtonVisible = false;
            txtFieldProp.Dock = DockStyle.Fill;
            panelDataVisualizer.Dock = DockStyle.Fill;
            prop.Controls.Add(panelDataVisualizer);
            prop.Show(dockPanelDataTab, DockState.DockRight);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Selected = true;
            Utils.PrepareFormTitle(this, false);

            //tsBtnConnect.Enabled = true;
            //tsBtnDisConnect.Enabled = false;

            //Getting tracking file path
            GetDBConnectionSetting();


            // if there is no path selected by user, then terminate the app
            //if (string.IsNullOrEmpty(_appSettingvalue.WFTrackingFolderPath))
            //{
            //    Application.Exit();
            //    return;
            //}
        }

        private void tsBtnConnect_Click(object sender, EventArgs e)
        {
            ShowSettingsDialog();
        }

        private void tsBtnDisConnect_Click(object sender, EventArgs e)
        {
            ConnectDisconnectDB(false);
        }


        /// <summary> 
        /// 
        /// </summary>
        private void GetDBConnectionSetting()
        {
            ShowSettingsDialog();
        }

        private void ShowSettingsDialog()
        {
            SelectConnection selConnform = new SelectConnection(_appSettingvalue);
            DialogResult result = selConnform.ShowDialog(this);

            //Connect to DB,
            if (result == DialogResult.OK)
            {
                ConnectDisconnectDB(true);
            }
        }

        private void tabControl1_ClosePressed(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab!=null)
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void ConnectDisconnectDB(bool connect)
        {
            try
            {
                if (connect)
                {
                    //MongoServer server = MongoServer.Create(SelectedConnectionString);
                    //server.Connect();
                    MongoUrl mongoUrl = new MongoUrl(SelectedConnectionString);

                    _selectedServer = MongoServer.Create(mongoUrl);
                    _selectedServer.Connect();

                    //MongoClient client = new MongoClient(mongoUrl);
                    //_server = client.GetServer();

                    SelectedDB = _selectedServer.GetDatabase(mongoUrl.DatabaseName);

                    //Load server object details in the UI
                    //*****************************************************************
                    treeViewDBExplorer.Nodes.Clear();
                    TreeNode tnServer = new TreeNode(_selectedServer.Settings.Server.ToString(), 5, 5);
                    treeViewDBExplorer.Nodes.Add(tnServer);


                    //Load Server objects
                    GetServerObjects(tnServer);

                    //DatabaseStatsResult dbstats= SelectedDB.GetStats();
                    //if (dbstats != null)
                    //{
                    //    BsonDocument response = dbstats.Response;
                    //    foreach (var element in response.Elements)
                    //    {
                    //        lstServerStatus.Items.Add(
                    //            new ListViewItem(new string[] { element.Name, element.Value.ToJson() })
                    //        );

                    //        if (element.Name.Equals("host"))
                    //            lblHost.Text = element.Value.ToJson();
                    //        if (element.Name.Equals("version"))
                    //            lblVersion.Text = element.Value.ToJson();
                    //    }
                    //}

                    //load Server Status 
                    //*****************************************************************
                    CommandResult result = SelectedDB.RunCommand("serverStatus");
                    if (result != null)
                    {
                        BsonDocument response = result.Response;
                        foreach (var element in response.Elements)
                        {
                            lstServerStatus.Items.Add(
                                new ListViewItem(new string[] { element.Name, element.Value.ToString() })//.ToJson() })
                            );

                            if (element.Name.Equals("host"))
                                lblHost.Text = element.Value.ToString();//.ToJson();
                            if (element.Name.Equals("version"))
                                lblVersion.Text = element.Value.ToString();//.ToJson();
                        }
                    }

                    //load Server Settings
                    //*****************************************************************
                    BsonDocument bsonSettings = _selectedServer.Settings.ToBsonDocument();
                    foreach (var element in bsonSettings.Elements)
                    {
                        lstServerSettings.Items.Add(
                            new ListViewItem(new string[] { element.Name, element.Value.ToString()})//.ToJson() })
                        );
                    }

                    tabControl1.TabPages[1].Selected = true;
                }
                else
                {
                    _selectedServer.Disconnect();
                    _selectedServer = null;

                    SelectedDB = null;
                    //Clear DB object explorer
                    treeViewDBExplorer.Nodes[0].Nodes.Clear();

                    //Clear all childs belongs to server node
                    lstDB.Items.Clear();

                    //Clear all childs belongs to collection node
                    lstCollection.Items.Clear();

                    //Clear all childs belongs to treeListViewData
                    treeListViewData.ClearObjects();

                    lstServerStatus.Items.Clear();
                    lstServerSettings.Items.Clear();

                    lblHost.Text = string.Empty;
                    lblVersion.Text = string.Empty;

                    txtFieldProp.Text = string.Empty;

                    treeListViewData.Roots = null;

                    //Hide all tabs
                    //disble all toolbar and menu
                }

                Utils.PrepareFormTitle(this, connect);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }


        private void GetServerObjects(TreeNode tnServer)
        {
            try
            {
                IEnumerable<string> dbNames=_selectedServer.GetDatabaseNames();

                //Clear all childs belongs to server node
                tnServer.Nodes.Clear();

                //Clear all db details
                lstDB.Items.Clear();

                //Clear all collection details
                lstCollection.Items.Clear();

                //treeViewDBExplorer.Nodes.Clear();
                //MongoUrl mongoUrl = new MongoUrl(SelectedConnectionString);
                //TreeNode tnServer = new TreeNode(_selectedServer.Settings.Server.ToString(), 5, 5);

                foreach (var db in dbNames)
                {
                    TreeNode tnDB = tnServer.Nodes.Add(db, db, 2, 2);
                    //tnDB.Tag = false;

                    MongoDatabase bDb = _selectedServer.GetDatabase(db);
                    //CommandResult result = bDb.RunCommand("stat");
                    lstDB.Items.Add(
                        new ListViewItem(new string[] {db, (bDb.GetStats().StorageSize.ToString())})
                        );

                    if (db.Equals(SelectedDB.Name))
                    {
                        treeViewDBExplorer.SelectedNode = tnDB;
                        tnDB.Tag = "true";

                        lstDB.FindItemWithText(db).Selected = true;

                        TreeNode tnCollectionLabel = tnDB.Nodes.Add("Collections", "Collections", 6, 6);

                        foreach (var coll in SelectedDB.GetCollectionNames())
                        {
                            //TreeNode tnCollection = tnDB.Nodes.Add(coll, coll, 3, 3);
                            TreeNode tnCollection = tnCollectionLabel.Nodes.Add(coll, coll, 3, 3);

                            TreeNode tnIndexLabel = tnCollection.Nodes.Add("Indexes", "Indexes", 6, 6);

                            //_selectedServer.GetDatabase(db).GetCollection(coll).GetTotalStorageSize()

                            var collection = _selectedServer.GetDatabase(db).GetCollection(coll);

                            lstCollection.Items.Add(
                                new ListViewItem(new string[]
                                                     {
                                                         collection.Name, collection.Count().ToString(),
                                                         (collection.GetStats().StorageSize/1024).ToString()
                                                     })
                                );
                            lstCollection.Items[0].Selected = true;

                            foreach (var index in collection.GetIndexes())
                            {
                                TreeNode tnIndex = tnIndexLabel.Nodes.Add(index.Name, index.Name, 7, 7);
                            }
                        }
                    }
                }

                tnServer.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void treeViewDBExplorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (_selectedServer == null)
                return;

            if (e.Node.Level == 1)
            {
                //Update the selected db name
                SelectedDB = _selectedServer.GetDatabase(e.Node.Text);

                if (e.Node.Tag == null) //Expanding db details
                {
                    GetServerObjects(treeViewDBExplorer.Nodes[0]);

                    Utils.PrepareFormTitle(this, true);
                }
            }
        }


        private void lstCollection_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;

            try
            {
                this.SelectedCollection = lstCollection.SelectedItems[0].Text;

                this.treeListViewData.CanExpandGetter = delegate(object x)
                                                            {
                                                                /*
                    var doc = (BsonDocumentBindable)x;
                    if (doc.BsonElements == null)
                        return false;
                    return doc.BsonElements.Count() > 0;
                    */

                                                                /*****old logic -- only bson document
                                                                 * var doc = (BsonDocumentBindable)x;
                                                                if (doc.BsonParent == null)
                                                                    return false;
                                                                if (!doc.BsonParent.IsBsonDocument)
                                                                    return false;

                                                                BsonDocument docVal = (BsonDocument)doc.BsonParent;//.ToBsonDocument();
                                                                return docVal.Elements.Count() > 0;
                                                                */
                                                                var doc = (BsonDocumentBindable)x;
                                                                if ((doc.BsonParent != null) && (doc.BsonParent.IsBsonDocument || doc.BsonParent.IsBsonArray))
                                                                {
                                                                    if (doc.BsonParent.IsBsonDocument)
                                                                    {
                                                                        return ((BsonDocument)doc.BsonParent).Elements.Count() > 0;
                                                                    }
                                                                    if (doc.BsonParent.IsBsonArray)
                                                                    {
                                                                        return ((BsonArray)doc.BsonParent).Count > 0;
                                                                    }
                                                                }
                                                                return false;

                                                                /*
                                                                var doc = (BsonDocumentBindable)x;
                                                                if (doc.BsonParent == null)
                                                                    return false;

                                                                if (doc.BsonParent.IsBsonDocument)
                                                                {
                                                                    BsonDocument docVal = (BsonDocument)doc.BsonParent;
                                                                    return docVal.Elements.Count() > 0;
                                                                }
                                                                else if (doc.BsonParent.IsBsonArray)
                                                                {
                                                                    BsonArray docVal = (BsonArray)doc.BsonParent;
                                                                    return docVal.Count > 0;
                                                                }
                                                                else
                                                                {
                                                                    return false;
                                                                }*/
                                                            };
                this.treeListViewData.ChildrenGetter = delegate(object x)
                                                           {
                                                               return
                                                                   Utils.GetBindableDocsFromElements(
                                                                       (BsonDocumentBindable) x);
                                                           };

                this.treeListViewData.Roots = Utils.GetBindableDocsFromCollection(SelectedDB,
                                                                                  lstCollection.SelectedItems[0].Text);

                tabControl1.TabPages[2].Text = "Data (" + lstCollection.SelectedItems[0].Text + ")";
                
                //Showing collection property
                MongoCollection collection = SelectedDB.GetCollection(lstCollection.SelectedItems[0].Text);
                propCollection.SelectedObject = Utils.GetCollectionProperty(collection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstCollection_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tabControl1.TabPages[2].Selected = true;
        }

        private void toolStripRefreshData_Click(object sender, EventArgs e)
        {
            if (lstCollection.SelectedItems.Count > 0)
            {
                lstCollection_ItemSelectionChanged(lstCollection,
                                                   new ListViewItemSelectionChangedEventArgs(
                                                       lstCollection.SelectedItems[0], 0, true));
            }
        }

        private void toolStripDataExport_Click(object sender, EventArgs e)
        {
            if (lstCollection.SelectedItems.Count < 1)
                return;

            var frmdataExp = new DataExport(SelectedDB.GetCollection(lstCollection.SelectedItems[0].Text),false);
            frmdataExp.ShowDialog(this);
        }

        private void exportDataMenu_Click(object sender, EventArgs e)
        {
            toolStripDataExport_Click(sender, e);
        }


        private void exportDataMenuData_Click(object sender, EventArgs e)
        {
            var model = Utils.GetRootBindableDoc(treeListViewData);
            //var frmdataExp = new DataExport(model.BsonDoc,false);
            var frmdataExp = new DataExport(model.BsonParent, false);
            frmdataExp.ShowDialog(this);
        }

        private void tsBtnRefreshDB_Click(object sender, EventArgs e)
        {
            ConnectDisconnectDB(true);
        }

        private void exportDataMenuAPEXData_Click(object sender, EventArgs e)
        {
            var model = Utils.GetRootBindableDoc(treeListViewData);
            //var frmdataExp = new DataExport(model.BsonDoc,true);
            var frmdataExp = new DataExport(model.BsonParent, true);
            frmdataExp.ShowDialog(this);
        }

        //private void LoadCollections()
        //{
        //    lstCollection.Items.Clear();
        //    foreach (var coll in SelectedDB.GetCollectionNames())
        //    {
        //        var collection = SelectedDB.GetCollection(coll);

        //        lstCollection.Items.Add(
        //            new ListViewItem(new string[]
        //                                             {
        //                                                 collection.Name, collection.Count().ToString(),
        //                                                 (collection.GetStats().StorageSize/1024).ToString()
        //                                             })
        //            );
        //        lstCollection.Items[0].Selected = true;
        //    }
        //}

        private void databaseManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void treeListViewData_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;

            var doc=(BsonDocumentBindable) treeListViewData.SelectedItem.RowObject;
            txtFieldProp.Text = doc.BsonParent.ToString();//.BsonDoc.ToJson();
        }

        private void tsSaveFieldProp_Click(object sender, EventArgs e)
        {
            var sfdialog=new SaveFileDialog
                                        {
                                            Filter = "Text Files (*.txt)|*.txt"
                                        };
            if(sfdialog.ShowDialog(this)==DialogResult.OK)
            {
                File.WriteAllText(sfdialog.FileName, txtFieldProp.Text);
            }
        }

        private void tsFindData_Click(object sender, EventArgs e)
        {
            FindData fd=new FindData(treeListViewData);
            fd.ShowDialog(this);
        }

        private void DeleteDocMenuData_Click(object sender, EventArgs e)
        {
            var model = Utils.GetRootBindableDoc(treeListViewData);
            if (lstCollection.SelectedItems.Count > 0)
            {
                SelectedDB.GetCollection(SelectedCollection).Remove(new QueryDocument(model.BsonParent.ToBsonDocument()));

                lstCollection_ItemSelectionChanged(lstCollection,
                                                   new ListViewItemSelectionChangedEventArgs(
                                                       lstCollection.SelectedItems[0], 0, true));
            }
        }

        private void treeViewDBExplorer_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
