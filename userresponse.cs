using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_WFA
{
    class userresponse
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
            public string id { get; set; }
        }
    }
    class userinfo
    {
        public class Rootobject
        {
            public string id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string link { get; set; }
            public string name { get; set; }
            public string updated_time { get; set; }
        }

        
    }
}
