namespace ABE_Review_Extractor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LBL_CountofTag = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.CB_TP = new System.Windows.Forms.CheckBox();
            this.CB_local = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LBL_Pattern = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LBL_Time = new System.Windows.Forms.Label();
            this.CB_UsePattern = new System.Windows.Forms.CheckBox();
            this.LBL_MainPattern = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_Outer = new System.Windows.Forms.RadioButton();
            this.RB_Inner = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.CB_Encoding = new System.Windows.Forms.ComboBox();
            this.BTN_Tikla = new System.Windows.Forms.Button();
            this.TXT_WebPage = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.CB_local);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.CB_Encoding);
            this.panel1.Controls.Add(this.BTN_Tikla);
            this.panel1.Controls.Add(this.TXT_WebPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1139, 253);
            this.panel1.TabIndex = 3;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LBL_CountofTag);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.CB_TP);
            this.groupBox6.Location = new System.Drawing.Point(469, 45);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(257, 79);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Visible = false;
            // 
            // LBL_CountofTag
            // 
            this.LBL_CountofTag.AutoSize = true;
            this.LBL_CountofTag.Location = new System.Drawing.Point(7, 42);
            this.LBL_CountofTag.Name = "LBL_CountofTag";
            this.LBL_CountofTag.Size = new System.Drawing.Size(56, 18);
            this.LBL_CountofTag.TabIndex = 3;
            this.LBL_CountofTag.Text = "label1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(65, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 22);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save EE Results";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save DOM Results";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CB_TP
            // 
            this.CB_TP.AutoSize = true;
            this.CB_TP.Checked = true;
            this.CB_TP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_TP.Location = new System.Drawing.Point(7, 14);
            this.CB_TP.Name = "CB_TP";
            this.CB_TP.Size = new System.Drawing.Size(35, 22);
            this.CB_TP.TabIndex = 0;
            this.CB_TP.Text = "T";
            this.CB_TP.UseVisualStyleBackColor = true;
            // 
            // CB_local
            // 
            this.CB_local.AutoSize = true;
            this.CB_local.Location = new System.Drawing.Point(744, 54);
            this.CB_local.Name = "CB_local";
            this.CB_local.Size = new System.Drawing.Size(67, 22);
            this.CB_local.TabIndex = 19;
            this.CB_local.Text = "Local";
            this.CB_local.UseVisualStyleBackColor = true;
            this.CB_local.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(744, 77);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(381, 166);
            this.listBox1.TabIndex = 18;
            this.listBox1.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LBL_Pattern);
            this.groupBox3.Location = new System.Drawing.Point(16, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(710, 45);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pattern for selected Tag";
            this.groupBox3.Visible = false;
            // 
            // LBL_Pattern
            // 
            this.LBL_Pattern.AutoSize = true;
            this.LBL_Pattern.Location = new System.Drawing.Point(6, 21);
            this.LBL_Pattern.Name = "LBL_Pattern";
            this.LBL_Pattern.Size = new System.Drawing.Size(64, 18);
            this.LBL_Pattern.TabIndex = 1;
            this.LBL_Pattern.Text = "Pattern";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LBL_Time);
            this.groupBox2.Controls.Add(this.CB_UsePattern);
            this.groupBox2.Controls.Add(this.LBL_MainPattern);
            this.groupBox2.Location = new System.Drawing.Point(16, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(710, 65);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Use this Pattern for Efficient Extraction (Only same web domain)";
            this.groupBox2.Visible = false;
            // 
            // LBL_Time
            // 
            this.LBL_Time.AutoSize = true;
            this.LBL_Time.Location = new System.Drawing.Point(6, 38);
            this.LBL_Time.Name = "LBL_Time";
            this.LBL_Time.Size = new System.Drawing.Size(32, 18);
            this.LBL_Time.TabIndex = 2;
            this.LBL_Time.Text = "...";
            // 
            // CB_UsePattern
            // 
            this.CB_UsePattern.AutoSize = true;
            this.CB_UsePattern.Location = new System.Drawing.Point(7, 21);
            this.CB_UsePattern.Name = "CB_UsePattern";
            this.CB_UsePattern.Size = new System.Drawing.Size(15, 14);
            this.CB_UsePattern.TabIndex = 1;
            this.CB_UsePattern.UseVisualStyleBackColor = true;
            // 
            // LBL_MainPattern
            // 
            this.LBL_MainPattern.AutoSize = true;
            this.LBL_MainPattern.Location = new System.Drawing.Point(28, 18);
            this.LBL_MainPattern.Name = "LBL_MainPattern";
            this.LBL_MainPattern.Size = new System.Drawing.Size(144, 18);
            this.LBL_MainPattern.TabIndex = 0;
            this.LBL_MainPattern.Text = "Main Pattern Text";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_Outer);
            this.groupBox1.Controls.Add(this.RB_Inner);
            this.groupBox1.Location = new System.Drawing.Point(16, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 46);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // RB_Outer
            // 
            this.RB_Outer.AutoSize = true;
            this.RB_Outer.Location = new System.Drawing.Point(169, 16);
            this.RB_Outer.Name = "RB_Outer";
            this.RB_Outer.Size = new System.Drawing.Size(154, 22);
            this.RB_Outer.TabIndex = 1;
            this.RB_Outer.Text = "Outer Layout Tag";
            this.RB_Outer.UseVisualStyleBackColor = true;
            // 
            // RB_Inner
            // 
            this.RB_Inner.AutoSize = true;
            this.RB_Inner.Checked = true;
            this.RB_Inner.Location = new System.Drawing.Point(7, 16);
            this.RB_Inner.Name = "RB_Inner";
            this.RB_Inner.Size = new System.Drawing.Size(154, 22);
            this.RB_Inner.TabIndex = 0;
            this.RB_Inner.TabStop = true;
            this.RB_Inner.Text = "Inner Layout Tag";
            this.RB_Inner.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(885, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Encoding";
            // 
            // CB_Encoding
            // 
            this.CB_Encoding.FormattingEnabled = true;
            this.CB_Encoding.Items.AddRange(new object[] {
            "Default",
            "UTF8"});
            this.CB_Encoding.Location = new System.Drawing.Point(965, 45);
            this.CB_Encoding.Margin = new System.Windows.Forms.Padding(4);
            this.CB_Encoding.Name = "CB_Encoding";
            this.CB_Encoding.Size = new System.Drawing.Size(160, 26);
            this.CB_Encoding.TabIndex = 12;
            this.CB_Encoding.Text = "Default";
            // 
            // BTN_Tikla
            // 
            this.BTN_Tikla.Location = new System.Drawing.Point(16, 45);
            this.BTN_Tikla.Margin = new System.Windows.Forms.Padding(4);
            this.BTN_Tikla.Name = "BTN_Tikla";
            this.BTN_Tikla.Size = new System.Drawing.Size(204, 32);
            this.BTN_Tikla.TabIndex = 5;
            this.BTN_Tikla.Text = "Load and Extract";
            this.BTN_Tikla.UseVisualStyleBackColor = true;
            this.BTN_Tikla.Click += new System.EventHandler(this.BTN_Tikla_Click);
            // 
            // TXT_WebPage
            // 
            this.TXT_WebPage.Location = new System.Drawing.Point(16, 12);
            this.TXT_WebPage.Margin = new System.Windows.Forms.Padding(4);
            this.TXT_WebPage.Name = "TXT_WebPage";
            this.TXT_WebPage.Size = new System.Drawing.Size(1109, 25);
            this.TXT_WebPage.TabIndex = 3;
            this.TXT_WebPage.Text = "http://blogs.technet.com/b/itprotr/default.aspx?PageIndex=4";
            this.TXT_WebPage.TextChanged += new System.EventHandler(this.TXT_WebPage_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 253);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1139, 228);
            this.splitContainer1.SplitterDistance = 506;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(504, 226);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer2.Size = new System.Drawing.Size(626, 226);
            this.splitContainer2.SplitterDistance = 140;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.webBrowser1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(626, 140);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selected Pattern Web Views";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 21);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(620, 116);
            this.webBrowser1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.webBrowser2);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(626, 82);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Selected Pattern Extracted Views";
            // 
            // webBrowser2
            // 
            this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser2.Location = new System.Drawing.Point(3, 21);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.ScriptErrorsSuppressed = true;
            this.webBrowser2.Size = new System.Drawing.Size(620, 58);
            this.webBrowser2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 481);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Review Extraction 1.2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTN_Tikla;
        private System.Windows.Forms.TextBox TXT_WebPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CB_Encoding;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_Outer;
        private System.Windows.Forms.RadioButton RB_Inner;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label LBL_Pattern;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox CB_UsePattern;
        private System.Windows.Forms.Label LBL_MainPattern;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label LBL_Time;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox CB_local;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox CB_TP;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label LBL_CountofTag;

    }
}

