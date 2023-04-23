
namespace MonitorAndControl
{
    partial class ServiceManage
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
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_ResumeService
            // 
            this.bt_ResumeService.Location = new System.Drawing.Point(279, 233);
            this.bt_ResumeService.Name = "bt_ResumeService";
            this.bt_ResumeService.Size = new System.Drawing.Size(75, 23);
            this.bt_ResumeService.TabIndex = 53;
            this.bt_ResumeService.Text = "恢复";
            this.bt_ResumeService.UseVisualStyleBackColor = true;
            // 
            // bt_StopService
            // 
            this.bt_StopService.Location = new System.Drawing.Point(360, 233);
            this.bt_StopService.Name = "bt_StopService";
            this.bt_StopService.Size = new System.Drawing.Size(75, 23);
            this.bt_StopService.TabIndex = 52;
            this.bt_StopService.Text = "停止";
            this.bt_StopService.UseVisualStyleBackColor = true;
            this.bt_StopService.Click += new System.EventHandler(this.bt_StopService_Click);
            // 
            // bt_StartService
            // 
            this.bt_StartService.Location = new System.Drawing.Point(117, 233);
            this.bt_StartService.Name = "bt_StartService";
            this.bt_StartService.Size = new System.Drawing.Size(75, 23);
            this.bt_StartService.TabIndex = 51;
            this.bt_StartService.Text = "运行";
            this.bt_StartService.UseVisualStyleBackColor = true;
            this.bt_StartService.Click += new System.EventHandler(this.bt_StartService_Click);
            // 
            // bt_PauseService
            // 
            this.bt_PauseService.Location = new System.Drawing.Point(198, 233);
            this.bt_PauseService.Name = "bt_PauseService";
            this.bt_PauseService.Size = new System.Drawing.Size(75, 23);
            this.bt_PauseService.TabIndex = 50;
            this.bt_PauseService.Text = "暂停";
            this.bt_PauseService.UseVisualStyleBackColor = true;
            // 
            // combo_ServiceName_User
            // 
            this.combo_ServiceName_User.FormattingEnabled = true;
            this.combo_ServiceName_User.Items.AddRange(new object[] {
            "SQLSERVERAGENT",
            "ABC"});
            this.combo_ServiceName_User.Location = new System.Drawing.Point(117, 37);
            this.combo_ServiceName_User.Name = "combo_ServiceName_User";
            this.combo_ServiceName_User.Size = new System.Drawing.Size(267, 23);
            this.combo_ServiceName_User.TabIndex = 49;
            this.combo_ServiceName_User.TextChanged += new System.EventHandler(this.combo_ServiceName_User_TextChanged);
            this.combo_ServiceName_User.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combo_ServiceName_User_KeyDown);
            this.combo_ServiceName_User.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combo_ServiceName_User_KeyPress);
            // 
            // combo_ServerIP
            // 
            this.combo_ServerIP.FormattingEnabled = true;
            this.combo_ServerIP.Items.AddRange(new object[] {
            "172.16.1.15",
            "172.16.1.33",
            "172.16.1.35",
            "172.16.1.37",
            "172.16.1.77",
            "172.16.1.96"});
            this.combo_ServerIP.Location = new System.Drawing.Point(117, 12);
            this.combo_ServerIP.Name = "combo_ServerIP";
            this.combo_ServerIP.Size = new System.Drawing.Size(267, 23);
            this.combo_ServerIP.TabIndex = 48;
            this.combo_ServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.combo_ServerIP_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "ServiceName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 46;
            this.label2.Text = "ServerIP";
            // 
            // lb_StartMode
            // 
            this.lb_StartMode.AutoSize = true;
            this.lb_StartMode.Location = new System.Drawing.Point(10, 199);
            this.lb_StartMode.Name = "lb_StartMode";
            this.lb_StartMode.Size = new System.Drawing.Size(79, 15);
            this.lb_StartMode.TabIndex = 45;
            this.lb_StartMode.Text = "StartMode";
            // 
            // lb_State
            // 
            this.lb_State.AutoSize = true;
            this.lb_State.Location = new System.Drawing.Point(10, 175);
            this.lb_State.Name = "lb_State";
            this.lb_State.Size = new System.Drawing.Size(47, 15);
            this.lb_State.TabIndex = 44;
            this.lb_State.Text = "State";
            // 
            // lb_DisplayName
            // 
            this.lb_DisplayName.AutoSize = true;
            this.lb_DisplayName.Location = new System.Drawing.Point(10, 150);
            this.lb_DisplayName.Name = "lb_DisplayName";
            this.lb_DisplayName.Size = new System.Drawing.Size(95, 15);
            this.lb_DisplayName.TabIndex = 43;
            this.lb_DisplayName.Text = "DisplayName";
            // 
            // lb_ServiceName
            // 
            this.lb_ServiceName.AutoSize = true;
            this.lb_ServiceName.Location = new System.Drawing.Point(10, 125);
            this.lb_ServiceName.Name = "lb_ServiceName";
            this.lb_ServiceName.Size = new System.Drawing.Size(95, 15);
            this.lb_ServiceName.TabIndex = 42;
            this.lb_ServiceName.Text = "ServiceName";
            // 
            // txb_StartMode
            // 
            this.txb_StartMode.Location = new System.Drawing.Point(117, 196);
            this.txb_StartMode.Name = "txb_StartMode";
            this.txb_StartMode.ReadOnly = true;
            this.txb_StartMode.Size = new System.Drawing.Size(566, 25);
            this.txb_StartMode.TabIndex = 41;
            // 
            // txb_State
            // 
            this.txb_State.Location = new System.Drawing.Point(117, 171);
            this.txb_State.Name = "txb_State";
            this.txb_State.ReadOnly = true;
            this.txb_State.Size = new System.Drawing.Size(566, 25);
            this.txb_State.TabIndex = 40;
            // 
            // txb_DisplayName
            // 
            this.txb_DisplayName.Location = new System.Drawing.Point(117, 146);
            this.txb_DisplayName.Name = "txb_DisplayName";
            this.txb_DisplayName.ReadOnly = true;
            this.txb_DisplayName.Size = new System.Drawing.Size(566, 25);
            this.txb_DisplayName.TabIndex = 39;
            // 
            // txb_ServiceName
            // 
            this.txb_ServiceName.Location = new System.Drawing.Point(117, 122);
            this.txb_ServiceName.Name = "txb_ServiceName";
            this.txb_ServiceName.ReadOnly = true;
            this.txb_ServiceName.Size = new System.Drawing.Size(566, 25);
            this.txb_ServiceName.TabIndex = 38;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(117, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "获取状态";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(242, 62);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 54;
            this.button2.Text = "添加到清单";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ServiceManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 272);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.bt_ResumeService);
            this.Controls.Add(this.bt_StopService);
            this.Controls.Add(this.bt_StartService);
            this.Controls.Add(this.bt_PauseService);
            this.Controls.Add(this.combo_ServiceName_User);
            this.Controls.Add(this.combo_ServerIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_StartMode);
            this.Controls.Add(this.lb_State);
            this.Controls.Add(this.lb_DisplayName);
            this.Controls.Add(this.lb_ServiceName);
            this.Controls.Add(this.txb_StartMode);
            this.Controls.Add(this.txb_State);
            this.Controls.Add(this.txb_DisplayName);
            this.Controls.Add(this.txb_ServiceName);
            this.Controls.Add(this.button1);
            this.Name = "ServiceManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ServiceManage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.Button button2;
    }
}