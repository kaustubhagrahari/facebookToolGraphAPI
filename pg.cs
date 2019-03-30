using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using Facebook;

namespace FB_WFA
{
    public partial class pg : Form
    {
        List<String> a = new List<string>();
        List<String> b = new List<string>();
        List<String> c = new List<string>();
        List<String> a1 = new List<string>();
        DataTable dt = new DataTable();
        DataTable dttemp = new DataTable();
        DataRow row;
        string dir = string.Empty;
        int multi = 0;
        string found = "--";
        string cur = "--";
        string cur1 = "--";
        FacebookClient fb = new FacebookClient();
        string token = "";


        public pg(string token)
        {
            InitializeComponent();
            dt.Columns.Add("Cat", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            this.token = token;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cur1 = "Connecting to Internet";
                cur = "Connecting to server";
                Thread tr = new Thread(() => catsearch());
                tr.Start();
                
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void catsearch()
        {
            try
            {
                
                if (!string.IsNullOrEmpty(textBox1.Text))
                {

                    Thread.Sleep(1000);
                    this.Invoke(new Action(() => dataGridView1.Rows.Clear()));
                    found = "---";
                    cur = "Searching in Facebook Database";
                    var fbclient = new Facebook.FacebookClient(token);

                    string result = (fbclient.Get("/search?q=" + textBox1.Text + "&type=page")).ToString();
                    fbresponse.Rootobject jobj = (fbresponse.Rootobject)JsonConvert.DeserializeObject<fbresponse.Rootobject>(result);

                    a.Clear();
                    b.Clear();
                    this.Invoke(new Action(() => listBox1.Items.Clear()));
                    this.Invoke(new Action(() => listBox2.Items.Clear()));
                    foreach (var data in jobj.data)
                    {
                        a.Add(data.category);
                        b.Add(data.id);

                    }
                    a1 = new HashSet<String>(a).ToList<String>();
                    a1.Sort();
                    this.Invoke(new Action(() => comboBox1.Items.Clear()));
                    if (a1.Count > 0)
                    {
                        for (int i = 0; i < a1.Count; i++)
                        {
                            this.Invoke(new Action(() => comboBox1.Items.Add(a1[i])));
                        }
                        this.Invoke(new Action(() => comboBox1.SelectedIndex = 0));
                        if (dt.Rows.Count > 0)
                            dt.Rows.Clear();

                        createtable();
                        for (int i = 0; i < a1.Count; i++)
                        {
                            this.Invoke(new Action(() => listBox1.Items.Add(a1[i])));
                        }
                        Thread.Sleep(3000);
                    }
                    cur = "Search Completed Successfully";
                    cur1 = "Disconnected from Internet";
                    this.Invoke(new Action(() => groupBox2.Enabled = true));
                    this.Invoke(new Action(() => label4.Text = a1.Count.ToString()));
                }
                else
                {

                    MessageBox.Show("Kindly enter the text ");
                    cur1 = "Disconnected From Internet";
                    cur = "Disconnected From Server";
                }
            }
            catch (Exception e1)
            {
                if (e1.ToString().Contains("The remote name could not be resolved"))
                    MessageBox.Show("We have found that your internet is not working. Kindly check and Try Again");
                else
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label11.Text = cur;
            label12.Text = cur1;
            label5.Text = found;
            if (listBox2.Items.Count > 0)
            {
                button4.Enabled = true;
            }
            else
                button4.Enabled = false;
            if (radioButton1.Checked == true)
            {
                groupBox4.Enabled = true;
                groupBox5.Enabled = false;
                groupBox7.Enabled = false;
            }
            if (radioButton2.Checked == true)
            {
                groupBox4.Enabled = false;
                groupBox5.Enabled = true;
                groupBox7.Enabled = false;
            }
            if (radioButton3.Checked == true)
            {
                groupBox7.Enabled = true;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
            }
        }
        private void createtable()
        {
            for (int i = 0; i < a.Count; i++)
            {
                dt.Rows.Add(a[i], b[i]);
            }
        }

        private void pg_Load(object sender, EventArgs e)
        {
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedItem) != null)
            {
                string selected = comboBox1.SelectedItem.ToString();
                Thread tr = new Thread(() => data(selected));
                tr.Start();
            }
        }

        private void data(string selected)
        {

            try
            {
                this.Invoke(new Action(() => checkBox1.Checked = false));
                this.Invoke(new Action(() => checkBox1.Enabled = false));
                cur1 = "Connecting to Internet";
                found = "--";
                cur = "Connecting to Server";
                multi = 0;
                Thread.Sleep(1000);
                var rows = from row in dt.AsEnumerable() where row.Field<string>("Cat") == selected select row;
                DataRow[] id = rows.ToArray();

                // DataRow[] id = dt.Select("id where Cat = '" + comboBox1.SelectedItem.ToString()+"'");
                this.Invoke(new Action(() => dataGridView1.Rows.Clear()));
                foreach (var row in id)
                {

                    try
                    {
                        this.Invoke(new Action(() => dataGridView1.Rows.Add()));
                        var fbclient = new Facebook.FacebookClient(token);
                        cur1 = "Connected to Internet";
                        cur = "Connected to Server";
                        string resp = fbclient.Get(row["ID"].ToString()).ToString();
                        fbjsonobjects.Rootobject obj = JsonConvert.DeserializeObject<fbjsonobjects.Rootobject>(resp);
                        found = (multi + 1).ToString();

                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Category"].Value = selected));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["ID"].Value = obj.id));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["PName"].Value = obj.name));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["About"].Value = obj.about));

                        if (!string.IsNullOrEmpty(obj.phone))
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Contact"].Value = obj.phone));
                        if (!string.IsNullOrEmpty(obj.description))
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Desc"].Value = obj.description));
                        if (obj.location != null)
                        {
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Location"].Value = obj.location.country));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Address"].Value = obj.location.city + ", " + obj.location.street + ", " + obj.location.zip));
                        }
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Likes"].Value = obj.likes));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["TAbout"].Value = obj.talking_about_count));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["WHCount"].Value = obj.were_here_count));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Link"].Value = obj.link));
                        if (!string.IsNullOrEmpty(obj.website))
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Website"].Value = obj.website));
                        if (!string.IsNullOrEmpty(obj.awards))
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Awards"].Value = obj.awards));

                        if (!string.IsNullOrEmpty(obj.founded))
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Founded"].Value = obj.founded));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["IsCommunity"].Value = obj.is_community_page));
                        this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Published"].Value = obj.is_published));

                        if (obj.location != null)
                            if (obj.location.country != null)
                                c.Add(obj.location.country);

                        multi++;
                    }
                    catch (System.ArgumentException e1)
                    {
                        MessageBox.Show(e1.ToString());
                    }
                }
                cur = "Search Completed Successfully";
                cur1 = "Disconnected from Internet";
                dttemp.Columns.Clear();
                dttemp.Rows.Clear();
                this.Invoke(new Action(() => comboBox2.Items.Clear()));
                for (int x = 0; x < dataGridView1.Columns.Count; x++)
                {
                    dttemp.Columns.Add(dataGridView1.Columns[x].Name);
                }

                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    dttemp.Rows.Add();
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        dttemp.Rows[x][j] = dataGridView1.Rows[x].Cells[j].Value;
                    }
                }

                List<string> a = new List<string>();
                a = new HashSet<string>(c).ToList<string>();
                a.Sort();
                if (a.Count > 0)
                    foreach (var item1 in a)
                        this.Invoke(new Action(() => comboBox2.Items.Add(item1)));
                this.Invoke(new Action(() => comboBox2.Items.Add("ALL")));
                this.Invoke(new Action(() => checkBox1.Enabled = true));
            }
            catch (Exception e1)
            {

                if (e1.ToString().Contains("The remote name could not be resolved"))
                    MessageBox.Show("We have found that your internet is not working. Kindly check and Try Again");
                else
                    MessageBox.Show(e1.ToString());
                cur = "Search Completed Successfully";
                cur1 = "Disconnected from Internet";
            }
        }
        private void _data(List<string> select)
        {

            try
            {
                this.Invoke(new Action(() => checkBox1.Checked = false));
                this.Invoke(new Action(() => checkBox1.Enabled = false));
                cur1 = "Connecting to Internet";
                cur = "Connecting to Server";
                c = new List<string>();
                c.Clear();
                dttemp.Rows.Clear();
                dttemp.Columns.Clear();
                multi = 0;
                foreach (var item in select)
                {
                    string selected = item.ToString();
                    var rows = from row in dt.AsEnumerable() where row.Field<string>("Cat") == selected select row;
                    DataRow[] id = rows.ToArray();
                    Thread.Sleep(1000);

                    foreach (var row in id)
                    {
                        try
                        {
                            this.Invoke(new Action(() => dataGridView1.Rows.Add()));
                            var fbclient = new Facebook.FacebookClient(token);
                            cur1 = "Connected to Internet";
                            cur = "Connected to Server";
                            string resp = fbclient.Get(row["ID"].ToString()).ToString();
                            fbjsonobjects.Rootobject obj = JsonConvert.DeserializeObject<fbjsonobjects.Rootobject>(resp);
                            found = (multi + 1).ToString();

                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Category"].Value = selected));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["ID"].Value = obj.id));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["PName"].Value = obj.name));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["About"].Value = obj.about));

                            if (!string.IsNullOrEmpty(obj.phone))
                                this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Contact"].Value = obj.phone));
                            if (!string.IsNullOrEmpty(obj.description))
                                this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Desc"].Value = obj.description));
                            if (obj.location != null)
                            {
                                this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Location"].Value = obj.location.country));
                                this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Address"].Value = obj.location.city + ", " + obj.location.street + ", " + obj.location.zip));
                            }
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Likes"].Value = obj.likes));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["TAbout"].Value = obj.talking_about_count));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["WHCount"].Value = obj.were_here_count));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Link"].Value = obj.link));
                            if (!string.IsNullOrEmpty(obj.website))
                                this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Website"].Value = obj.website));
                            if (!string.IsNullOrEmpty(obj.awards))
                                this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Awards"].Value = obj.awards));

                            if (!string.IsNullOrEmpty(obj.founded))
                                this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Founded"].Value = obj.founded));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["IsCommunity"].Value = obj.is_community_page));
                            this.Invoke(new Action(() => dataGridView1.Rows[multi].Cells["Published"].Value = obj.is_published));

                            if (obj.location != null)
                                if (obj.location.country != null)
                                    c.Add(obj.location.country);

                            multi++;
                        }
                        catch (System.ArgumentException e1)
                        {
                            MessageBox.Show(e1.ToString());
                        }
                    }

                }
                cur = "Search Completed Successfully";
                cur1 = "Disconnected from Internet";
                dttemp.Columns.Clear();
                dttemp.Rows.Clear();
                this.Invoke(new Action(() => comboBox2.Items.Clear()));
                for (int x = 0; x < dataGridView1.Columns.Count; x++)
                {
                    dttemp.Columns.Add(dataGridView1.Columns[x].Name);
                }

                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    dttemp.Rows.Add();
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        dttemp.Rows[x][j] = dataGridView1.Rows[x].Cells[j].Value;
                    }
                }

                List<string> a = new List<string>();
                a = new HashSet<string>(c).ToList<string>();
                a.Sort();
                if (a.Count > 0)
                    foreach (var item1 in a)
                        this.Invoke(new Action(() => comboBox2.Items.Add(item1)));
                this.Invoke(new Action(() => comboBox2.Items.Add("ALL")));
                this.Invoke(new Action(() => checkBox1.Enabled = true));
            }
            catch (System.ArgumentException e1)
            {

                MessageBox.Show(e1.ToString());
                cur = "Search Completed Successfully";
                cur1 = "Disconnected from Internet";
            }
            catch (Exception e1)
            {
                if (e1.ToString().Contains("The remote name could not be resolved"))
                    MessageBox.Show("We have found that your internet is not working. Kindly check and Try Again");
                else
                    MessageBox.Show(e1.ToString());
                cur = "Search Completed Successfully";
                cur1 = "Disconnected from Internet";
            }

        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                multi = 0;
                dataGridView1.Rows.Clear();
                found = "--";
                Thread tr = new Thread(() => _data(a1));
                tr.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItems.Count > 0)
                {
                    List<string> obj = new List<string>();

                    for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                        obj.Add(listBox1.SelectedItems[i].ToString());
                    for (int i = 0; i < obj.Count; i++)
                        listBox1.Items.Remove(obj[i].ToString());
                    for (int i = 0; i < listBox2.Items.Count; i++)
                        obj.Add(listBox2.Items[i].ToString());

                    listBox2.Items.Clear();

                    obj.Sort();
                    for (int i = 0; i < obj.Count; i++)
                    {
                        listBox2.Items.Add(obj[i].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Select atleast one Category to move");
                }
                button2.Enabled = false;
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
                if (listBox2.SelectedItems.Count > 0)
                {
                    List<string> obj = new List<string>();

                    for (int i = 0; i < listBox2.SelectedItems.Count; i++)
                        obj.Add(listBox2.SelectedItems[i].ToString());
                    for (int i = 0; i < obj.Count; i++)
                        listBox2.Items.Remove(obj[i].ToString());
                    for (int i = 0; i < listBox1.Items.Count; i++)
                        obj.Add(listBox1.Items[i].ToString());

                    listBox1.Items.Clear();

                    obj.Sort();
                    for (int i = 0; i < obj.Count; i++)
                    {
                        listBox1.Items.Add(obj[i].ToString());
                    }
                    button3.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Select atleast one Category to move");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                multi = 0;
                List<string> temp = new List<string>();
                dataGridView1.Rows.Clear();
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    temp.Add(listBox2.Items[i].ToString());
                }

                Thread tr = new Thread(() => _data(temp));
                tr.Start();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] row;
                dataGridView1.Rows.Clear();
                String country = comboBox2.SelectedItem.ToString();

                if (string.Compare(country, "ALL") == 0)
                    row = dttemp.Select();
                else
                    row = dttemp.Select("Location = '" + country + "'");

                for (int x = 0; x < row.Length; x++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < dttemp.Columns.Count; j++)
                    {
                        dataGridView1.Rows[x].Cells[j].Value = row[x][j];
                    }
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox2.Enabled = true;
                button7.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                button7.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                object misValue = System.Reflection.Missing.Value;
                int row = 0;
                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    xlWorkSheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (row = 0; row < dataGridView1.RowCount; row++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        DataGridViewCell cell = dataGridView1[j, row];
                        xlWorkSheet.Cells[row + 2, j + 1] = cell.Value;
                    }
                }

                createdirectory();
                xlWorkBook.SaveAs(dir + @"\fbdata" + "-.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Saved = true;
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();



                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MessageBox.Show("Excel file created , you can find the file in FB-Extracted-Data folder in MyDocuments folder!!!", "Files Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e1) { MessageBox.Show(e1.ToString()); }
        }
        private void createdirectory()
        {
            try
            {
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FB-Extracted-Data");
                if (Directory.Exists(dir))
                {

                }
                else
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count > 0)
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }
    }
}
