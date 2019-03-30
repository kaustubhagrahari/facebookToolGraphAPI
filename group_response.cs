using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_WFA
{
    public class group_response
    {
        public class Rootobject
        {
            public Datum[] data { get; set; }
            public Paging paging { get; set; }
        }

        public class Paging
        {
            public Cursors cursors { get; set; }
        }

        public class Cursors
        {
            public string before { get; set; }
            public string after { get; set; }
        }

        public class Datum
        {
            public string name { get; set; }
            public string id { get; set; }
            public bool administrator { get; set; }
            public int bookmark_order { get; set; }
            public int unread { get; set; }
        }

    }
}
