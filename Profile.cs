using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;
using System.Dynamic;
using Facebook;
using System.IO;

namespace FB_WFA
{
    public partial class Profile : Form
    {
        string token = string.Empty;
        public Profile(string tk)
        {
            InitializeComponent();
            token = tk;
            this.Resize+=Profile_Resize;
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            Thread tr = new Thread(() => yourname());
            tr.Start();
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            //pictureBox3.Anchor = AnchorStyles.None;

        }

       

        private void yourname()
        {
            try
            {
                FacebookClient fb = new FacebookClient(token);
                dynamic result = fb.Get("/me?name");

               Invoke(new Action(()=>label2.Text = result.name));
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }


        private void catsearch()
        {
            try
            {

                //string result = new System.Net.WebClient().DownloadString(https://graph.facebook.com/);


                    Thread.Sleep(1000);
                    // this.Invoke(new Action(() => dataGridView1.Rows.Clear()));
                    //found = "--";
                    //cur = "Searching in Facebook Database";
                    var fbclient = new Facebook.FacebookClient(token);

                    string result = (fbclient.Get("/me/groups?pretty=0&limit=1000")).ToString();
                    group_response.Rootobject jobj = (group_response.Rootobject)JsonConvert.DeserializeObject<group_response.Rootobject>(result);

                   

                    //a.Clear();
                    //b.Clear();
                    //this.Invoke(new Action(() => listBox1.Items.Clear()));
                    //this.Invoke(new Action(() => listBox2.Items.Clear()));
                    foreach (var data in jobj.data)
                    {
                       // richTextBox1.Text += data.name + Environment.NewLine;
                    }

                    //a1 = new HashSet<String>(a).ToList<String>();
                    //a1.Sort();
                    //this.Invoke(new Action(() => comboBox1.Items.Clear()));
                    //if (a1.Count > 0)
                    //{
                    //    for (int i = 0; i < a1.Count; i++)
                    //    {
                    //        this.Invoke(new Action(() => comboBox1.Items.Add(a1[i])));
                    //    }
                    //    this.Invoke(new Action(() => comboBox1.SelectedIndex = 0));
                    //    if (dt.Rows.Count > 0)
                    //        dt.Rows.Clear();

                    //    createtable();
                    //    for (int i = 0; i < a1.Count; i++)
                    //    {
                    //        this.Invoke(new Action(() => listBox1.Items.Add(a1[i])));
                    //    }
                    //    Thread.Sleep(3000);
                    //}
                    //cur = "Search Completed Successfully";
                    //cur1 = "Disconnected from Internet";
                    //this.Invoke(new Action(() => groupBox2.Enabled = true));
                    //this.Invoke(new Action(() => label4.Text = a1.Count.ToString()));
                
            }
            catch (Exception e1)
            {
                if (e1.ToString().Contains("The remote name could not be resolved"))
                    MessageBox.Show("We have found that your internet is not working. Kindly check and Try Again");
                else
                    MessageBox.Show(e1.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            shrpag a = new shrpag(token);
            this.Hide();
            a.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FacebookClient fb = new FacebookClient();

            System.Net.WebClient wr = new System.Net.WebClient();
            
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                pg a = new pg(token);
                this.Hide();
                a.ShowDialog();
                this.Show();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                FB_Groups a = new FB_Groups(token);
                this.Hide();
                a.ShowDialog();
                this.Show();
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                User a = new User(token);
                this.Hide();
                a.ShowDialog();
                this.Show();

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void Profile_Resize(object sender, EventArgs e)
        {
            //pictureBox3.Location = new Point((pictureBox3.Parent.ClientSize.Width / 2) - (pictureBox3.Image.Width / 2),
                             //  (pictureBox3.Parent.ClientSize.Height / 2) - (pictureBox3.Image.Height / 2));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
         
        }                     
    }
}
