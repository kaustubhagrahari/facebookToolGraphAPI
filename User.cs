using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Facebook;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace FB_WFA
{
    public partial class User : Form
    {
        string cur,cur1;
        List<string> stored = new List<string>();
        string token;
        int set = 1;
        public User(string token)
        {
            InitializeComponent();
            this.token = token;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                timer1.Start();
                cur1 = "Connecting to Internet";
                cur = "Connecting to server";
                Thread tr = new Thread(() => gsearch());
                tr.Start();
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void gsearch()
        {
            try
            {
                int x= 0;
                Thread.Sleep(2000);
                cur1 = "Connected to Internet ...";
                cur = "Searching in Facebook Database ...";
                //token = getToken();
                Thread.Sleep(1000);
                Invoke(new Action(() => dataGridView1.Rows.Clear()));
                FacebookClient fb = new FacebookClient(token);
                string result = (fb.Get("search?type=user&q="+textBox1.Text)).ToString();
                userresponse.Rootobject obj = (userresponse.Rootobject)JsonConvert.DeserializeObject<userresponse.Rootobject>(result);

                stored.Clear();
                foreach(var data in obj.data)
                {
                    Invoke(new Action(() => label4.Text = (x + 1).ToString()));
                    Invoke(new Action(() => dataGridView1.Rows.Add()));
                    Invoke(new Action(() => dataGridView1.Rows[x].Cells["srn"].Value = x+1));
                    Invoke(new Action(() => dataGridView1.Rows[x].Cells["name"].Value = data.name));
                    Invoke(new Action(() => dataGridView1.Rows[x].Cells["profile_link"].Value = "Check User Profile"));
                    stored.Add(data.id);
                    x++;
                }

                cur = "Searche Completed Successfully !!!";
                cur1 = "Disconnecting from Internet ...";
                Thread.Sleep(2000);
                cur1 = "Disconnected from Internet !!!";
                Thread.Sleep(1000);
                   Invoke(new Action(() => timer1.Stop()));
                   Invoke(new Action(() => button1.Enabled = true));
                   Invoke(new Action(() => linkLabel1.Enabled = true));
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                cur1 = e1.Message;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                   // preview a = new preview("https://www.facebook.com/"+stored[e.RowIndex]);
                   // a.ShowDialog();
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkLabel1.Enabled = false;
                linkLabel2.Enabled = true;
                set = 1;
                Thread tr = new Thread(()=>lastupdate());
                tr.Start();
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        private void lastupdate()
        {
            try
            {
                int i = 0;
                FacebookClient fb = new FacebookClient(token);
                foreach (string user in stored)
                {
                    if (set == 1)
                    {
                        string result = (fb.Get("/" + user)).ToString();
                        userinfo.Rootobject obj = (userinfo.Rootobject)JsonConvert.DeserializeObject<userinfo.Rootobject>(result);
                        Invoke(new Action(() => dataGridView1.Rows[i].Cells["updt"].Value = obj.updated_time));
                        i++;
                    }
                }
               Invoke(new Action(() => linkLabel1.Enabled = false));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            } 
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            set = 0;
            linkLabel2.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label12.Text = cur1;
                label11.Text = cur;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private string getToken()
        {
            using (WebClient client = new WebClient())
            {
                cur = "Creating link to Facebook Server";
                Thread.Sleep(100);
                Stream data = client.OpenRead("https://graph.facebook.com/oauth/access_token?client_id=260683033987662&client_secret=962856d913531094e2c32c338d249952&grant_type=client_credentials");
                StreamReader reader = new StreamReader(data);
                cur1 = "Connetced to Internet";

                cur = "Link Created";
                Thread.Sleep(1000);
                return reader.ReadToEnd().Split('=')[1];
            }
        }
    }
}
