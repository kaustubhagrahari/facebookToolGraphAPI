using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_WFA
{
    class user_obj
    {

        public class Rootobject
        {
            public string id { get; set; }
            public string birthday { get; set; }
            public Education[] education { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string gender { get; set; }
            public Hometown hometown { get; set; }
            public string[] interested_in { get; set; }
            public Language[] languages { get; set; }
            public string last_name { get; set; }
            public string link { get; set; }
            public Location location { get; set; }
            public string locale { get; set; }
            public string name { get; set; }
            public float timezone { get; set; }
            public DateTime updated_time { get; set; }
            public bool verified { get; set; }
        }

        public class Hometown
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Location
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Education
        {
            public School school { get; set; }
            public string type { get; set; }
            public Year year { get; set; }
        }

        public class School
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Year
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Language
        {
            public string id { get; set; }
            public string name { get; set; }
        }

    }
}
