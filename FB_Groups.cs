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
using System.Threading;
using Newtonsoft.Json;

namespace FB_WFA
{
    public partial class FB_Groups : Form
    {
        string token = string.Empty;
        string cur1 = "------", cur = "------";
        List<string> stored = new List<string>();
        public FB_Groups(string token)
        {
            InitializeComponent();
            this.token = token;
            timer1.Tick += timer1_Tick;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
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
                Thread.Sleep(1000);
                Invoke(new Action(() => dataGridView1.Rows.Clear()));
                FacebookClient fb = new FacebookClient(token);
                string result = (fb.Get("/search?q="+textBox1.Text+"&type=group")).ToString();
                fbgresponse.Rootobject obj = (fbgresponse.Rootobject)JsonConvert.DeserializeObject<fbgresponse.Rootobject>(result);

                stored.Clear();
                foreach(var data in obj.data)
                {
                    Invoke(new Action(() => label4.Text = ( x + 1).ToString()));
                    Invoke(new Action(() => dataGridView1.Rows.Add()));
                    Invoke(new Action(() => dataGridView1.Rows[x].Cells["srn"].Value = x+1));
                    Invoke(new Action(() => dataGridView1.Rows[x].Cells["gname"].Value = data.name));
                    Invoke(new Action(() => dataGridView1.Rows[x].Cells["gid"].Value = "Click to Join Group"));
                    stored.Add(data.id);
                    Invoke(new Action(() => dataGridView1.Rows[x].Cells["vers"].Value = data.version));
                    x++;
                }
                cur = "Searche Completed Successfully !!!";
                cur1 = "Disconnecting from Internet ...";
                Thread.Sleep(2000);
                cur1 = "Disconnected from Internet !!!";
                Thread.Sleep(1000);
               Invoke(new Action(() => timer1.Stop()));
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
                cur1 = e1.Message;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label12.Text = cur1;
                label11.Text = cur;
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                  if(e.ColumnIndex == 3)
                  {
                     // preview a = new preview("https://www.facebook.com/"+stored[e.RowIndex]);
                     // a.ShowDialog();                     
                  }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void FB_Groups_Load(object sender, EventArgs e)
        {

        }
    }
}
