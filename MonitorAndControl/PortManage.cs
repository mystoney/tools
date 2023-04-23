using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorAndControl
{
    public partial class PortManage : Form
    {
        public PortManage()
        {
            InitializeComponent();
        }

        private void tb_ServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
        }

        private void tb_AddPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }

        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>
        /// <returns></returns>
        public static bool isIp(string ip)
        {
            //如果为空，认为验证不合格
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            //清除要验证字符传中的空格
            ip = ip.Trim();

            //模式字符串，正则表达式
            string patten = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

            //验证
            return Regex.IsMatch(ip, patten);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isIp(tb_ServerIP.Text.Trim()) == false)
            { MessageBox.Show("Ip地址错误"); return; }

            if (tb_AddPort.Text.ToString().Trim() == "") { MessageBox.Show("请输入端口号"); return; }
            try
            {
                string ServerIP = tb_ServerIP.Text.Trim();
                int ServerPort = Convert.ToInt32(tb_AddPort.Text.ToString());
                int ss = Convert.ToInt32(ServerPort);
                if (ss < 0 || ss > 65535) { MessageBox.Show("端口号错误"); return; }
                ;
  
                Win32ServiceManager wsm = new Win32ServiceManager();
                string s = wsm.CmdPing(ServerIP,1);
                if (s != "连接")
                {
                    MessageBox.Show(s); return;
                }                
                button6.Enabled = true;
                string c = "";
                if (rbTcpClient.Checked) {  c = wsm.TcpClientCheck(ServerIP, ServerPort,1); }
                if (rbSocket.Checked) {  c = wsm.SocketCheck(ServerIP, ServerPort,1); }

                if (c != "端口打开")
                {
                    MessageBox.Show(c); return;
                }                

                if (MessageBox.Show(this, "需要添加到监控清单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int CheckType = 1;//0为服务 1为端口
                    string CheckItem = tb_AddPort.Text.Trim();
                    int save = wsm.SaveItem(ServerIP, CheckType, CheckItem);
                    if (save == 1)
                    { MessageBox.Show("成功"); tb_ServerIP.Text = ""; tb_AddPort.Text = ""; button6.Enabled = false; Close(); }
                    else { MessageBox.Show("重复的项目"); return; }
                }






            }
            catch
            {
                MessageBox.Show("端口号错误"); return;
            }

        }
    }
}
