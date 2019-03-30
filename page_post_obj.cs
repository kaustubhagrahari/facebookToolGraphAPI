using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_WFA
{
    class page_post_obj
    {
        public class Rootobject
        {
            public Datum[] data { get; set; }
            public Paging paging { get; set; }
        }

        public class Paging
        {
            public string previous { get; set; }
            public string next { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public From from { get; set; }
            public string message { get; set; }
            public string picture { get; set; }
            public string link { get; set; }
            public string name { get; set; }
            public string caption { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
            public Action[] actions { get; set; }
            public Privacy privacy { get; set; }
            public string type { get; set; }
            public string status_type { get; set; }
            public DateTime created_time { get; set; }
            public DateTime updated_time { get; set; }
            public bool is_hidden { get; set; }
            public Likes likes { get; set; }
            public Comments comments { get; set; }
            public Admin_Creator admin_creator { get; set; }
            public Shares shares { get; set; }
            public string story { get; set; }
            public string object_id { get; set; }
            public Application application { get; set; }
            public With_Tags with_tags { get; set; }
        }

        public class From
        {
            public string category { get; set; }
            public Category_List[] category_list { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }

        public class Category_List
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Privacy
        {
            public string value { get; set; }
            public string description { get; set; }
            public string friends { get; set; }
            public string allow { get; set; }
            public string deny { get; set; }
        }

        public class Likes
        {
            public int count { get; set; }
            public Datum1[] data { get; set; }
        }

        public class Datum1
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Comments
        {
            public int count { get; set; }
            public Datum2[] data { get; set; }
        }

        public class Datum2
        {
            public string id { get; set; }
            public From1 from { get; set; }
            public string message { get; set; }
            public DateTime created_time { get; set; }
            public int likes { get; set; }
        }

        public class From1
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Admin_Creator
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Shares
        {
            public int count { get; set; }
        }

        public class Application
        {
            public string name { get; set; }
            public string _namespace { get; set; }
            public string id { get; set; }
        }

        public class With_Tags
        {
            public Datum3[] data { get; set; }
        }

        public class Datum3
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Action
        {
            public string name { get; set; }
            public string link { get; set; }
        }
    }
}
