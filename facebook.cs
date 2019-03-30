using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;

namespace FB_WFA
{
    class facebook
    {
        public static string get_page(FacebookClient fb)
        {           
             return (fb.Get("/me/accounts").ToString());
        }

        public static string get_group(FacebookClient fb)
        {
            return fb.Get("/me/groups").ToString();
        }

        public static string get_posts(FacebookClient fb,string id)
        {
            return fb.Get("/" + id + "/feed?limit=250").ToString();
        }



    }
}
