namespace MonitorAndControl
{
    partial class ItemAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemAdd));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tb_ServiceName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_ServerIP = new System.Windows.Forms.TextBox();
            this.btCheckPort = new System.Windows.Forms.Button();
            this.rbTcpClient = new System.Windows.Forms.RadioButton();
            this.rbSocket = new System.Windows.Forms.RadioButton();
            this.tb_AddPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bt_ResumeService = new System.Windows.Forms.Button();
            this.btCheckPing = new System.Windows.Forms.Button();
            this.bt_StopService = new System.Windows.Forms.Button();
            this.bt_StartService = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_PauseService = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btCheckkService = new System.Windows.Forms.Button();
            this.txb_ServiceName = new System.Windows.Forms.TextBox();
            this.lb_StartMode = new System.Windows.Forms.Label();
            this.txb_DisplayName = new System.Windows.Forms.TextBox();
            this.lb_State = new System.Windows.Forms.Label();
            this.txb_State = new System.Windows.Forms.TextBox();
            this.lb_DisplayName = new System.Windows.Forms.Label();
            this.txb_StartMode = new System.Windows.Forms.TextBox();
            this.lb_ServiceName = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.DGNOW = new MyContrals.ExDataGridView();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.DGADD = new MyContrals.ExDataGridView();
            this.btDelItem = new System.Windows.Forms.Button();
            this.bt_Save = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGNOW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGADD)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tb_ServiceName);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.tb_ServerIP);
            this.splitContainer1.Panel1.Controls.Add(this.btCheckPort);
            this.splitContainer1.Panel1.Controls.Add(this.rbTcpClient);
            this.splitContainer1.Panel1.Controls.Add(this.rbSocket);
            this.splitContainer1.Panel1.Controls.Add(this.tb_AddPort);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.bt_ResumeService);
            this.splitContainer1.Panel1.Controls.Add(this.btCheckPing);
            this.splitContainer1.Panel1.Controls.Add(this.bt_StopService);
            this.splitContainer1.Panel1.Controls.Add(this.bt_StartService);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.bt_PauseService);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btCheckkService);
            this.splitContainer1.Panel1.Controls.Add(this.txb_ServiceName);
            this.splitContainer1.Panel1.Controls.Add(this.lb_StartMode);
            this.splitContainer1.Panel1.Controls.Add(this.txb_DisplayName);
            this.splitContainer1.Panel1.Controls.Add(this.lb_State);
            this.splitContainer1.Panel1.Controls.Add(this.txb_State);
            this.splitContainer1.Panel1.Controls.Add(this.lb_DisplayName);
            this.splitContainer1.Panel1.Controls.Add(this.txb_StartMode);
            this.splitContainer1.Panel1.Controls.Add(this.lb_ServiceName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(550, 877);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 0;
            // 
            // tb_ServiceName
            // 
            this.tb_ServiceName.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tb_ServiceName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_ServiceName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tb_ServiceName.Location = new System.Drawing.Point(120, 94);
            this.tb_ServiceName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_ServiceName.Name = "tb_ServiceName";
            this.tb_ServiceName.Size = new System.Drawing.Size(267, 27);
            this.tb_ServiceName.TabIndex = 81;
            this.tb_ServiceName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ServiceName_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(330, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 80;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_ServerIP
            // 
            this.tb_ServerIP.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ServerIP.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_ServerIP.Location = new System.Drawing.Point(120, 7);
            this.tb_ServerIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_ServerIP.Name = "tb_ServerIP";
            this.tb_ServerIP.Size = new System.Drawing.Size(204, 27);
            this.tb_ServerIP.TabIndex = 79;
            this.tb_ServerIP.Text = "172.16.1.";
            this.tb_ServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ServerIP_KeyPress);
            // 
            // btCheckPort
            // 
            this.btCheckPort.Enabled = false;
            this.btCheckPort.Location = new System.Drawing.Point(394, 39);
            this.btCheckPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btCheckPort.Name = "btCheckPort";
            this.btCheckPort.Size = new System.Drawing.Size(119, 22);
            this.btCheckPort.TabIndex = 78;
            this.btCheckPort.Text = "检测并添加";
            this.btCheckPort.UseVisualStyleBackColor = true;
            this.btCheckPort.Click += new System.EventHandler(this.btCheckPort_Click);
            // 
            // rbTcpClient
            // 
            this.rbTcpClient.AutoSize = true;
            this.rbTcpClient.Checked = true;
            this.rbTcpClient.Location = new System.Drawing.Point(120, 69);
            this.rbTcpClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbTcpClient.Name = "rbTcpClient";
            this.rbTcpClient.Size = new System.Drawing.Size(130, 19);
            this.rbTcpClient.TabIndex = 73;
            this.rbTcpClient.TabStop = true;
            this.rbTcpClient.Text = "TcpClient检测";
            this.rbTcpClient.UseVisualStyleBackColor = true;
            // 
            // rbSocket
            // 
            this.rbSocket.AutoSize = true;
            this.rbSocket.Location = new System.Drawing.Point(256, 69);
            this.rbSocket.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbSocket.Name = "rbSocket";
            this.rbSocket.Size = new System.Drawing.Size(106, 19);
            this.rbSocket.TabIndex = 74;
            this.rbSocket.Text = "Socket检测";
            this.rbSocket.UseVisualStyleBackColor = true;
            // 
            // tb_AddPort
            // 
            this.tb_AddPort.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tb_AddPort.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_AddPort.Location = new System.Drawing.Point(120, 38);
            this.tb_AddPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_AddPort.Name = "tb_AddPort";
            this.tb_AddPort.Size = new System.Drawing.Size(267, 27);
            this.tb_AddPort.TabIndex = 76;
            this.tb_AddPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_AddPort_KeyDown);
            this.tb_AddPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_AddPort_KeyPress_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 15);
            this.label7.TabIndex = 75;
            this.label7.Text = "PortNum";
            // 
            // bt_ResumeService
            // 
            this.bt_ResumeService.Enabled = false;
            this.bt_ResumeService.Location = new System.Drawing.Point(357, 237);
            this.bt_ResumeService.Name = "bt_ResumeService";
            this.bt_ResumeService.Size = new System.Drawing.Size(75, 23);
            this.bt_ResumeService.TabIndex = 69;
            this.bt_ResumeService.Text = "恢复";
            this.bt_ResumeService.UseVisualStyleBackColor = true;
            // 
            // btCheckPing
            // 
            this.btCheckPing.Location = new System.Drawing.Point(394, 11);
            this.btCheckPing.Name = "btCheckPing";
            this.btCheckPing.Size = new System.Drawing.Size(119, 23);
            this.btCheckPing.TabIndex = 71;
            this.btCheckPing.Text = "PING检测并添加";
            this.btCheckPing.UseVisualStyleBackColor = true;
            this.btCheckPing.Click += new System.EventHandler(this.btCheckPing_Click);
            // 
            // bt_StopService
            // 
            this.bt_StopService.Enabled = false;
            this.bt_StopService.Location = new System.Drawing.Point(438, 237);
            this.bt_StopService.Name = "bt_StopService";
            this.bt_StopService.Size = new System.Drawing.Size(75, 23);
            this.bt_StopService.TabIndex = 68;
            this.bt_StopService.Text = "停止";
            this.bt_StopService.UseVisualStyleBackColor = true;
            // 
            // bt_StartService
            // 
            this.bt_StartService.Enabled = false;
            this.bt_StartService.Location = new System.Drawing.Point(195, 237);
            this.bt_StartService.Name = "bt_StartService";
            this.bt_StartService.Size = new System.Drawing.Size(75, 23);
            this.bt_StartService.TabIndex = 67;
            this.bt_StartService.Text = "运行";
            this.bt_StartService.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "ServerIP";
            // 
            // bt_PauseService
            // 
            this.bt_PauseService.Enabled = false;
            this.bt_PauseService.Location = new System.Drawing.Point(276, 237);
            this.bt_PauseService.Name = "bt_PauseService";
            this.bt_PauseService.Size = new System.Drawing.Size(75, 23);
            this.bt_PauseService.TabIndex = 66;
            this.bt_PauseService.Text = "暂停";
            this.bt_PauseService.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 64;
            this.label1.Text = "ServiceName";
            // 
            // btCheckkService
            // 
            this.btCheckkService.Enabled = false;
            this.btCheckkService.Location = new System.Drawing.Point(394, 98);
            this.btCheckkService.Name = "btCheckkService";
            this.btCheckkService.Size = new System.Drawing.Size(119, 23);
            this.btCheckkService.TabIndex = 55;
            this.btCheckkService.Text = "获取状态并添加";
            this.btCheckkService.UseVisualStyleBackColor = true;
            this.btCheckkService.Click += new System.EventHandler(this.btCheckkService_Click);
            // 
            // txb_ServiceName
            // 
            this.txb_ServiceName.Location = new System.Drawing.Point(225, 132);
            this.txb_ServiceName.Name = "txb_ServiceName";
            this.txb_ServiceName.ReadOnly = true;
            this.txb_ServiceName.Size = new System.Drawing.Size(288, 25);
            this.txb_ServiceName.TabIndex = 56;
            // 
            // lb_StartMode
            // 
            this.lb_StartMode.AutoSize = true;
            this.lb_StartMode.Location = new System.Drawing.Point(118, 209);
            this.lb_StartMode.Name = "lb_StartMode";
            this.lb_StartMode.Size = new System.Drawing.Size(79, 15);
            this.lb_StartMode.TabIndex = 63;
            this.lb_StartMode.Text = "StartMode";
            // 
            // txb_DisplayName
            // 
            this.txb_DisplayName.Location = new System.Drawing.Point(225, 156);
            this.txb_DisplayName.Name = "txb_DisplayName";
            this.txb_DisplayName.ReadOnly = true;
            this.txb_DisplayName.Size = new System.Drawing.Size(288, 25);
            this.txb_DisplayName.TabIndex = 57;
            // 
            // lb_State
            // 
            this.lb_State.AutoSize = true;
            this.lb_State.Location = new System.Drawing.Point(118, 185);
            this.lb_State.Name = "lb_State";
            this.lb_State.Size = new System.Drawing.Size(47, 15);
            this.lb_State.TabIndex = 62;
            this.lb_State.Text = "State";
            // 
            // txb_State
            // 
            this.txb_State.Location = new System.Drawing.Point(225, 181);
            this.txb_State.Name = "txb_State";
            this.txb_State.ReadOnly = true;
            this.txb_State.Size = new System.Drawing.Size(288, 25);
            this.txb_State.TabIndex = 58;
            // 
            // lb_DisplayName
            // 
            this.lb_DisplayName.AutoSize = true;
            this.lb_DisplayName.Location = new System.Drawing.Point(118, 160);
            this.lb_DisplayName.Name = "lb_DisplayName";
            this.lb_DisplayName.Size = new System.Drawing.Size(95, 15);
            this.lb_DisplayName.TabIndex = 61;
            this.lb_DisplayName.Text = "DisplayName";
            // 
            // txb_StartMode
            // 
            this.txb_StartMode.Location = new System.Drawing.Point(225, 206);
            this.txb_StartMode.Name = "txb_StartMode";
            this.txb_StartMode.ReadOnly = true;
            this.txb_StartMode.Size = new System.Drawing.Size(288, 25);
            this.txb_StartMode.TabIndex = 59;
            // 
            // lb_ServiceName
            // 
            this.lb_ServiceName.AutoSize = true;
            this.lb_ServiceName.Location = new System.Drawing.Point(118, 135);
            this.lb_ServiceName.Name = "lb_ServiceName";
            this.lb_ServiceName.Size = new System.Drawing.Size(95, 15);
            this.lb_ServiceName.TabIndex = 60;
            this.lb_ServiceName.Text = "ServiceName";
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btDelItem);
            this.splitContainer2.Panel2.Controls.Add(this.bt_Save);
            this.splitContainer2.Panel2.Controls.Add(this.bt_Cancel);
            this.splitContainer2.Size = new System.Drawing.Size(550, 602);
            this.splitContainer2.SplitterDistance = 549;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer3.Size = new System.Drawing.Size(550, 549);
            this.splitContainer3.SplitterDistance = 253;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.DGNOW);
            this.splitContainer4.Size = new System.Drawing.Size(550, 253);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 64;
            this.label3.Text = "现有项目";
            // 
            // DGNOW
            // 
            this.DGNOW.AllowUserToAddRows = false;
            this.DGNOW.AllowUserToDeleteRows = false;
            this.DGNOW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGNOW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGNOW.Location = new System.Drawing.Point(0, 0);
            this.DGNOW.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DGNOW.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DGNOW.MergeColumnNames")));
            this.DGNOW.Name = "DGNOW";
            this.DGNOW.RowHeadersVisible = false;
            this.DGNOW.RowHeadersWidth = 51;
            this.DGNOW.RowTemplate.Height = 27;
            this.DGNOW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGNOW.Size = new System.Drawing.Size(550, 224);
            this.DGNOW.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.DGADD);
            this.splitContainer5.Size = new System.Drawing.Size(550, 292);
            this.splitContainer5.SplitterDistance = 26;
            this.splitContainer5.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 64;
            this.label4.Text = "新增项目";
            // 
            // DGADD
            // 
            this.DGADD.AllowUserToAddRows = false;
            this.DGADD.AllowUserToDeleteRows = false;
            this.DGADD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGADD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGADD.Location = new System.Drawing.Point(0, 0);
            this.DGADD.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DGADD.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DGADD.MergeColumnNames")));
            this.DGADD.Name = "DGADD";
            this.DGADD.RowHeadersVisible = false;
            this.DGADD.RowHeadersWidth = 51;
            this.DGADD.RowTemplate.Height = 27;
            this.DGADD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGADD.Size = new System.Drawing.Size(550, 262);
            this.DGADD.TabIndex = 0;
            // 
            // btDelItem
            // 
            this.btDelItem.Location = new System.Drawing.Point(8, 6);
            this.btDelItem.Name = "btDelItem";
            this.btDelItem.Size = new System.Drawing.Size(75, 23);
            this.btDelItem.TabIndex = 70;
            this.btDelItem.Text = "删除";
            this.btDelItem.UseVisualStyleBackColor = true;
            this.btDelItem.Click += new System.EventHandler(this.btDelItem_Click);
            // 
            // bt_Save
            // 
            this.bt_Save.Location = new System.Drawing.Point(185, 6);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(75, 23);
            this.bt_Save.TabIndex = 69;
            this.bt_Save.Text = "提交";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(266, 6);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_Cancel.TabIndex = 68;
            this.bt_Cancel.Text = "取消";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            // 
            // ItemAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 877);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ItemAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择和查看需要检测的项目";
            this.Load += new System.EventHandler(this.ItemAdd_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGNOW)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGADD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btCheckPing;
        private System.Windows.Forms.Button bt_ResumeService;
        private System.Windows.Forms.Button bt_StopService;
        private System.Windows.Forms.Button bt_StartService;
        private System.Windows.Forms.Button bt_PauseService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_StartMode;
        private System.Windows.Forms.Label lb_State;
        private System.Windows.Forms.Label lb_DisplayName;
        private System.Windows.Forms.Label lb_ServiceName;
        private System.Windows.Forms.TextBox txb_StartMode;
        private System.Windows.Forms.TextBox txb_State;
        private System.Windows.Forms.TextBox txb_DisplayName;
        private System.Windows.Forms.TextBox txb_ServiceName;
        private System.Windows.Forms.Button btCheckkService;
        private System.Windows.Forms.Button btDelItem;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Button btCheckPort;
        private System.Windows.Forms.RadioButton rbTcpClient;
        private System.Windows.Forms.RadioButton rbSocket;
        private System.Windows.Forms.TextBox tb_AddPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Label label3;
        private MyContrals.ExDataGridView DGNOW;
        private System.Windows.Forms.Label label4;
        private MyContrals.ExDataGridView DGADD;
        private System.Windows.Forms.TextBox tb_ServerIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_ServiceName;
    }
}