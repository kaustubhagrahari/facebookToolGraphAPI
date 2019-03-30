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
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using mshtml;


namespace FB_WFA
{
    public partial class preview : Form
    {
        string uri,linkid;
        string cmnd;
        string t1, t2;
        Boolean run = true,shared = false;
        int i = 0;
        int temp = 0;
        HtmlElementCollection links0,links1,links2,links3;
        int link0 = 0, link1 = 0, link2 = 0, link3 = 0,wbvisible=1;
        HtmlElementCollection otl;
        int z = 0;
        int done = 0, shrset=0;
        IList<Thread> thread_pool;
        BackgroundWorker bg = new BackgroundWorker();
        WebBrowser web = new WebBrowser();

        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();

        public preview(string lk)
        {
            InitializeComponent();
            uri = lk;
            
            webBrowser1.ScriptErrorsSuppressed = true ;
            bg.DoWork += bg_DoWork;
            bg.RunWorkerCompleted += bg_RunWorkerCompleted;
            thread_pool = new List<Thread>();
            web.Navigated += web_Navigated;


        }

        void web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            MessageBox.Show("done");
        }        

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
      
            sr();
        }


        private void preview_Load(object sender, EventArgs e)
        {
            try
            {
                Thread tr  = new Thread(()=>chkgnm());
                tr.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void chkgnm()
        {
            try
            {
                Invoke(new Action(()=> webBrowser1.Navigate(uri)));
               // Invoke(new Action(() => web.Navigate(uri)));
                
                foreach(group_response.Datum a in shrpag.gData.data)
                {
                    Invoke(new Action(()=>checkedListBox1.Items.Add(a.name)));
                }
            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.Rows[i].Cells[1].Value = "Processing...";
            webBrowser1.Focus();

            bg.RunWorkerAsync();
            

            //Thread tr = new Thread(() => sr());
            //tr.Name = i.ToString();
            //thread_pool.Add(tr);
            //tr.Start();


            
        }


        public void sr()
        {
            
            try
            {
                if(link0 ==0)
                Invoke(new Action(() => links0 = webBrowser1.Document.GetElementsByTagName("a")));
                link0 = 1;
                    //cmnd = "a";
                shr1: if (shrset == 0)
                    {
                          //  Invoke(new Action(() => timer1.Start()));
                            //Thread.Sleep(1000);
                            Invoke(new Action(() => webBrowser1.Focus()));
                           // Invoke(new Action(() => richTextBox1.Text += "clicking share button" + Environment.NewLine));
                        foreach (HtmlElement link in links0)
                        {
                            string l = link.GetAttribute("className");
                            if (!string.IsNullOrEmpty(l))
                            {

                                if (l.Equals("share_action_link"))
                                {
                                    link.Focus();
                                    Thread.Sleep(300);
                                    System.Windows.Forms.SendKeys.SendWait("{Enter}");
                                   // Invoke(new Action(() => richTextBox1.Text += "Share button" + Environment.NewLine));
                                    shrset = 1;
                                    break;
                                }
                            }
                        }
                    }
                  cmnd = "span";
                  if(link1 == 0)
                  Invoke(new Action(() => links1 = webBrowser1.Document.GetElementsByTagName("span")));
                  link1 = 1;
                 // Invoke(new Action(() => timer1.Start()));
                //  Invoke(new Action(() => richTextBox1.Text += "clicking Share" + Environment.NewLine));
                    //run = true;
                    //while (run)
                    //{

                    //}

                    foreach (HtmlElement link in links1)
                    {
                        if (!string.IsNullOrEmpty(link.InnerText) && link.InnerText.Equals("Share…"))
                        {
                                link.InvokeMember("Click");
                               // Invoke(new Action(() => richTextBox1.Text += "clicked Share" + Environment.NewLine));
                                done = 1;
                                break;
                        }
                       
                    }
                    if (done == 0)
                        goto shr1;
                    else
                        done = 0;


                shr2:
                  //  run = true;
                    Thread.Sleep(5000);
                if(link2==0)
                Invoke(new Action(() => links2 = webBrowser1.Document.GetElementsByTagName("span")));
                link2 = 1;
                   // Invoke(new Action(() => timer1.Start()));
                Thread.Sleep(500);
                    //while (run)
                    //{
                        
                    //}
                   // Invoke(new Action(() => richTextBox1.Text += "clicking Timeline button" + Environment.NewLine));
                    foreach (HtmlElement link in links2)
                    {

                        if (!string.IsNullOrEmpty(link.InnerText))
                        {
                            if (string.Equals(link.InnerText, "On your own Timeline") && link.TabIndex == 0)
                            {

                                System.Windows.Forms.SendKeys.SendWait("+{Tab}");
                                Thread.Sleep(200);
                                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                                Thread.Sleep(200);
                                System.Windows.Forms.SendKeys.SendWait("{Down}");
                                Thread.Sleep(200);
                                System.Windows.Forms.SendKeys.SendWait("{Down}");
                                Thread.Sleep(200);
                                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                                Thread.Sleep(200);
                                done = 1;
                              //  Invoke(new Action(() => richTextBox1.Text += "Clicked Timeline button  " + link.TabIndex + "::" + link.TagName + Environment.NewLine));
                                break;

                            }
                        }
                    }

                    if (done == 0)
                        goto shr2;
                    else
                        done = 0;
                    run = true;
                  
                
                   // Invoke(new Action(() => label1.Text = "33"));
                    //Invoke(new Action(() => timer1.Start()));
                    //run = true;
                    //while (run)
                    //{

                    //}
                    //Invoke(new Action(() => richTextBox1.Text += "Clicking Group" + Environment.NewLine));
                    //foreach (HtmlElement link in links)
                    //{
                    //    if (!string.IsNullOrEmpty(link.InnerText))
                    //    {
                    //        if (link.InnerText.Equals("In a group"))
                    //        {
                    //            Invoke(new Action(() => webBrowser1.Focus()));
                    //            link.InvokeMember("Click");
                    //            Invoke(new Action(() => richTextBox1.Text += "clicked group" + Environment.NewLine));
                    //            break;
                    //        }
                    //    }
                    //}
                    //Invoke(new Action(() => label1.Text = "44"));

                    run = true;
                
                    Invoke(new Action(()=>InvokeOnClick(button2,EventArgs.Empty)));

                    //imp_code(i);
                    
                    while(run)
                    {

                    }
                    
                    Invoke(new Action(() => label1.Text = "5"));
                shr3:
                    run = true;
                if(link3==0)
                    Invoke(new Action(() => links3 = webBrowser1.Document.GetElementsByTagName("li")));
                link3 = 1;
                    //Invoke(new Action(() => timer3.Start()));
                    //while (run)
                    //{

                    //}
                Thread.Sleep(1000);
               //  Invoke(new Action(() => webBrowser1.Focus()));
                  //  Invoke(new Action(() => richTextBox1.Text += "Clicking selected group" + Environment.NewLine));
                    foreach (HtmlElement link in links3)
                    {
                        string l = link.GetAttribute("title");                       
                        if (!string.IsNullOrEmpty(l))
                        {
                                               
                            if (string.Equals(l, dataGridView1.Rows[i].Cells[0].Value.ToString()))
                            {                               
                                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                                
                                Thread.Sleep(300);
                                System.Windows.Forms.SendKeys.SendWait("{Enter}");
                                shared = true;
                                done = 1;
                                
                                Thread.Sleep(3500);
                                break;
                            }
                            else
                            {
                                System.Windows.Forms.SendKeys.SendWait("{Down}");
                            }   
                        }
                    }

                    if (done == 0)
                    {

                        goto shr3;

                    }
                    else
                        done = 0;
                    
                    run = true;

                   if(shared)
                   {
                       shared = false;
                       Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen));
                       Invoke(new Action(() => dataGridView1.Rows[i].Cells[1].Value = "Post Shared"));
                      // Invoke(new Action(() => richTextBox1.Text += "Shared" + Environment.NewLine));
                   }
                   else
                   {
                       Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White));
                       Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Brown));
                       Invoke(new Action(() => dataGridView1.Rows[i].Cells[1].Value = "Issue detected"));
                   }

                   
                  // Invoke(new Action(() => richTextBox1.Text += ""));
                   
                    
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                
                run = true;

               
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                
                if(e.NewValue == CheckState.Checked)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count-1].Cells[0].Value = checkedListBox1.Items[e.Index];
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = "Not Started";
                }
                else if(e.NewValue == CheckState.Unchecked)
                {
                    for(int i =0;i<dataGridView1.Rows.Count;i++)
                    {
                        if(string.Equals(checkedListBox1.Items[e.Index].ToString(),dataGridView1.Rows[i].Cells[0].Value.ToString()))
                        {
                            dataGridView1.Rows.RemoveAt(i);
                        }
                    }
                }

            }catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            //links = webBrowser1.Document.GetElementsByTagName(cmnd);
            run = false;
            timer1.Stop();
        }

        private void otltimer_Tick(object sender, EventArgs e)
        {
            otl = webBrowser1.Document.GetElementsByTagName(cmnd);
            run = false;
            otltimer.Stop();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            //links = webBrowser1.Document.GetElementsByTagName("input");
            timer2.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HtmlElementCollection links = webBrowser1.Document.GetElementsByTagName("input");
            
            try
            {
                foreach (HtmlElement link in links)
                {
                    if (!string.IsNullOrEmpty(link.OuterHtml))
                    {
                        label2.Text = z.ToString();
                        if (link.OuterHtml.Contains("Group name"))
                        {
                            link.Focus();
                            t1 = dataGridView1.Rows[i].Cells[0].Value.ToString();

                            link.SetAttribute("value", t1.TrimEnd(t1[t1.Length - 1]));
                            Thread.Sleep(300);
                            System.Windows.Forms.SendKeys.Send("{"+t1[t1.Length - 1].ToString()+"}");
                            break;
                        }
                    }
                    z++;
                }
                
                run = false;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            var links = webBrowser1.Document.GetElementsByTagName("li");
            run = false;
            timer3.Stop();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {  
           run = false;
           //MessageBox.Show(run.ToString());
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            label2.Text = (temp++).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                for(int i =0; i<dataGridView1.Rows.Count;i++)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
                Thread tr = new Thread(() => Execute());
                thread_pool.Add(tr);
                tr.Start();
            }
            catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

       private void Execute()
        {
            try
            {
                i = 0;
                while(dataGridView1.Rows.Count>i)
                {

                    //if (wbvisible == 1)
                    //{
                    //    Invoke(new Action(() => webBrowser2.Refresh()));
                    //    Invoke(new Action(() => webBrowser2.Visible = false));
                    //    Invoke(new Action(() => webBrowser1.Visible = true));
                    //    share(webBrowser1);
                    //}

                    //else
                    //{
                    //    Invoke(new Action(() => webBrowser1.Refresh()));
                    //    Invoke(new Action(() => webBrowser1.Visible = false));
                    //    Invoke(new Action(() => webBrowser2.Visible = true));
                    //    share(webBrowser2);
                    //}
                    share(webBrowser1);
                    i++;
                    Invoke(new Action(() => webBrowser1.Refresh()));
                    z = 0;
                    run = true;
                    while (run)
                    {
                        Invoke(new Action(() => label2.Text = z.ToString()));
                        z++;
                    }

                    Invoke(new Action(() => label2.Text = "resizing"));
                    IntPtr pt = GetCurrentProcess();
                    SetProcessWorkingSetSize(pt, -1, -1);

                    Invoke(new Action(() => label2.Text = "resized"));
                }


                for (int x = 0; x < thread_pool.Count; x++)
                {
                    MessageBox.Show(thread_pool[x].IsAlive.ToString()+"::"+thread_pool[x].Name);
                }
               
            }
           catch(Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

       private void button4_Click(object sender, EventArgs e)
       {
           try
           {
               if (string.Equals("Select All", button4.Text))
               {
                   for (int i = 0; i < checkedListBox1.Items.Count; i++)
                   {
                       checkedListBox1.SetItemChecked(i, true);
                   }
                   button4.Text = "Deselect All";
               }
               else if(string.Equals("Deselect All", button4.Text))
               {
                   for(int i =0; i<checkedListBox1.Items.Count;i++)
                   {
                       checkedListBox1.SetItemChecked(i, false);
                   }
                   button4.Text = "Select All";
               }
           }
           catch(Exception e1)
           {

           }
       }

       private void preview_FormClosing(object sender, FormClosingEventArgs e)
       {
           bg.Dispose();
           foreach (Thread tr in thread_pool)
               tr.Abort();
       }

       private void button5_Click(object sender, EventArgs e)
       {
           share(web);
       }

       private void webBrowser1_Validating(object sender, CancelEventArgs e)
       {
           label4.Text = "Validating";
       }

       private void webBrowser1_Validated(object sender, EventArgs e)
       {
           label4.Text = "Validated";
       }



       public void share(WebBrowser wb)
       {

           try
           {
               if (link0 == 0)
                   Invoke(new Action(() => links0 = wb.Document.GetElementsByTagName("a")));
               link0 = 1;
           //cmnd = "a";
           shr1: if (shrset == 0)
               {
                   //  Invoke(new Action(() => timer1.Start()));
                   //Thread.Sleep(1000);
                   Invoke(new Action(() => wb.Focus()));
                   // Invoke(new Action(() => richTextBox1.Text += "clicking share button" + Environment.NewLine));
                   foreach (HtmlElement link in links0)
                   {
                       string l = link.GetAttribute("className");
                       if (!string.IsNullOrEmpty(l))
                       {

                           if (l.Equals("share_action_link"))
                           {
                               link.Focus();
                               Thread.Sleep(300);
                               System.Windows.Forms.SendKeys.SendWait("{Enter}");
                               // Invoke(new Action(() => richTextBox1.Text += "Share button" + Environment.NewLine));
                               shrset = 1;
                               break;
                           }
                       }
                   }
               }
               cmnd = "span";
               if (link1 == 0)
                   Invoke(new Action(() => links1 = wb.Document.GetElementsByTagName("span")));
               link1 = 1;
               // Invoke(new Action(() => timer1.Start()));
               //  Invoke(new Action(() => richTextBox1.Text += "clicking Share" + Environment.NewLine));
               //run = true;
               //while (run)
               //{

               //}

               foreach (HtmlElement link in links1)
               {
                   if (!string.IsNullOrEmpty(link.InnerText) && link.InnerText.Equals("Share…"))
                   {
                       link.InvokeMember("Click");
                       // Invoke(new Action(() => richTextBox1.Text += "clicked Share" + Environment.NewLine));
                       done = 1;
                       break;
                   }

               }
               if (done == 0)
                   goto shr1;
               else
                   done = 0;


           shr2:
               //  run = true;
               Thread.Sleep(5000);
               if (link2 == 0)
                   Invoke(new Action(() => links2 = wb.Document.GetElementsByTagName("span")));
               link2 = 1;
               // Invoke(new Action(() => timer1.Start()));
               Thread.Sleep(500);
               //while (run)
               //{

               //}
               // Invoke(new Action(() => richTextBox1.Text += "clicking Timeline button" + Environment.NewLine));
               foreach (HtmlElement link in links2)
               {

                   if (!string.IsNullOrEmpty(link.InnerText))
                   {
                       if (string.Equals(link.InnerText, "On your own Timeline") && link.TabIndex == 0)
                       {

                           System.Windows.Forms.SendKeys.SendWait("+{Tab}");
                           Thread.Sleep(200);
                           System.Windows.Forms.SendKeys.SendWait("{Enter}");
                           Thread.Sleep(200);
                           System.Windows.Forms.SendKeys.SendWait("{Down}");
                           Thread.Sleep(200);
                           System.Windows.Forms.SendKeys.SendWait("{Down}");
                           Thread.Sleep(200);
                           System.Windows.Forms.SendKeys.SendWait("{Enter}");
                           Thread.Sleep(200);
                           done = 1;
                           //  Invoke(new Action(() => richTextBox1.Text += "Clicked Timeline button  " + link.TabIndex + "::" + link.TagName + Environment.NewLine));
                           break;

                       }
                   }
               }

               if (done == 0)
                   goto shr2;
               else
                   done = 0;
               run = true;


               // Invoke(new Action(() => label1.Text = "33"));
               //Invoke(new Action(() => timer1.Start()));
               //run = true;
               //while (run)
               //{

               //}
               //Invoke(new Action(() => richTextBox1.Text += "Clicking Group" + Environment.NewLine));
               //foreach (HtmlElement link in links)
               //{
               //    if (!string.IsNullOrEmpty(link.InnerText))
               //    {
               //        if (link.InnerText.Equals("In a group"))
               //        {
               //            Invoke(new Action(() => wb.Focus()));
               //            link.InvokeMember("Click");
               //            Invoke(new Action(() => richTextBox1.Text += "clicked group" + Environment.NewLine));
               //            break;
               //        }
               //    }
               //}
               //Invoke(new Action(() => label1.Text = "44"));

               run = true;

               Invoke(new Action(() => InvokeOnClick(button2, EventArgs.Empty)));

               //imp_code(i);

               while (run)
               {

               }

               Invoke(new Action(() => label1.Text = "5"));
           shr3:
               run = true;
               if (link3 == 0)
                   Invoke(new Action(() => links3 = wb.Document.GetElementsByTagName("li")));
               link3 = 1;
               //Invoke(new Action(() => timer3.Start()));
               //while (run)
               //{

               //}
               Thread.Sleep(1000);
               //  Invoke(new Action(() => wb.Focus()));
               //  Invoke(new Action(() => richTextBox1.Text += "Clicking selected group" + Environment.NewLine));
               foreach (HtmlElement link in links3)
               {
                   string l = link.GetAttribute("title");
                   if (!string.IsNullOrEmpty(l))
                   {

                       if (string.Equals(l, dataGridView1.Rows[i].Cells[0].Value.ToString()))
                       {
                           System.Windows.Forms.SendKeys.SendWait("{Enter}");

                           Thread.Sleep(300);
                           System.Windows.Forms.SendKeys.SendWait("{Enter}");
                           shared = true;
                           done = 1;

                           Thread.Sleep(3500);
                           break;
                       }
                       else
                       {
                           System.Windows.Forms.SendKeys.SendWait("{Down}");
                       }
                   }
               }

               if (done == 0)
               {

                   goto shr3;

               }
               else
                   done = 0;

               run = true;

               if (shared)
               {
                   shared = false;
                   Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen));
                   Invoke(new Action(() => dataGridView1.Rows[i].Cells[1].Value = "Post Shared"));
                   // Invoke(new Action(() => richTextBox1.Text += "Shared" + Environment.NewLine));
               }
               else
               {
                   Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White));
                   Invoke(new Action(() => dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Brown));
                   Invoke(new Action(() => dataGridView1.Rows[i].Cells[1].Value = "Issue detected"));
               }


               // Invoke(new Action(() => richTextBox1.Text += ""));
               if (wbvisible == 1)
                   wbvisible = 2;
               else
                   wbvisible = 1;


           }
           catch (Exception e1)
           {
               MessageBox.Show(e1.ToString());

               run = true;


           }
       }
       

    }
}
