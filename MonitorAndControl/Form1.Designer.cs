
namespace MonitorAndControl
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DGMAIN = new MyContrals.ExDataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.bt_ResumeService = new System.Windows.Forms.Button();
            this.bt_StopService = new System.Windows.Forms.Button();
            this.bt_StartService = new System.Windows.Forms.Button();
            this.bt_PauseService = new System.Windows.Forms.Button();
            this.combo_ServiceName_User = new System.Windows.Forms.ComboBox();
            this.combo_ServerIP = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_StartMode = new System.Windows.Forms.Label();
            this.lb_State = new System.Windows.Forms.Label();
            this.lb_DisplayName = new System.Windows.Forms.Label();
            this.lb_ServiceName = new System.Windows.Forms.Label();
            this.txb_StartMode = new System.Windows.Forms.TextBox();
            this.txb_State = new System.Windows.Forms.TextBox();
            this.txb_DisplayName = new System.Windows.Forms.TextBox();
            this.txb_ServiceName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.rbSocket = new System.Windows.Forms.RadioButton();
            this.rbTcpClient = new System.Windows.Forms.RadioButton();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.R_Service = new System.Windows.Forms.RadioButton();
            this.R_Port = new System.Windows.Forms.RadioButton();
            this.tb_AddPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_AddServiceName = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGMAIN)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(860, 536);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(852, 507);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "总览";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DGMAIN);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Size = new System.Drawing.Size(846, 501);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 0;
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
            this.DGMAIN.Size = new System.Drawing.Size(846, 444);
            this.DGMAIN.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(232, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 23);
            this.button4.TabIndex = 38;
            this.button4.Text = "暂停";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(107, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 37;
            this.button3.Text = "添加";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "开始";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.bt_ResumeService);
            this.tabPage1.Controls.Add(this.bt_StopService);
            this.tabPage1.Controls.Add(this.bt_StartService);
            this.tabPage1.Controls.Add(this.bt_PauseService);
            this.tabPage1.Controls.Add(this.combo_ServiceName_User);
            this.tabPage1.Controls.Add(this.combo_ServerIP);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lb_StartMode);
            this.tabPage1.Controls.Add(this.lb_State);
            this.tabPage1.Controls.Add(this.lb_DisplayName);
            this.tabPage1.Controls.Add(this.lb_ServiceName);
            this.tabPage1.Controls.Add(this.txb_StartMode);
            this.tabPage1.Controls.Add(this.txb_State);
            this.tabPage1.Controls.Add(this.txb_DisplayName);
            this.tabPage1.Controls.Add(this.txb_ServiceName);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(852, 507);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Windows服务";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(401, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(119, 23);
            this.button5.TabIndex = 36;
            this.button5.Text = "Ping";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // bt_ResumeService
            // 
            this.bt_ResumeService.Location = new System.Drawing.Point(290, 237);
            this.bt_ResumeService.Name = "bt_ResumeService";
            this.bt_ResumeService.Size = new System.Drawing.Size(75, 23);
            this.bt_ResumeService.TabIndex = 35;
            this.bt_ResumeService.Text = "恢复";
            this.bt_ResumeService.UseVisualStyleBackColor = true;
            // 
            // bt_StopService
            // 
            this.bt_StopService.Location = new System.Drawing.Point(371, 237);
            this.bt_StopService.Name = "bt_StopService";
            this.bt_StopService.Size = new System.Drawing.Size(75, 23);
            this.bt_StopService.TabIndex = 34;
            this.bt_StopService.Text = "停止";
            this.bt_StopService.UseVisualStyleBackColor = true;
            this.bt_StopService.Click += new System.EventHandler(this.bt_StopService_Click);
            // 
            // bt_StartService
            // 
            this.bt_StartService.Location = new System.Drawing.Point(128, 237);
            this.bt_StartService.Name = "bt_StartService";
            this.bt_StartService.Size = new System.Drawing.Size(75, 23);
            this.bt_StartService.TabIndex = 33;
            this.bt_StartService.Text = "运行";
            this.bt_StartService.UseVisualStyleBackColor = true;
            this.bt_StartService.Click += new System.EventHandler(this.button3_Click);
            // 
            // bt_PauseService
            // 
            this.bt_PauseService.Location = new System.Drawing.Point(209, 237);
            this.bt_PauseService.Name = "bt_PauseService";
            this.bt_PauseService.Size = new System.Drawing.Size(75, 23);
            this.bt_PauseService.TabIndex = 32;
            this.bt_PauseService.Text = "暂停";
            this.bt_PauseService.UseVisualStyleBackColor = true;
            // 
            // combo_ServiceName_User
            // 
            this.combo_ServiceName_User.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_ServiceName_User.FormattingEnabled = true;
            this.combo_ServiceName_User.Items.AddRange(new object[] {
            "SQLSERVERAGENT",
            "ABC"});
            this.combo_ServiceName_User.Location = new System.Drawing.Point(128, 41);
            this.combo_ServiceName_User.Name = "combo_ServiceName_User";
            this.combo_ServiceName_User.Size = new System.Drawing.Size(267, 23);
            this.combo_ServiceName_User.TabIndex = 31;
            // 
            // combo_ServerIP
            // 
            this.combo_ServerIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_ServerIP.FormattingEnabled = true;
            this.combo_ServerIP.Items.AddRange(new object[] {
            "172.16.1.15",
            "172.16.1.33",
            "172.16.1.35",
            "172.16.1.37",
            "172.16.1.77",
            "172.16.1.96"});
            this.combo_ServerIP.Location = new System.Drawing.Point(128, 16);
            this.combo_ServerIP.Name = "combo_ServerIP";
            this.combo_ServerIP.Size = new System.Drawing.Size(267, 23);
            this.combo_ServerIP.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "ServiceName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "ServerIP";
            // 
            // lb_StartMode
            // 
            this.lb_StartMode.AutoSize = true;
            this.lb_StartMode.Location = new System.Drawing.Point(21, 203);
            this.lb_StartMode.Name = "lb_StartMode";
            this.lb_StartMode.Size = new System.Drawing.Size(79, 15);
            this.lb_StartMode.TabIndex = 27;
            this.lb_StartMode.Text = "StartMode";
            // 
            // lb_State
            // 
            this.lb_State.AutoSize = true;
            this.lb_State.Location = new System.Drawing.Point(21, 179);
            this.lb_State.Name = "lb_State";
            this.lb_State.Size = new System.Drawing.Size(47, 15);
            this.lb_State.TabIndex = 26;
            this.lb_State.Text = "State";
            // 
            // lb_DisplayName
            // 
            this.lb_DisplayName.AutoSize = true;
            this.lb_DisplayName.Location = new System.Drawing.Point(21, 154);
            this.lb_DisplayName.Name = "lb_DisplayName";
            this.lb_DisplayName.Size = new System.Drawing.Size(95, 15);
            this.lb_DisplayName.TabIndex = 25;
            this.lb_DisplayName.Text = "DisplayName";
            // 
            // lb_ServiceName
            // 
            this.lb_ServiceName.AutoSize = true;
            this.lb_ServiceName.Location = new System.Drawing.Point(21, 129);
            this.lb_ServiceName.Name = "lb_ServiceName";
            this.lb_ServiceName.Size = new System.Drawing.Size(95, 15);
            this.lb_ServiceName.TabIndex = 24;
            this.lb_ServiceName.Text = "ServiceName";
            // 
            // txb_StartMode
            // 
            this.txb_StartMode.Location = new System.Drawing.Point(128, 200);
            this.txb_StartMode.Name = "txb_StartMode";
            this.txb_StartMode.ReadOnly = true;
            this.txb_StartMode.Size = new System.Drawing.Size(566, 25);
            this.txb_StartMode.TabIndex = 23;
            // 
            // txb_State
            // 
            this.txb_State.Location = new System.Drawing.Point(128, 175);
            this.txb_State.Name = "txb_State";
            this.txb_State.ReadOnly = true;
            this.txb_State.Size = new System.Drawing.Size(566, 25);
            this.txb_State.TabIndex = 22;
            // 
            // txb_DisplayName
            // 
            this.txb_DisplayName.Location = new System.Drawing.Point(128, 150);
            this.txb_DisplayName.Name = "txb_DisplayName";
            this.txb_DisplayName.ReadOnly = true;
            this.txb_DisplayName.Size = new System.Drawing.Size(566, 25);
            this.txb_DisplayName.TabIndex = 21;
            // 
            // txb_ServiceName
            // 
            this.txb_ServiceName.Location = new System.Drawing.Point(128, 126);
            this.txb_ServiceName.Name = "txb_ServiceName";
            this.txb_ServiceName.ReadOnly = true;
            this.txb_ServiceName.Size = new System.Drawing.Size(566, 25);
            this.txb_ServiceName.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "获取状态";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnQuit);
            this.tabPage2.Controls.Add(this.btnCheck);
            this.tabPage2.Controls.Add(this.rbSocket);
            this.tabPage2.Controls.Add(this.rbTcpClient);
            this.tabPage2.Controls.Add(this.txtPort);
            this.tabPage2.Controls.Add(this.txtIP);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(852, 507);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "端口";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(342, 92);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 12;
            this.btnQuit.Text = "退   出";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(25, 88);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 11;
            this.btnCheck.Text = "检   测";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click_1);
            // 
            // rbSocket
            // 
            this.rbSocket.AutoSize = true;
            this.rbSocket.Location = new System.Drawing.Point(293, 47);
            this.rbSocket.Name = "rbSocket";
            this.rbSocket.Size = new System.Drawing.Size(106, 19);
            this.rbSocket.TabIndex = 9;
            this.rbSocket.Text = "Socket检测";
            this.rbSocket.UseVisualStyleBackColor = true;
            this.rbSocket.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbTcpClient
            // 
            this.rbTcpClient.AutoSize = true;
            this.rbTcpClient.Location = new System.Drawing.Point(293, 23);
            this.rbTcpClient.Name = "rbTcpClient";
            this.rbTcpClient.Size = new System.Drawing.Size(130, 19);
            this.rbTcpClient.TabIndex = 10;
            this.rbTcpClient.Text = "TcpClient检测";
            this.rbTcpClient.UseVisualStyleBackColor = true;
            this.rbTcpClient.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(102, 48);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(158, 25);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "2121";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(102, 20);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(158, 25);
            this.txtIP.TabIndex = 8;
            this.txtIP.Text = "172.16.1.99";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "端 口 号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "计算机IP：";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.splitContainer2);
            this.tabPage4.Controls.Add(this.button6);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.tb_AddPort);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.cb_AddServiceName);
            this.tabPage4.Controls.Add(this.comboBox2);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(852, 507);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "添加监控项";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(112, 224);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(613, 252);
            this.splitContainer2.SplitterDistance = 126;
            this.splitContainer2.TabIndex = 42;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer3.Size = new System.Drawing.Size(613, 126);
            this.splitContainer3.SplitterDistance = 95;
            this.splitContainer3.TabIndex = 43;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(150, 195);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 41;
            this.button6.Text = "添加";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.R_Service);
            this.groupBox1.Controls.Add(this.R_Port);
            this.groupBox1.Location = new System.Drawing.Point(150, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 51);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // R_Service
            // 
            this.R_Service.AutoSize = true;
            this.R_Service.Checked = true;
            this.R_Service.Location = new System.Drawing.Point(6, 24);
            this.R_Service.Name = "R_Service";
            this.R_Service.Size = new System.Drawing.Size(58, 19);
            this.R_Service.TabIndex = 38;
            this.R_Service.TabStop = true;
            this.R_Service.Text = "服务";
            this.R_Service.UseVisualStyleBackColor = true;
            this.R_Service.CheckedChanged += new System.EventHandler(this.R_Service_CheckedChanged);
            // 
            // R_Port
            // 
            this.R_Port.AutoSize = true;
            this.R_Port.Location = new System.Drawing.Point(70, 24);
            this.R_Port.Name = "R_Port";
            this.R_Port.Size = new System.Drawing.Size(58, 19);
            this.R_Port.TabIndex = 39;
            this.R_Port.Text = "端口";
            this.R_Port.UseVisualStyleBackColor = true;
            this.R_Port.CheckedChanged += new System.EventHandler(this.R_Port_CheckedChanged);
            // 
            // tb_AddPort
            // 
            this.tb_AddPort.Location = new System.Drawing.Point(150, 155);
            this.tb_AddPort.Name = "tb_AddPort";
            this.tb_AddPort.Size = new System.Drawing.Size(267, 25);
            this.tb_AddPort.TabIndex = 37;
            this.tb_AddPort.Text = "2121";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 15);
            this.label7.TabIndex = 36;
            this.label7.Text = "端 口 号：";
            // 
            // cb_AddServiceName
            // 
            this.cb_AddServiceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_AddServiceName.FormattingEnabled = true;
            this.cb_AddServiceName.Items.AddRange(new object[] {
            "SQLSERVERAGENT"});
            this.cb_AddServiceName.Location = new System.Drawing.Point(150, 121);
            this.cb_AddServiceName.Name = "cb_AddServiceName";
            this.cb_AddServiceName.Size = new System.Drawing.Size(267, 23);
            this.cb_AddServiceName.TabIndex = 35;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "172.16.1.15",
            "172.16.1.33",
            "172.16.1.35",
            "172.16.1.37",
            "172.16.1.77",
            "172.16.1.96"});
            this.comboBox2.Location = new System.Drawing.Point(150, 24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(267, 23);
            this.comboBox2.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "ServiceName";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 32;
            this.label6.Text = "ServerIP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 571);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGMAIN)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button bt_ResumeService;
        private System.Windows.Forms.Button bt_StopService;
        private System.Windows.Forms.Button bt_StartService;
        private System.Windows.Forms.Button bt_PauseService;
        private System.Windows.Forms.ComboBox combo_ServiceName_User;
        private System.Windows.Forms.ComboBox combo_ServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_StartMode;
        private System.Windows.Forms.Label lb_State;
        private System.Windows.Forms.Label lb_DisplayName;
        private System.Windows.Forms.Label lb_ServiceName;
        private System.Windows.Forms.TextBox txb_StartMode;
        private System.Windows.Forms.TextBox txb_State;
        private System.Windows.Forms.TextBox txb_DisplayName;
        private System.Windows.Forms.TextBox txb_ServiceName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.RadioButton rbSocket;
        private System.Windows.Forms.RadioButton rbTcpClient;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyContrals.ExDataGridView DGMAIN;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton R_Service;
        private System.Windows.Forms.RadioButton R_Port;
        private System.Windows.Forms.TextBox tb_AddPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_AddServiceName;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
    }
}

