using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ApexMongoMonitor
{
    class CollectionProperty
    {
        [Category("Common")]
        public string Catalog { set; get; }

        [Category("Common")]
        public string Comment { set; get; }
        [Category("Common")]
        public long Count { set; get; }
        [Category("Common")]
        public string Definition { set; get; }
        [Category("Common")]
        public int Flags { set; get; }
        [Category("Common")]
        public bool IsSystemCollection { set; get; }
        [Category("Common")]
        public string Name { set; get; }
        [Category("Common")]
        public string NameSpace { set; get; }
        [Category("Common")]
        public int nIndexes { set; get; }
        [Category("Common")]
        public long NumExtents { set; get; }
        [Category("Common")]
        public double PaddingFactor { set; get; }
        [Category("Common")]
        public string Schema { set; get; }

        public string Scripts { set; get; }


        [Category("Size")]
        public long DataSize { set; get; }

        [Category("Size")]
        public string IndexSizes { set; get; }

        [Category("Size")]
        public long LastExtentSize { set; get; }

        [Category("Size")]
        public double SizeStr { set; get; }

        [Category("Size")]
        public long StorageSize { set; get; }
        [Category("Size")]
        public long TotalIndexSize { set; get; }
    }
}
