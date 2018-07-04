using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using Library_ABE.html;
using HtmlAgilityPack;
using System.IO;
using System.Diagnostics;

namespace ABE_Review_Extractor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Hashtable _ht_parent;
        HTMLMarkerClass.Review_Inf _ri;
        string html_doc = "";

        /*private void Download_Web_Pages(string url)
        {
            Encoding _encode = Encoding.Default;
            if (CB_Encoding.Text == "UTF8")
                _encode = Encoding.UTF8;

            HtmlAgilityPack.HtmlDocument doc01 = new HtmlAgilityPack.HtmlDocument();
            doc01.LoadHtml(downloader.GetHtmlContent(url, _encode));
        }*/

        private void BTN_Tikla_Click(object sender, EventArgs e)
        {
            Encoding _encode = Encoding.Default;
            if (CB_Encoding.Text == "UTF8")
                _encode = Encoding.UTF8;

            
            if(!CB_local.Checked)    
                html_doc = downloader.GetHtmlContent(TXT_WebPage.Text, _encode);
            else
            {
                string dosya = listBox1.SelectedItem.ToString().Split(':')[0].Trim();
                html_doc = File.ReadAllText(dosya);
            }
            LBL_Time.Text = "";

            if (html_doc != "Hata var!!!")
            {
                if (!CB_UsePattern.Checked)
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();

                    HTMLMarkerClass.DOM _dom = new HTMLMarkerClass.DOM();
                    _dom.prepareDOM(html_doc);
                    _ri = HTMLMarkerClass.desicionClass_Review.find_Review_Main_Tag(_dom._list);
                    stopWatch.Stop();
                    // Get the elapsed time as a TimeSpan value.
                    LBL_Time.Text = stopWatch.ElapsedMilliseconds.ToString() + " ms";
                    LBL_CountofTag.Text = _dom._list.Count.ToString();
                }
                else
                {
                    string pattern1 =  _ri.pub_tag;
                    pattern1 = pattern1.Replace(" ", ".").Replace("\"",".");
                    string pattern2 =  _ri.sub_tag;
                    pattern2 = pattern2.Replace(" ", ".").Replace("\"",".");
                    try
                    {
                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();
                        _ri.pub_outerhtml = HTMLMarkerClass.webfilter.Contents_of_givenLayout_Tags_TESTER(html_doc, pattern1, false)[0];
                        _ri.sub_outerhtml = HTMLMarkerClass.webfilter.Contents_of_givenLayout_Tags_TESTER(_ri.pub_outerhtml, pattern2, false)[0];
                        stopWatch.Stop();
                        // Get the elapsed time as a TimeSpan value.
                        LBL_Time.Text = stopWatch.ElapsedMilliseconds.ToString() + " ms";
                    }
                    catch
                    {
                        webBrowser1.DocumentText = "Error...<br /> Check your link of web page";
                        webBrowser2.DocumentText = "";
                        return;
                    }
                    
                }

                HtmlAgilityPack.HtmlDocument _dom2 = new HtmlAgilityPack.HtmlDocument();
                string islem = "all Reviews";
                if (RB_Inner.Checked)
                {
                    _dom2.LoadHtml(_ri.sub_outerhtml);
                    islem = "a Review";
                    webBrowser1.DocumentText = _ri.sub_outerhtml;
                    webBrowser2.DocumentText = "";
                }
                else
                {
                    _dom2.LoadHtml(_ri.pub_outerhtml);
                    webBrowser1.DocumentText = _ri.sub_outerhtml;
                    webBrowser2.DocumentText = "";
                }

                groupBox2.Visible = true;

                TreeNode root = new TreeNode(islem);
                root.Name = "-1";

                treeView1.Nodes.Clear();
                LBL_Pattern.Text = "";

                treeView1.Nodes.Add(root);
                element _e = new element();
                _e.innerHTML = html_doc;
                _e.tag = islem;
                _ht_parent = new Hashtable();
                _ht_parent.Add(root.Name, _e);
                LoadTree(root, _dom2.DocumentNode);
            }
            else
            {
                webBrowser1.DocumentText = "Error...<br /> Check your internet connection / link of the web page";
                groupBox2.Visible = false;
                groupBox3.Visible = false;
            }

            /*int counter = 1;
            string line;

            StreamReader file = new StreamReader("webpage.txt");
            while ((line = file.ReadLine()) != null)
            {
                string webpage = line.Substring(line.IndexOf(',')+1);
                Download_Web_Pages(webpage);
                string html_doc = doc01.DocumentNode.OuterHtml;
                string filename = counter.ToString();
                if(filename.Length==1)
                    filename = "00"+filename+".txt";
                else if (filename.Length==2)
                    filename = "0"+filename+".txt";
                else
                    filename = filename+".txt";


                StreamWriter file_w = new StreamWriter("TrainingDataset/" + filename, false);
                file_w.Write(html_doc);
                file_w.Close();
                counter++;
            }

            file.Close();*/
        }

        void LoadTree(TreeNode treeNode, HtmlAgilityPack.HtmlNode rootNode)
        {
            foreach (var node in rootNode.ChildNodes)
            {
                if (node.Name != "#text" && node.Name != "#comment")
                {
                    element _e = new element();
                    foreach (HtmlAgilityPack.HtmlAttribute _a in node.Attributes)
                    {
                        if (_e.attribute_names == null)
                            _e.attribute_names = _a.Name;
                        else
                            _e.attribute_names += "," + _a.Name;

                        if (_e.attribute_values == null)
                            _e.attribute_values = _a.Value;
                        else
                            _e.attribute_values += "," + _a.Value;
                    }

                    int start = node.OuterHtml.IndexOf("<");
                    int end = node.OuterHtml.IndexOf(">");
                    string temp = node.OuterHtml.Substring(start, end - start + 1);
                    _e.tag = Library_ABE.text.text_process.String_Decimal_Clear(temp);

                    _e.innerHTML = node.InnerHtml;
                    _e.innerText = node.InnerText;
                    _e.xpath = node.XPath;
                    _ht_parent.Add(node.StreamPosition.ToString(), _e);

                    TreeNode n = treeNode.Nodes.Add(_e.tag);
                    n.Name = node.StreamPosition.ToString();
                    LoadTree(n, node);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            element _e = (element)_ht_parent[treeView1.SelectedNode.Name];
            if (_e.tag != null)
            {
                if (treeView1.SelectedNode.Name == "-1")
                {
                    webBrowser1.DocumentText = _e.innerHTML;
                    webBrowser2.DocumentText = "";
                }
                else
                {
                    webBrowser1.DocumentText = _e.innerHTML;
                    groupBox3.Visible = true;
                    string pattern = _e.tag.Replace(" ", ".").Replace("\"", ".");
                    LBL_Pattern.Text = "RegEx Pattern: " + pattern;
                    try
                    {
                        string[] sonuclar = HTMLMarkerClass.webfilter.Contents_of_givenLayout_Tags_TESTER(html_doc, pattern, false);
                        string webpage2 = "<ol>";
                        foreach (string item in sonuclar)
                        {
                            webpage2 += "<li>" + item + "</li>";
                        }
                        webpage2 += "</ol>";
                        webBrowser2.DocumentText = webpage2;
                    }
                    catch (Exception)
                    {
                        webBrowser1.DocumentText = "Error...<br /> Check the pattern";
                        webBrowser2.DocumentText = "";
                    }
                }
            }
            else
            {
                webBrowser1.DocumentText = "Select a tag";
                webBrowser2.DocumentText = ""; 
            }
            
        }

        private void TXT_WebPage_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // string[] filePaths = Directory.GetFiles(@"TestingDataset\");
           

            /*listBox1.Items.Clear();
            foreach (string afile in filePaths)
            {
                FileInfo f = new FileInfo(afile);
                listBox1.Items.Add(afile + " : " + f.Length.ToString());
            }*/

           /* foreach (string afile in filePaths)
            {
                FileInfo f = new FileInfo(afile);
                StreamWriter file_w = new StreamWriter("TestingDataset_webpages.txt", true);
                file_w.WriteLine("TestingDataset\\" + afile + " : " + f.Length.ToString());
                file_w.Close();
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string[] filePaths = Directory.GetFiles(@"TestingDataset\");
            string cnt = (filePaths.Length + 1).ToString();
            if (cnt.Length == 1)
                cnt = "00" + cnt + ".txt";
            else if (cnt.Length == 2)
                cnt = "0" + cnt + ".txt";
            else
                cnt = cnt + ".txt";

            StreamWriter file_w2 = new StreamWriter(@"TestingDataset\"+cnt, false);
            file_w2.Write(html_doc);
            file_w2.Close();

            string website = TXT_WebPage.Text.Substring(0, TXT_WebPage.Text.IndexOf("/", 7));

            StreamWriter file_w3 = new StreamWriter("web_page2.txt", true);
            file_w3.WriteLine(website + "," + TXT_WebPage.Text);
            file_w3.Close();

            StreamWriter file_w = new StreamWriter("TestingDataset_resuts_DOM.txt", true);
            string t = "0";
            if (CB_TP.Checked)
                t = "1";
            file_w.WriteLine("TestingDataset\\" + cnt + ":" + t + ":" + LBL_Time.Text.Replace("ms", "").Trim() + ":" + LBL_CountofTag.Text);
            file_w.Close();*/

            /*for (int i = 0; i < listBox1.Items.Count; i++)
            {
                StreamWriter file_w = new StreamWriter("Testing_countof_tags.txt", true);
                try
                {
                    
                    string dosya = listBox1.Items[i].ToString().Split(':')[0].Trim();
                    html_doc = File.ReadAllText(dosya);
                    HTMLMarkerClass.DOM _dom = new HTMLMarkerClass.DOM();
                    _dom.prepareDOM(html_doc);

                    file_w.WriteLine(listBox1.Items[i].ToString() + ":" + _dom._list.Count.ToString());
                }
                catch
                {
                }

                file_w.Close();
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] filePaths = Directory.GetFiles(@"TestingDataset\");
            string cnt = (filePaths.Length).ToString();
            if (cnt.Length == 1)
                cnt = "00" + cnt + ".txt";
            else if (cnt.Length == 2)
                cnt = "0" + cnt + ".txt";
            else
                cnt = cnt + ".txt";

            StreamWriter file_w = new StreamWriter("TestingDataset_resuts_EE.txt", true);
            string t = "0";
            if (CB_TP.Checked)
                t = "1";

            file_w.WriteLine("TestingDataset\\" + cnt + ":" + t + ":" + LBL_Time.Text.Replace("ms", "").Trim());
            file_w.Close();
        }
    }
}
