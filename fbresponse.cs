using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_WFA
{
    class fbresponse
    {
        public class Rootobject
        {
            public Datum[] data { get; set; }
            public Paging paging { get; set; }
        }

        public class Paging
        {
            public string next { get; set; }
        }

        public class Datum
        {
            public string category { get; set; }
            public string name { get; set; }
            public string id { get; set; }
            public Category_List[] category_list { get; set; }
        }

        public class Category_List
        {
            public string id { get; set; }
            public string name { get; set; }
        }
    }

    class fbgresponse
    {

        public class Rootobject
        {
            public Datum[] data { get; set; }
            public Paging paging { get; set; }
        }

        public class Paging
        {
            public string next { get; set; }
        }

        public class Datum
        {
            public string name { get; set; }
            public int version { get; set; }
            public string id { get; set; }
        }

    }
}
