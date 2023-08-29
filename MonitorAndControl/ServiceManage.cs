using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IronPython.Modules._ast;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MonitorAndControl
{
    public partial class ServiceManage : Form
    {
        public ServiceManage()
        {
            InitializeComponent();
        }

        string ServerIP = "";//服务器IP
        string ServiceName = "";//服务名称
        private void button1_Click(object sender, EventArgs e)
        {
            txb_ServiceName.Text = "";
            txb_DisplayName.Text = "";
            txb_StartMode.Text = "";
            txb_State.Text = "";
            ServerIP = combo_ServerIP.Text.ToString();
            ServiceName = combo_ServiceName_User.Text.ToString().Trim();
            InputPassWord ps = new InputPassWord();
            ps.ShowDialog();
            if (ps.DialogResult != DialogResult.OK) { MessageBox.Show("请重新输入密码"); return; }
            string SvrUser = ps.stringUsername;
            string Svrpwd = ps.stringPassword;

            Win32ServiceManager M = new Win32ServiceManager(ServerIP, SvrUser, Svrpwd);
            string s = M.CmdPing(ServerIP,1);
            if (s != "连接")
            {

                MessageBox.Show(s);
                return;
            }

            if (ServerIP == "" || ServiceName == "")
            {
                MessageBox.Show("请选择服务器地址和服务");
                return;
            }
            GetValue(ServerIP, SvrUser, Svrpwd, ServiceName);
        }

        private void bt_StartService_Click(object sender, EventArgs e)
        {
            ServerIP = combo_ServerIP.Text.ToString();
            ServiceName = combo_ServiceName_User.Text.ToString().Trim();
            if (ServerIP == "" || ServiceName == "")
            {
                return;
            }

            Win32ServiceManager s = new Win32ServiceManager(ServerIP, "highrock\\administrator", "@pStRy8214");

            s.StartService(ServiceName);
          //  GetValue(ServerIP, ServiceName);
        }

        private void GetValue(String ServerIP,string stringUsername, string stringPassword, string ServiceName)
        {
            
            string psw = stringPassword;
            string username = stringUsername;
            Win32ServiceManager s = new Win32ServiceManager(ServerIP, username, psw);
            
            //string ServerName1 = "SQLSERVERAGENT";
            Array aaaa = s.GetServiceList(ServiceName);


            //string a=s.GetServiceList(ServerName1).ToString();
            if ((aaaa.GetValue(0, 0) == null))
            { MessageBox.Show("找不到此服务，请确认服务名称"); return; }
            else if (aaaa.GetValue(0, 0).ToString().Trim() == "找不到" || aaaa.GetValue(0, 0).ToString().Trim() == "拒绝访问")
            { MessageBox.Show("找不到此服务，请确认服务名称"); return; }
            txb_ServiceName.Text = aaaa.GetValue(0, 0).ToString();

            txb_DisplayName.Text = aaaa.GetValue(0, 1).ToString();
            txb_State.Text = aaaa.GetValue(0, 2).ToString();
            txb_StartMode.Text = aaaa.GetValue(0, 3).ToString();

            if (aaaa.GetValue(0, 2).ToString() == "")
            {
                bt_StartService.Enabled = false;
                bt_PauseService.Enabled = false;
                bt_ResumeService.Enabled = false;
                bt_StopService.Enabled = false;
                button2.Enabled = false;
            }
            else if (aaaa.GetValue(0, 2).ToString() == "Running")
            {
                bt_StartService.Enabled = false;
                bt_PauseService.Enabled = true;
                bt_ResumeService.Enabled = false;
                bt_StopService.Enabled = true;
                button2.Enabled = true;
            }
            else if (aaaa.GetValue(0, 2).ToString() == "Stopped" || aaaa.GetValue(0, 2).ToString() == "Stop Pending")
            {
                bt_StartService.Enabled = true;
                bt_PauseService.Enabled = false;
                bt_ResumeService.Enabled = false;
                bt_StopService.Enabled = false;
                button2.Enabled = true;
            }


        }

        private void bt_StopService_Click(object sender, EventArgs e)
        {
            ServerIP = combo_ServerIP.Text.ToString();
            ServiceName = combo_ServiceName_User.Text.ToString().Trim();
            if (ServerIP == "" || ServiceName == "")
            {
                return;
            }

            Win32ServiceManager s = new Win32ServiceManager(ServerIP, "highrock\\administrator", "@pStRy8214");

            s.StopService(ServiceName);
           // GetValue(ServerIP, ServiceName);
        }


 

        private void combo_ServiceName_User_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true;
            }
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

        private void combo_ServiceName_User_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == 8)
                    e.Handled = false;
                else
                    e.Handled = true;
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());            
        }

        private void combo_ServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
        }

        private void combo_ServiceName_User_TextChanged(object sender, EventArgs e)
        {
            txb_ServiceName.Text = "";
            txb_DisplayName.Text = "";
            txb_StartMode.Text = "";
            txb_State.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txb_DisplayName.Text != "")
            {
                Win32ServiceManager wsm = new Win32ServiceManager();
                int save = wsm.SaveItem(ServerIP, 0, ServiceName);
            }
            else
            {
                MessageBox.Show("请选择服务");
            }
        }

        private void ServiceManage_Load(object sender, EventArgs e)
        {

        }
    }
}
