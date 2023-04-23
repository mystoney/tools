using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorAndControl
{
    public partial class ServerAdd : Form
    {
        public ServerAdd()
        {
            InitializeComponent();
        }
        public Boolean s = false;
        private string ServerIP = "";
        private int CheckType = -1;
        private string CheckItem = "";


        private void R_Service_CheckedChanged(object sender, EventArgs e)
        {
            if (R_Service.Checked) { tb_AddPort.Enabled = false; tb_ServiceName.Enabled = true; } 
        }

        private void R_Port_CheckedChanged(object sender, EventArgs e)
        {
            if (R_Port.Checked) { tb_AddPort.Enabled = true; tb_ServiceName.Enabled = false; }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (isIp(tb_ServerIP.Text.Trim()) == false)
            { MessageBox.Show("Ip地址错误"); return; }
            int save = 0;
            if (R_Service.Checked) 
            {
                CheckType = 0;//0为服务 1为端口
                if (tb_ServiceName.Text.ToString().Trim()==""|| tb_ServiceName.Text.ToString().Trim() == "输入服务名称") { MessageBox.Show("请输入服务名称"); return; }
                ServerIP = tb_ServerIP.Text.Trim();
                string s = CmdPing(ServerIP);
                if (s != "连接")
                {
                    MessageBox.Show(s);
                    return;
                }
                

                int r= GetValue(ServerIP, tb_ServiceName.Text.ToString().Trim());
                if (r == 0) { MessageBox.Show("找不到此服务，请确认服务名称"); return; }
                tb_AddPort.Text = "";                
                CheckItem = tb_ServiceName.Text.ToString().Trim();
                Win32ServiceManager wsm = new Win32ServiceManager();
                save= wsm.SaveItem(ServerIP, CheckType, CheckItem);

            }
            if (R_Port.Checked)
            {
                if (tb_AddPort.Text.ToString().Trim() == "") { MessageBox.Show("请输入端口号"); return; }
                try
                {
                    int ss = Convert.ToInt32(tb_AddPort.Text.ToString());
                    if (ss < 0 || ss > 65535) { MessageBox.Show("端口号错误"); return; }
                }
                catch
                {
                    MessageBox.Show("端口号错误"); return;
                }
                ServerIP = tb_ServerIP.Text.Trim();
                string s = CmdPing(ServerIP);
                if (s != "连接")
                {
                    MessageBox.Show(s);
                    return;
                }
                CheckType = 1;//0为服务 1为端口
               
                tb_ServiceName.Text = "";

                CheckItem = tb_AddPort.Text.Trim();
                Win32ServiceManager wsm = new Win32ServiceManager();
                save = wsm.SaveItem(ServerIP, CheckType, CheckItem);
            }
            if (save == 1) 
            { MessageBox.Show("成功"); tb_ServerIP.Text = "";tb_ServiceName.Text = "";tb_ServiceName.Text = "";tb_AddPort.Text = ""; }
            else { MessageBox.Show("重复的项目"); }
        }

        private int GetValue(String ServerIP, string ServiceName)
        {
            Win32ServiceManager s = new Win32ServiceManager();
            //string ServerName1 = "SQLSERVERAGENT";
            Array aaaa = s.GetServiceList(ServerIP, "highrock\\administrtor", "@pStRy8214", ServiceName);
            //string a=s.GetServiceList(ServerName1).ToString();
            if (aaaa.GetValue(0, 0).ToString().Trim() == "找不到" || aaaa.GetValue(0, 0).ToString().Trim() == "RPC 服务器不可用。")
            { return 0; }
            else { return 1; }
        }

        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>
        /// <returns></returns>
        public static bool isIp(string ip)
        {
             //如果为空，认为验证不合格
            if(string.IsNullOrEmpty(ip))
             {
                 return false;
             }
 
             //清除要验证字符传中的空格
             ip=ip.Trim();
 
             //模式字符串，正则表达式
             string patten = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
 
             //验证
             return Regex.IsMatch(ip, patten); 
         }

        private void tb_AddPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 )
                e.Handled = true; 
        }

        private void tb_ServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
        }
        /// <summary>
        /// 服务名称自动填入大写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_ServiceName_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void tb_ServiceName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true;
            }
        }

        private static string CmdPing(string strIp)
        {
            Process p = new Process(); p.StartInfo.FileName = "cmd.exe";//设定程序名
            p.StartInfo.UseShellExecute = false; //关闭Shell的使用
            p.StartInfo.RedirectStandardInput = true;//重定向标准输入
            p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
            p.StartInfo.RedirectStandardError = true;//重定向错误输出
            p.StartInfo.CreateNoWindow = true;//设置不显示窗口
            string pingrst; p.Start(); p.StandardInput.WriteLine("ping " + strIp);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("(0% loss)") != -1)
            {
                pingrst = "连接";
            }
            if (strRst.IndexOf("(0% 丢失)") != -1)
            {
                pingrst = "连接";
            }
            else if (strRst.IndexOf("Destination host unreachable.") != -1)
            {
                pingrst = "无法到达目的主机";
            }
            else if (strRst.IndexOf("一般故障。") != -1)
            {
                pingrst = "无法到达目的主机";
            }
            else if (strRst.IndexOf("Request timed out.") != -1)
            {
                pingrst = "超时";
            }
            else if (strRst.IndexOf("请求超时。") != -1)
            {
                pingrst = "超时";
            }
            else if (strRst.IndexOf("Unknown host") != -1)
            {
                pingrst = "无法解析主机";
            }
            else if (strRst.IndexOf("请求找不到主机") != -1)
            {
                pingrst = "无法解析主机";
            }
            else
            {
                pingrst = strRst;
            }
            p.Close();
            return pingrst;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }



    }
 }
    
