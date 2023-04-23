namespace MonitorAndControl
{
    partial class PortManage
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
            this.tb_ServerIP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tb_AddPort = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rbSocket = new System.Windows.Forms.RadioButton();
            this.rbTcpClient = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_ServerIP
            // 
            this.tb_ServerIP.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ServerIP.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_ServerIP.Location = new System.Drawing.Point(89, 7);
            this.tb_ServerIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_ServerIP.Name = "tb_ServerIP";
            this.tb_ServerIP.Size = new System.Drawing.Size(267, 23);
            this.tb_ServerIP.TabIndex = 58;
            this.tb_ServerIP.Text = "172.16.1.";
            this.tb_ServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_ServerIP_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 122);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 57;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(170, 122);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 22);
            this.button6.TabIndex = 56;
            this.button6.Text = "添加到清单";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // tb_AddPort
            // 
            this.tb_AddPort.Font = new System.Drawing.Font("微软雅黑", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.tb_AddPort.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_AddPort.Location = new System.Drawing.Point(89, 44);
            this.tb_AddPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tb_AddPort.Name = "tb_AddPort";
            this.tb_AddPort.Size = new System.Drawing.Size(267, 23);
            this.tb_AddPort.TabIndex = 54;
            this.tb_AddPort.Text = "请输入端口号";
            this.tb_AddPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_AddPort_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 53;
            this.label7.Text = "端口号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 52;
            this.label6.Text = "ServerIP";
            // 
            // rbSocket
            // 
            this.rbSocket.AutoSize = true;
            this.rbSocket.Location = new System.Drawing.Point(222, 86);
            this.rbSocket.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbSocket.Name = "rbSocket";
            this.rbSocket.Size = new System.Drawing.Size(103, 19);
            this.rbSocket.TabIndex = 39;
            this.rbSocket.Text = "Socket检测";
            this.rbSocket.UseVisualStyleBackColor = true;
            // 
            // rbTcpClient
            // 
            this.rbTcpClient.AutoSize = true;
            this.rbTcpClient.Checked = true;
            this.rbTcpClient.Location = new System.Drawing.Point(89, 86);
            this.rbTcpClient.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbTcpClient.Name = "rbTcpClient";
            this.rbTcpClient.Size = new System.Drawing.Size(127, 19);
            this.rbTcpClient.TabIndex = 38;
            this.rbTcpClient.TabStop = true;
            this.rbTcpClient.Text = "TcpClient检测";
            this.rbTcpClient.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(89, 122);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 22);
            this.button2.TabIndex = 59;
            this.button2.Text = "检测";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PortManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 162);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rbTcpClient);
            this.Controls.Add(this.tb_ServerIP);
            this.Controls.Add(this.rbSocket);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.tb_AddPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Name = "PortManage";
            this.Text = "PortManage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_ServerIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox tb_AddPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbSocket;
        private System.Windows.Forms.RadioButton rbTcpClient;
        private System.Windows.Forms.Button button2;
    }
}