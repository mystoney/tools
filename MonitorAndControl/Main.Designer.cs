﻿
namespace MonitorAndControl
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.手动检测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.端口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动检测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加测试项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.发送邮件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.不朽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加密ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.访问linuxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getJokeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nPOIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DGMAIN = new MyContrals.ExDataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGMAIN)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.手动检测ToolStripMenuItem,
            this.自动检测ToolStripMenuItem,
            this.添加测试项目ToolStripMenuItem,
            this.测试ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1378, 28);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 手动检测ToolStripMenuItem
            // 
            this.手动检测ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务ToolStripMenuItem,
            this.端口ToolStripMenuItem,
            this.pingToolStripMenuItem});
            this.手动检测ToolStripMenuItem.Name = "手动检测ToolStripMenuItem";
            this.手动检测ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.手动检测ToolStripMenuItem.Text = "手动检测";
            this.手动检测ToolStripMenuItem.Click += new System.EventHandler(this.手动检测ToolStripMenuItem_Click);
            // 
            // 服务ToolStripMenuItem
            // 
            this.服务ToolStripMenuItem.Name = "服务ToolStripMenuItem";
            this.服务ToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.服务ToolStripMenuItem.Text = "服务";
            this.服务ToolStripMenuItem.Click += new System.EventHandler(this.服务ToolStripMenuItem_Click);
            // 
            // 端口ToolStripMenuItem
            // 
            this.端口ToolStripMenuItem.Name = "端口ToolStripMenuItem";
            this.端口ToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.端口ToolStripMenuItem.Text = "端口";
            this.端口ToolStripMenuItem.Click += new System.EventHandler(this.端口ToolStripMenuItem_Click);
            // 
            // pingToolStripMenuItem
            // 
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            this.pingToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.pingToolStripMenuItem.Text = "Ping";
            this.pingToolStripMenuItem.Click += new System.EventHandler(this.pingToolStripMenuItem_Click);
            // 
            // 自动检测ToolStripMenuItem
            // 
            this.自动检测ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.停止ToolStripMenuItem});
            this.自动检测ToolStripMenuItem.Name = "自动检测ToolStripMenuItem";
            this.自动检测ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.自动检测ToolStripMenuItem.Text = "自动检测";
            this.自动检测ToolStripMenuItem.Click += new System.EventHandler(this.自动检测ToolStripMenuItem_Click);
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.开始ToolStripMenuItem.Text = "开始";
            this.开始ToolStripMenuItem.Click += new System.EventHandler(this.开始ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.停止ToolStripMenuItem.Text = "停止";
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // 添加测试项目ToolStripMenuItem
            // 
            this.添加测试项目ToolStripMenuItem.Name = "添加测试项目ToolStripMenuItem";
            this.添加测试项目ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.添加测试项目ToolStripMenuItem.Text = "添加测试项目";
            this.添加测试项目ToolStripMenuItem.Click += new System.EventHandler(this.添加测试项目ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.发送邮件ToolStripMenuItem,
            this.新增ToolStripMenuItem,
            this.不朽ToolStripMenuItem,
            this.加密ToolStripMenuItem,
            this.访问linuxToolStripMenuItem,
            this.getJokeToolStripMenuItem,
            this.nPOIToolStripMenuItem});
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.测试ToolStripMenuItem.Text = "测试";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.toolStripMenuItem2.Text = "发微信";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // 发送邮件ToolStripMenuItem
            // 
            this.发送邮件ToolStripMenuItem.Name = "发送邮件ToolStripMenuItem";
            this.发送邮件ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.发送邮件ToolStripMenuItem.Text = "发送邮件";
            this.发送邮件ToolStripMenuItem.Click += new System.EventHandler(this.发送邮件ToolStripMenuItem_Click);
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.新增ToolStripMenuItem.Text = "新增服务";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.新增ToolStripMenuItem_Click);
            // 
            // 不朽ToolStripMenuItem
            // 
            this.不朽ToolStripMenuItem.Name = "不朽ToolStripMenuItem";
            this.不朽ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.不朽ToolStripMenuItem.Text = "不朽";
            this.不朽ToolStripMenuItem.Click += new System.EventHandler(this.不朽ToolStripMenuItem_Click);
            // 
            // 加密ToolStripMenuItem
            // 
            this.加密ToolStripMenuItem.Name = "加密ToolStripMenuItem";
            this.加密ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.加密ToolStripMenuItem.Text = "加密";
            this.加密ToolStripMenuItem.Click += new System.EventHandler(this.加密ToolStripMenuItem_Click);
            // 
            // 访问linuxToolStripMenuItem
            // 
            this.访问linuxToolStripMenuItem.Name = "访问linuxToolStripMenuItem";
            this.访问linuxToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.访问linuxToolStripMenuItem.Text = "访问linux";
            this.访问linuxToolStripMenuItem.Click += new System.EventHandler(this.访问linuxToolStripMenuItem_Click);
            // 
            // getJokeToolStripMenuItem
            // 
            this.getJokeToolStripMenuItem.Name = "getJokeToolStripMenuItem";
            this.getJokeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.getJokeToolStripMenuItem.Text = "GetJoke()";
            this.getJokeToolStripMenuItem.Click += new System.EventHandler(this.getJokeToolStripMenuItem_Click);
            // 
            // nPOIToolStripMenuItem
            // 
            this.nPOIToolStripMenuItem.Name = "nPOIToolStripMenuItem";
            this.nPOIToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.nPOIToolStripMenuItem.Text = "NPOI";
            this.nPOIToolStripMenuItem.Click += new System.EventHandler(this.nPOIToolStripMenuItem_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel2,
            this.Label1});
            this.StatusStrip.Location = new System.Drawing.Point(0, 720);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.StatusStrip.Size = new System.Drawing.Size(1378, 26);
            this.StatusStrip.TabIndex = 17;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(17, 20);
            this.ToolStripStatusLabel2.Text = "  ";
            // 
            // Label1
            // 
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(0, 20);
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DGMAIN);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1378, 692);
            this.splitContainer1.SplitterDistance = 458;
            this.splitContainer1.TabIndex = 18;
            // 
            // DGMAIN
            // 
            this.DGMAIN.AllowUserToAddRows = false;
            this.DGMAIN.AllowUserToDeleteRows = false;
            this.DGMAIN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGMAIN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGMAIN.Location = new System.Drawing.Point(0, 0);
            this.DGMAIN.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DGMAIN.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DGMAIN.MergeColumnNames")));
            this.DGMAIN.Name = "DGMAIN";
            this.DGMAIN.RowHeadersVisible = false;
            this.DGMAIN.RowHeadersWidth = 51;
            this.DGMAIN.RowTemplate.Height = 27;
            this.DGMAIN.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGMAIN.Size = new System.Drawing.Size(1378, 458);
            this.DGMAIN.TabIndex = 19;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1372, 223);
            this.textBox1.TabIndex = 0;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 746);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGMAIN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        internal System.Windows.Forms.StatusStrip StatusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel2;
        public System.Windows.Forms.ToolStripStatusLabel Label1;
        private System.Windows.Forms.ToolStripMenuItem 自动检测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 手动检测ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 端口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加测试项目ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyContrals.ExDataGridView DGMAIN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发送邮件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 不朽ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem 加密ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 访问linuxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getJokeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nPOIToolStripMenuItem;
    }
}