
namespace MonitorAndControl
{
    partial class ServerAdd
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
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ServiceName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_AddPort = new System.Windows.Forms.TextBox();
            this.R_Port = new System.Windows.Forms.RadioButton();
            this.R_Service = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_ServerIP = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 15);
            this.label5.TabIndex = 43;
            this.label5.Text = "ServiceName";
            // 
            // tb_ServiceName
            // 
            this.tb_ServiceName.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tb_ServiceName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_ServiceName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tb_ServiceName.Location = new System.Drawing.Point(121, 109);
            this.tb_ServiceName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_ServiceName.Name = "tb_ServiceName";
            this.tb_ServiceName.Size = new System.Drawing.Size(267, 23);
            this.tb_ServiceName.TabIndex = 52;
            this.tb_ServiceName.Text = "输入服务名称";
            this.tb_ServiceName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_ServiceName_KeyDown);
            this.tb_ServiceName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ServiceName_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 42;
            this.label6.Text = "ServerIP";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 15);
            this.label7.TabIndex = 46;
            this.label7.Text = "端 口 号：";
            // 
            // tb_AddPort
            // 
            this.tb_AddPort.Enabled = false;
            this.tb_AddPort.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tb_AddPort.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_AddPort.Location = new System.Drawing.Point(121, 142);
            this.tb_AddPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_AddPort.Name = "tb_AddPort";
            this.tb_AddPort.Size = new System.Drawing.Size(267, 23);
            this.tb_AddPort.TabIndex = 47;
            this.tb_AddPort.Text = "请输入端口号";
            this.tb_AddPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_AddPort_KeyPress);
            // 
            // R_Port
            // 
            this.R_Port.AutoSize = true;
            this.R_Port.Location = new System.Drawing.Point(69, 24);
            this.R_Port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.R_Port.Name = "R_Port";
            this.R_Port.Size = new System.Drawing.Size(55, 19);
            this.R_Port.TabIndex = 39;
            this.R_Port.Text = "端口";
            this.R_Port.UseVisualStyleBackColor = true;
            this.R_Port.CheckedChanged += new System.EventHandler(this.R_Port_CheckedChanged);
            // 
            // R_Service
            // 
            this.R_Service.AutoSize = true;
            this.R_Service.Checked = true;
            this.R_Service.Location = new System.Drawing.Point(5, 24);
            this.R_Service.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.R_Service.Name = "R_Service";
            this.R_Service.Size = new System.Drawing.Size(55, 19);
            this.R_Service.TabIndex = 38;
            this.R_Service.TabStop = true;
            this.R_Service.Text = "服务";
            this.R_Service.UseVisualStyleBackColor = true;
            this.R_Service.CheckedChanged += new System.EventHandler(this.R_Service_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.R_Service);
            this.groupBox1.Controls.Add(this.R_Port);
            this.groupBox1.Location = new System.Drawing.Point(121, 41);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(148, 51);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(121, 182);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 22);
            this.button6.TabIndex = 49;
            this.button6.Text = "添加";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(203, 182);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 50;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_ServerIP
            // 
            this.tb_ServerIP.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ServerIP.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_ServerIP.Location = new System.Drawing.Point(121, 10);
            this.tb_ServerIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_ServerIP.Name = "tb_ServerIP";
            this.tb_ServerIP.Size = new System.Drawing.Size(267, 23);
            this.tb_ServerIP.TabIndex = 51;
            this.tb_ServerIP.Text = "172.16.1.";
            this.tb_ServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ServerIP_KeyPress);
            // 
            // ServerAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 225);
            this.Controls.Add(this.tb_ServiceName);
            this.Controls.Add(this.tb_ServerIP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_AddPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ServerAdd";
            this.Text = "ServerAdd";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_ServiceName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_AddPort;
        private System.Windows.Forms.RadioButton R_Port;
        private System.Windows.Forms.RadioButton R_Service;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_ServerIP;
    }
}