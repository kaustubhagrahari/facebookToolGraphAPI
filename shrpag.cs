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
using System.Dynamic;
using System.IO;

namespace FB_WFA
{
    public partial class shrpag : Form 
    {
        int s = 0,row = 0,group =0;
        string pid = string.Empty;
        string[] pgdata = new string[200];
        string[] post_data = new string[270];
        string[] image_link = new string[270];
        string[] groupid = new string[5000];
        string[] groupnm = new string[5000];
        int post_select = 0;
        int count = 0;
        public static group_response.Rootobject gData;
        FacebookClient fb = new FacebookClient();
        public shrpag(string token)
        {
            InitializeComponent();
            fb.AccessToken = token;
            timer1.Tick+=timer1_Tick;
            
        }

        private void shrpag_Load(object sender, EventArgs e)
        {
            Thread tr = new Thread(()=> getpage());
            tr.Start();
            Thread tr1 = new Thread(() => getgroup());
            tr1.Start();
        }

        private void  getpage()
        {
            try
            {
              
              page_obj.Rootobject page = (page_obj.Rootobject)JsonConvert.DeserializeObject<page_obj.Rootobject>(facebook.get_page(fb));
              int i = 0;
              foreach(page_obj.Datum obj in page.data)
              {
                Invoke(new Action(()=> listBox1.Items.Add(obj.name)));
                
                pgdata[i] = obj.id;
                Invoke(new Action(() => label11.Text = (i+1).ToString() + " Pages")); 
                i++;
              }

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void getgroup()
        {
            try
            {

                gData = (group_response.Rootobject)JsonConvert.DeserializeObject<group_response.Rootobject>(facebook.get_group(fb));
                int i = 0;
                
                foreach (group_response.Datum obj in gData.data)
                {
                    groupid[i] = obj.id;
                    groupnm[i] = obj.name;
                   i++;
                   Invoke(new Action(()=> checkedListBox1.Items.Add(obj.name)));
                                    
                }
                Invoke(new Action(() => label10.Text = i.ToString() + " Groups")); 

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                Thread tr = new Thread(() => fill_grid());
                tr.Start();
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
                if (string.Compare("Select All", button3.Text) == 0)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                    button3.Text = "Deselect All";
                }
                else
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, false);

                        button3.Text = "Select All";
                    }
                }
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItems.Count > 0)
                {
                    button1.Enabled = true;
                    label12.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                }
                else
                {
                    button1.Enabled = false;
                    label12.Text = "-----------";
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void fill_grid()
        {
            try
            {
                int i=0;
                page_post_obj.Rootobject posts = new page_post_obj.Rootobject();

             Invoke(new Action(()=>   posts = (page_post_obj.Rootobject)JsonConvert.DeserializeObject<page_post_obj.Rootobject>(facebook.get_posts(fb, pgdata[listBox1.SelectedIndex]))));
             Invoke(new Action(() => dataGridView1.Rows.Clear()));

                foreach (page_post_obj.Datum obj in posts.data)
                {
                  Invoke(new Action(() => dataGridView1.Rows.Add()));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["name"].Value = obj.name));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["message"].Value = obj.message));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["description"].Value = obj.description));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["link"].Value = obj.link));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["pst_source"].Value = "Click to see Post"));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["hyplink"].Value = obj.actions[1].link));
                  post_data[i] = obj.actions[1].link;
                  image_link[i] = obj.picture;
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["created"].Value = obj.created_time));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["privacy"].Value = obj.privacy));
                  Invoke(new Action(() => dataGridView1.Rows[i].Cells["ishidden"].Value = obj.is_hidden));
                  Invoke(new Action (()=> dataGridView1.Rows[i].HeaderCell.Value = (i+1).ToString()));
                  i++;
                  
                }
               
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Thread tr = new Thread(()=>post());
                tr.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                 s = checkedListBox1.CheckedItems.Count;
                if (e.NewValue == CheckState.Checked)
                    s++;
                if (e.NewValue == CheckState.Unchecked)
                    s--;

                Invoke(new Action(() => label13.Text = s.ToString()));
                
                if (s > 0)
                {
                    Invoke(new Action(() => button2.Enabled = true));
                }
                else
                    Invoke(new Action(() => button2.Enabled = false));

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void dataGridView1_CellValueChanged (object sender, DataGridViewCellEventArgs e)
        {
            timer1.Start();
            try
            {
                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value))
                {
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                }
            } catch(Exception e1)
            {

            }
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            FacebookClient fb = new FacebookClient();

            dynamic param = new ExpandoObject();
            param.message = "Hello";
            // param.picture = "https://yt3.ggpht.com/-uNzPuOLvOyI/AAAAAAAAAAI/AAAAAAAAAAA/Xr8VYIc4FRc/s900-c-k-no/photo.jpg";
            //param.link = "https://www.facebook.com/724827514295452/posts/1129955690449297";
            param.link = textBox3.Text;
            param.name = "Name : Like Our Page";
            param.description = "Here is description";

            for (int j = group; j < checkedListBox1.Items.Count; j++)
            {
                if (checkedListBox1.GetItemCheckState(j) == CheckState.Checked)
                {
                    group = j;
                    Invoke(new Action(() => label15.Text = checkedListBox1.Items[j].ToString()));
                    Invoke(new Action(() => label17.Text = "Posting now..."));
                    feed(param, groupid[j]);
                    count++;
                    Invoke(new Action(() => label16.Text = count.ToString()));
                }
            }

        }

        private void post()
        {
            try
            {
                Invoke(new Action(() => label17.Text = "Started Process..."));
                Invoke(new Action(() => button2.Enabled =false));
                dynamic res;
                for (int i = row; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                    {
                        row = i;
                        dynamic param = new ExpandoObject();
                        param.message = dataGridView1.Rows[i].Cells["message"].Value;
                        param.picture = image_link[i];
                        param.link = dataGridView1.Rows[i].Cells["link"].Value;
                        param.name = dataGridView1.Rows[i].Cells["name"].Value;
                        param.description = dataGridView1.Rows[i].Cells["description"].Value;

                    L1:  group+=1;
                        for (int j = group; j < checkedListBox1.Items.Count; j++)
                        {
                            if (checkedListBox1.GetItemCheckState(j) == CheckState.Checked)
                            {
                                group = j;
                                Invoke(new Action(() => label15.Text = checkedListBox1.Items[j].ToString()));
                                Invoke(new Action(() => label17.Text = "Posting now..."));
                                feed(param,groupid[j]);
                                count++;
                                Invoke(new Action(() => label16.Text = count.ToString()));
                            }
                        }

                        Invoke(new Action(()=>dataGridView1.Rows[i].DefaultCellStyle.BackColor= Color.Purple));
                        Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White));
                        group = 0;
                    }
                }
                Invoke(new Action(() => label17.Text = "Posting Completed !!!"));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                

            }
        }

        void feed(dynamic param, string id)
        {
            try
            {
                fb.Post(id + "/feed", param);  
            }catch(Exception e1)
            {

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //MessageBox.Show(post_data[e.RowIndex]);
                if (e.ColumnIndex == 5)
                {
                    preview pr = new preview(post_data[e.RowIndex]);
                    pr.ShowDialog();
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                post_select = 0;
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[row].Cells[0].Value))
                    {
                        post_select++;
                    }
                }
               
                Invoke(new Action(() => label14.Text = post_select.ToString()));
                
                timer1.Stop();
            }catch(Exception e1)
            {

            }
          
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
               if(!string.IsNullOrEmpty(textBox1.Text)&& !string.IsNullOrWhiteSpace(textBox1.Text))
               {
                   textBox2.Enabled = true;
               }
               else
               {
                   textBox2.Text = string.Empty;
                   textBox2.Enabled = false;
               }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            validate.intvalid(textBox1, e);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(textBox2.Text) - Convert.ToInt32(textBox1.Text);
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                if (x <= 20 && x >= 0)
                {
                    for (int i = Convert.ToInt32(textBox1.Text); i <= Convert.ToInt32(textBox2.Text); i++)
                    {
                        checkedListBox1.SetItemChecked(i-1, true);
                    }
                    button2.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                }
            }catch(Exception e1)
            {
                //MessageBox.Show(e1.ToString());
                for(int j=0;j<checkedListBox1.Items.Count;j++)
                {
                    checkedListBox1.SetItemChecked(j, false);
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                validate.intvalid(textBox2, e);
             }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(group == 0)
            {

            }
        }


    }
}

/*
 
 
try
{

}
catch(Exception e1)
{
    MessageBox.Show(e1.ToString());
}
 
 
*/