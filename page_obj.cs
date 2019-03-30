using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB_WFA
{
    class page_obj
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
            public string access_token { get; set; }
            public string category { get; set; }
            public Category_List[] category_list { get; set; }
            public string name { get; set; }
            public string id { get; set; }
            public string[] perms { get; set; }
        }

        public class Category_List
        {
            public string id { get; set; }
            public string name { get; set; }
        }

    }

    class fbjsonobjects
    {


        public class Rootobject
        {
            public string id { get; set; }
            public string about { get; set; }
            public string awards { get; set; }
            public bool can_post { get; set; }
            public string category { get; set; }
            public int checkins { get; set; }
            public Cover cover { get; set; }
            public string description { get; set; }
            public string founded { get; set; }
            public bool has_added_app { get; set; }
            public bool is_community_page { get; set; }
            public bool is_published { get; set; }
            public int likes { get; set; }
            public string link { get; set; }
            public Location location { get; set; }
            public string mission { get; set; }
            public string name { get; set; }
            public Parking parking { get; set; }
            public string phone { get; set; }
            public int talking_about_count { get; set; }
            public string username { get; set; }
            public string website { get; set; }
            public int were_here_count { get; set; }
        }

        public class Cover
        {
            public string cover_id { get; set; }
            public int offset_x { get; set; }
            public int offset_y { get; set; }
            public string source { get; set; }
            public string id { get; set; }
        }

        public class Location
        {
            public string city { get; set; }
            public string country { get; set; }
            public string street { get; set; }
            public string zip { get; set; }
        }

        public class Parking
        {
            public int lot { get; set; }
            public int street { get; set; }
            public int valet { get; set; }
        }


    }
}