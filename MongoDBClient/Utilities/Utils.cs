using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApexMongoMonitor
{
    static class Utils
    {
        private const string MainFormTitle = "ApexMongoMonitor";

        static Utils()
        { }

        public static void PrepareFormTitle(MainForm mf, bool connected2DB)
        {
            ToolStrip ts = (ToolStrip)mf.Controls.Find("toolStripConnectDisconnect", true).First();

            ToolStripButton tsBtnConnect = ts.Items.Cast<ToolStripButton>().Where(btn => btn.Name.Equals("tsBtnConnect")).First();
            ToolStripButton tsBtnDisConnect = ts.Items.Cast<ToolStripButton>().Where(btn => btn.Name.Equals("tsBtnDisConnect")).First();

            if (connected2DB)
            {
                mf.Text = mf.SelectedDB.Server.Settings.Server + "/" + mf.SelectedDB.Name + " - " + MainFormTitle;

                tsBtnConnect.Enabled = false;
                tsBtnDisConnect.Enabled = true;
            }
            else
            {
                mf.Text = MainFormTitle;

                tsBtnConnect.Enabled = true;
                tsBtnDisConnect.Enabled = false;
            }
        }
        

        //public static ArrayList GetBindableDocsFromCollection(MongoDatabase selectedDB,
        //    string selectedCollectionName)

        public static IList<BsonDocumentBindable> GetBindableDocsFromCollection(MongoDatabase selectedDB,
                string selectedCollectionName)
        {
            //try
            //{
                MongoCollection coll = selectedDB.GetCollection(selectedCollectionName);

                var bsonDocumentBindable = coll.FindAllAs<BsonDocument>().AsEnumerable();

                // List all documents as the roots of the tree
                IList<BsonDocumentBindable> roots = new List<BsonDocumentBindable>();
                //var roots = new ArrayList();
                int x = 1;
                foreach (BsonDocument doc in bsonDocumentBindable)
                {
                    //roots.Add(new BsonDocumentBindable(string.Format("({0}) Document ({1})",x, doc.ElementCount), null, doc.Elements));
                    roots.Add(new BsonDocumentBindable(string.Format("({0}) Document ({1})", x, doc.ElementCount), null, doc.BsonType, doc));
                    x++;
                }
                return roots;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return new ArrayList();
            //}
        }

        //public static ArrayList GetBindableDocsFromElements(BsonDocumentBindable doc)
        public static IList<BsonDocumentBindable> GetBindableDocsFromElements(BsonDocumentBindable doc)
        {
            try
            {
                //var alist = new ArrayList();
                IList<BsonDocumentBindable> alist = new List<BsonDocumentBindable>();

                /*
                foreach (var el in doc.BsonElements)
                {
                    if ((el.Value.IsBsonDocument))
                    {
                        var bsonDoc = el.Value.ToBsonDocument();
                        alist.Add(new BsonDocumentBindable(string.Format("{0} ({1})", el.Name, bsonDoc.ElementCount),
                                                           null, bsonDoc.Elements));
                    }
                    else if (el.Value.IsBsonArray)
                    {
                        List<BsonElement> lstEls = new List<BsonElement>();
                        BsonArray arr = (BsonArray)el.Value;
                        foreach (var a in arr.Values)
                        {
                            lstEls.Add(new BsonElement("Document", a));
                        }

                        var toDoc = new BsonDocument {
				            {lstEls} 
			            };


                        alist.Add(new BsonDocumentBindable(string.Format("{0} ({1})", el.Name, arr.Count),
                                                           null, toDoc));// lstEls));
                    }
                    else
                    {
                        alist.Add(new BsonDocumentBindable(el.Name, el.Value, null));
                    }
                }
                */

                if (doc.BsonParent==null)
                    return alist;

                if(doc.BsonParent.IsBsonDocument)
                {
                    BsonDocument bsonDoc = (BsonDocument)doc.BsonParent;
                    foreach (var el in bsonDoc.Elements)
                    {
                        if(el.Value.IsBsonDocument)
                            alist.Add(TransformBsonDoc(el.Value,el.Name));
                        else if (el.Value.IsBsonArray)
                        {
                            alist.Add(TransformBsonArray(el.Value, el.Name));
                            //BsonArray arr = (BsonArray)el.Value;
                            //alist.Add(new BsonDocumentBindable(string.Format("{0} ({1})", el.Name, arr.Count),
                            //                                   null, BsonType.Array, arr));
                        }
                        else
                            alist.Add(new BsonDocumentBindable(el.Name, el.Value, el.Value.BsonType, el.Value));
                    }
                }
                else if(doc.BsonParent.IsBsonArray)
                {
                    BsonArray arr = (BsonArray)doc.BsonParent;

                    int index = 0;
                    foreach (var arrEl in arr.Values)
                    {
                        if (arrEl.IsBsonDocument)
                            alist.Add(TransformBsonDoc(arrEl, "Document"));
                        else if (arrEl.IsBsonArray)
                        {
                            alist.Add(TransformBsonArray(arrEl, "[" + index + "]"));

                            /*BsonArray elArr = (BsonArray)arrEl;
                            alist.Add(new BsonDocumentBindable(string.Format("{0} ({1})", "[" + index + "]", elArr.Count),
                                                           null, BsonType.Array, elArr));
                             */
                            index++;
                        }
                        else
                        {
                            alist.Add(new BsonDocumentBindable("[" + index + "]", arrEl, arrEl.BsonType, arrEl));
                            index++;
                        }
                    }
                }

                return alist;


                /* OLD code ************************
                //We wont process any data, if it is not of bsonDocument type.
                if (!doc.BsonParent.IsBsonDocument)
                    return new ArrayList();

                BsonDocument bsonDoc = (BsonDocument)doc.BsonParent;
                
                //foreach (var el in doc.BsonDoc.Elements)
                foreach (var el in bsonDoc.Elements)
                {
                    if (el.Value.IsBsonDocument)
                    {
                        var bsonElDoc = el.Value.ToBsonDocument();
                        alist.Add(new BsonDocumentBindable(string.Format("{0} ({1})", el.Name, bsonElDoc.ElementCount),
                                                           null, bsonElDoc.BsonType, bsonElDoc));
                    }
                    else if (el.Value.IsBsonArray)
                    {
                        List<BsonElement> lstEls = new List<BsonElement>();
                        BsonArray arr = (BsonArray)el.Value;

                        int x = 0;
                        foreach (var a in arr.Values)
                        {
                            if (!a.IsBsonDocument && !a.IsBsonArray)
                                lstEls.Add(new BsonElement("[" + x +"]",a));
                            else
                                lstEls.Add(new BsonElement("Document" + x, a));
                            x++;
                        }

                        var toDoc = new BsonDocument(lstEls);

                        alist.Add(new BsonDocumentBindable(string.Format("{0} ({1})", el.Name, toDoc.ElementCount),
                                                           null, BsonType.Null, arr));//toDoc));
                    }
                    else
                    {
                        //alist.Add(new BsonDocumentBindable(el.Name, el.Value, el.Value.BsonType, null));
                        alist.Add(new BsonDocumentBindable(el.Name, el.Value, el.Value.BsonType, el.Value));
                    }
                }
                return alist;
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;// new List<BsonDocumentBindable>();// new ArrayList();
            }
        }

        private static BsonDocumentBindable TransformBsonDoc(BsonValue el, string name)
        {
            var bsonElDoc = el.ToBsonDocument();
            //bsonElDoc.AllowDuplicateNames = true;

            return new BsonDocumentBindable(string.Format("{0} ({1})", name, bsonElDoc.ElementCount),
                                               null, bsonElDoc.BsonType, bsonElDoc);
        }
        private static BsonDocumentBindable TransformBsonArray(BsonValue arrVal, string name)
        {
            var bsonArr = (BsonArray)arrVal;
            return new BsonDocumentBindable(string.Format("{0} ({1})", name, bsonArr.Count),
                                               null, BsonType.Array, bsonArr);
        }


        public static CollectionProperty GetCollectionProperty(MongoCollection collection)
        {
            CollectionStatsResult stat = collection.GetStats();
            CollectionProperty colProp = new CollectionProperty()
            {
                Catalog = string.Empty,
                Comment = string.Empty,
                Count = collection.Count(),
                Definition = string.Empty,
                Flags = stat.Flags,
                IsSystemCollection = false,
                Name = collection.Name,
                NameSpace = stat.Namespace,
                nIndexes = stat.IndexCount,
                NumExtents = stat.ExtentCount,
                PaddingFactor = stat.PaddingFactor,
                Schema = string.Empty,
                Scripts = "(Collection)",
                DataSize = collection.GetTotalDataSize(),
                IndexSizes = stat.IndexSizes.ToString(),
                LastExtentSize = stat.LastExtentSize,
                SizeStr = stat.AverageObjectSize,// string.Empty,
                StorageSize = stat.StorageSize,
                TotalIndexSize = stat.TotalIndexSize
            };
            return colProp;
        }

        public static BsonDocumentBindable GetRootBindableDoc(TreeListView treeListViewData)
        {
            if (treeListViewData.SelectedItem == null)
                return null;

            var model = treeListViewData.SelectedItem.RowObject;
            while (treeListViewData.GetParent(model) != null)
            {
                model = treeListViewData.GetParent(model);
            }

            return (BsonDocumentBindable)model;
        }
    }

    class BsonDocumentBindable
    {
        //public BsonDocumentBindable(string name, BsonValue value, IEnumerable<BsonElement> elements)
        //public BsonDocumentBindable(string name, BsonValue value, BsonType type, BsonDocument bsonDoc)
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name of the document</param>
        /// <param name="value">value of the document</param>
        /// <param name="type"></param>
        /// <param name="bsonParent"></param>
        public BsonDocumentBindable(string name, BsonValue value, BsonType type, BsonValue bsonParent)
        {
            Name = name;
            Value = value;
            Type = type;
            //BsonElements = elements;
            //BsonDoc = bsonDoc;

            BsonParent = bsonParent;
        }

        public string Name { get; set; }
        public BsonValue Value { get; set; }
        public BsonType Type { get; set; }

        //public IEnumerable<BsonElement> BsonElements { get; set; }
        //public BsonDocument BsonDoc { get; set; }

        //prop holds either bsonDocument/bsonValue
        public BsonValue BsonParent { get; set; }
    }
}
