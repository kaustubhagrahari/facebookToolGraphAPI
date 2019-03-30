using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Facebook;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.IO;


namespace FB_WFA
{
    public partial class Form1 : Form
    {
        string dir = string.Empty;
        FacebookClient fb = new FacebookClient();
        string token,fileread;
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Log Out")

                logout();
            else
            {

                webBrowser1.Visible = true;
               // webBrowser1.Navigate("https://www.facebook.com/v1.0/dialog/oauth?redirect_uri=http://www.nokia.com/en_int/Copy_All_This_Url&scope=email%2Cpublish_actions%2Cuser_about_me%2Cuser_actions.books%2Cuser_actions.music%2Cuser_actions.news%2Cuser_actions.video%2Cuser_activities%2Cuser_birthday%2Cuser_education_history%2Cuser_events%2Cuser_games_activity%2Cuser_groups%2Cuser_hometown%2Cuser_interests%2Cuser_likes%2Cuser_location%2Cuser_notes%2Cuser_photos%2Cuser_questions%2Cuser_relationship_details%2Cuser_relationships%2Cuser_religion_politics%2Cuser_status%2Cuser_subscriptions%2Cuser_videos%2Cuser_website%2Cuser_work_history%2Cfriends_about_me%2Cfriends_actions.books%2Cfriends_actions.music%2Cfriends_actions.news%2Cfriends_actions.video%2Cfriends_activities%2Cfriends_birthday%2Cfriends_education_history%2Cfriends_events%2Cfriends_games_activity%2Cfriends_groups%2Cfriends_hometown%2Cfriends_interests%2Cfriends_likes%2Cfriends_location%2Cfriends_notes%2Cfriends_photos%2Cfriends_questions%2Cfriends_relationship_details%2Cfriends_relationships%2Cfriends_religion_politics%2Cfriends_status%2Cfriends_subscriptions%2Cfriends_videos%2Cfriends_website%2Cfriends_work_history%2Cads_management%2Ccreate_event%2Ccreate_note%2Cexport_stream%2Cfriends_online_presence%2Cmanage_friendlists%2Cmanage_notifications%2Cmanage_pages%2Cphoto_upload%2Cpublish_stream%2Cread_friendlists%2Cread_insights%2Cread_mailbox%2Cread_page_mailboxes%2Cread_requests%2Cread_stream%2Crsvp_event%2Cshare_item%2Csms%2Cstatus_update%2Cuser_online_presence%2Cvideo_upload%2Cxmpp_login&response_type=token+code&client_id=200758583311692&_rdr");

                webBrowser1.Navigate(fb.GetLoginUrl(new
                {

                    //client_id = "41158896424",
                    client_id = "124024574287414",
                    //client_id = "6628568379",
                    authorizeUrl = "https://www.instagram.com",
                    redirect_uri = "https://www.instagram.com/accounts/signup/index/",
                    //redirect_uri = "http://www.sharpdroid.com/", 
                    response_type = "token",

                    //scope = "publish_actions,publish_pages,manage_pages,user_groups,publish_actions,email,user_likes,publish_stream,user_friends,public_profile" // Add other permissions as needed
                    scope = "user_birthday, user_religion_politics, user_relationships, user_relationship_details, user_hometown, user_location, user_likes, user_education_history, user_work_history, user_website, user_groups, user_managed_groups, user_events, user_photos, user_videos, user_friends, user_about_me, user_status, user_games_activity, user_posts, read_stream, read_mailbox, email, manage_pages, publish_pages, publish_actions, read_custom_friendlists, user_actions.books, user_actions.music, user_actions.video, user_actions.news, public_profile,manage_pages,publish_actions,publish_pages"
                }));

            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            var fb = new FacebookClient();
            FacebookOAuthResult result;
            if(fb.TryParseOAuthCallbackUrl(e.Url,out result))
            {
                if(result.IsSuccess)
                {
                    webBrowser1.Navigate("www.facebook.com");
                    textBox1.Text = token = result.AccessToken;
                    createdir();
                    System.IO.File.WriteAllText(dir + "\\key.crypt", token);
                    button1.Text = "Log Out";
                    
                    Profile a = new Profile(token);
                    this.Hide();
                    button3.Enabled = true;
                    a.ShowDialog();
                    this.Show();
                    
                }
                else
                {
                    richTextBox1.Text = result.ErrorDescription;
                    richTextBox1.Text += Environment.NewLine + "Reason :" + result.ErrorReason;
                }
            }
            
        }

        private string createdir()
        {

            try
            {
                dir = System.IO.Path.Combine(Environment.CurrentDirectory);
                if(Directory.Exists(dir))
                {
                    return dir;
                }
                else
                {
                    System.IO.Directory.CreateDirectory(dir);
                    return dir;
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return dir;
            }
        }

        private void logout()
        {
            var fb = new FacebookClient();

            webBrowser1.Navigate( fb.GetLogoutUrl(new
            {
                access_token = token,

                next = "http://facebook.com"

            }));
            button1.Text = "Log In";
            //Session.Remove("AccessToken");

            //Response.Redirect(logoutUrl.AbsoluteUri);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Profile a = new Profile(token);
                this.Hide();
                a.ShowDialog();
                this.Show();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        private  string getToken()
        {
            string tkn = string.Empty;
            try
            {
                return tkn;
            }catch(Exception e1)
            {
                return tkn;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread tr = new Thread(() => webstart());
            tr.SetApartmentState(ApartmentState.STA);
            tr.Start();
        }


        private void webstart()
        {
            try
            {
                

                createdir();
                string ndir = dir + "\\key.crypt";
                if (File.Exists(dir + "\\key.crypt"))
                {
                    try
                    {
                        token = System.IO.File.ReadAllText(dir + "\\key.crypt");
                        FacebookClient fb = new FacebookClient(token);
                        string result = fb.Get("/me?fields=id,name").ToString();
                       
                        Invoke(new Action(() => button1.Text = "Log Out"));
                        Profile a = new Profile(token);
                        Invoke(new Action(() => this.Hide()));
                        Invoke(new Action(()=>button3.Enabled = true));
                        a.ShowDialog();
                        
                        Invoke(new Action(()=> this.Show()));
                        Invoke(new Action(() => webBrowser1.Visible = true));
                        
                    }
                    catch (Exception e1)
                    {

                        if (e1.ToString().Contains(("Error validating access token")) || e1.ToString().Contains(("Invalid OAuth access token")))
                        {
                            MessageBox.Show("Your key is old or not correct. Kindly Re-authorize your account by Signing-In.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Invoke(new Action(() => webBrowser1.Navigate(fb.GetLoginUrl(new
                            {

                                
                               client_id = "200758583311692",
                                //client_id = "41158896424",
                    authorizeUrl = "https://www.facebook.com/dialog/oauth/",
                    redirect_uri = "http://www.facebook.com/connect/login_success.html",
                    //redirect_uri = "http://www.sharpdroid.com/", 
                    response_type = "token",

                    //scope = "publish_actions,publish_pages,manage_pages,user_groups,publish_actions,email,user_likes,publish_stream,user_friends,public_profile" // Add other permissions as needed
                    
                                //scope = "publish_actions,publish_pages,manage_pages,user_groups,publish_actions,email,user_likes,publish_stream,user_friends,public_profile" // Add other permissions as needed
                                scope = "user_birthday, user_religion_politics, user_relationships, user_relationship_details, user_hometown, user_location, user_likes, user_education_history, user_work_history, user_website, user_groups, user_managed_groups, user_events, user_photos, user_videos, user_friends, user_about_me, user_status, user_games_activity, user_posts, read_stream, read_mailbox, email, manage_pages, publish_pages, publish_actions, whitelisted_offline_access, read_custom_friendlists, user_actions.books, user_actions.music, user_actions.video, user_actions.news, public_profile,manage_pages,publish_actions,publish_pages"

                            }))));
                            Invoke(new Action(() => webBrowser1.Visible = true));
                        }
                        else
                            MessageBox.Show(e1.ToString());
                    }

                }
                else
                {
                    Invoke(new Action(() => webBrowser1.Navigate(fb.GetLoginUrl(new
                        {

                            //client_id = "41158896424",
                            client_id = "200758583311692",
                            authorizeUrl = "https://www.facebook.com/dialog/oauth/",
                            redirect_uri = "http://www.facebook.com/connect/login_success.html",
                            response_type = "token",

                            //scope = "publish_actions,publish_pages,manage_pages,user_groups,publish_actions,email,user_likes,publish_stream,user_friends,public_profile" // Add other permissions as needed
                            scope = "user_birthday, user_religion_politics, user_relationships, user_relationship_details, user_hometown, user_location, user_likes, user_education_history, user_work_history, user_website, user_groups, user_managed_groups, user_events, user_photos, user_videos, user_friends, user_about_me, user_status, user_games_activity, user_posts, read_stream, read_mailbox, email, manage_pages, publish_pages, publish_actions, whitelisted_offline_access, read_custom_friendlists, user_actions.books, user_actions.music, user_actions.video, user_actions.news, public_profile,manage_pages,publish_actions,publish_pages"

                        }))));
                    Invoke(new Action(() => webBrowser1.Visible = true));
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                logout();
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

    }
}



//client_id = "41158896424",

//redirect_uri = "http://www.facebook.com/connect/login_success.html",
