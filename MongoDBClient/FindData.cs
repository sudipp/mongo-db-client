using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Wrappers;

namespace ApexMongoMonitor
{
    public partial class FindData : Form
    {
        private readonly TreeListView _treeListViewData;
        Regex _rx = new Regex(@"[^\s]"); //white space


        public FindData(TreeListView treeListViewData)
        {
            _treeListViewData = treeListViewData;

            InitializeComponent();
        }

        private void btnClearQuery_Click(object sender, EventArgs e)
        {
            txtQuery.Text = string.Empty;
            txtSort.Text = string.Empty;
            txtField.Text = string.Empty;
            numLimit.Value = 500;
            numSkip.Value = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(_treeListViewData.Roots==null)
                return;

            try
            {
                MainForm mf = (MainForm) Owner;

                MongoDatabase SelectedDB = mf.SelectedDB;
                string SelectedCollection = mf.SelectedCollection;

                MongoCollection coll = SelectedDB.GetCollection(SelectedCollection);
                //var mongoCursor1 = coll.FindAllAs<BsonDocument>().AsEnumerable();

                BsonDocument queryBsonDoc=null;
                QueryDocument queryDoc = null;
                BsonDocument fieldBsonDoc = null;
                FieldsDocument fieldDoc = null;

                if(_rx.IsMatch(txtQuery.Text))
                {
                    queryBsonDoc = BsonSerializer.Deserialize<BsonDocument>(txtQuery.Text.Trim());
                    queryDoc = new QueryDocument(queryBsonDoc.Elements);    
                }

                if (_rx.IsMatch(txtField.Text))
                {
                    fieldBsonDoc = BsonSerializer.Deserialize<BsonDocument>(txtField.Text.Trim());
                    fieldDoc = new FieldsDocument(fieldBsonDoc.Elements);
                }

                var mongoCursor = coll.FindAs<BsonDocument>(queryDoc);
                mongoCursor.Skip = (int)numSkip.Value;
                mongoCursor.Limit = (int)numLimit.Value;

                if (fieldDoc != null)
                {
                    mongoCursor.Fields = fieldDoc;
                }

                if (_rx.IsMatch(txtSort.Text))
                {
                    BsonDocument orderDoc = BsonSerializer.Deserialize<BsonDocument>(txtSort.Text.Trim());
                    var order = new SortByWrapper(orderDoc);
                    if (orderDoc != null)
                        mongoCursor.SetSortOrder(order);
                }


                IList<BsonDocumentBindable> roots = new List<BsonDocumentBindable>();
                int x = 1;
                foreach (BsonDocument doc in mongoCursor)
                {
                    roots.Add(new BsonDocumentBindable(string.Format("({0}) Document ({1})", x, doc.ElementCount), null, doc.BsonType, doc));
                    x++;
                }
                _treeListViewData.Roots = roots;
                _treeListViewData.Refresh();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FindData_Load(object sender, EventArgs e)
        {

        }
    }
}
