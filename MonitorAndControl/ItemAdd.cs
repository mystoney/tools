using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TX.Framework.Security.Base;
using static MonitorAndControl.Win32ServiceManager;

namespace MonitorAndControl
{
    public partial class ItemAdd : Form
    {
        List<Win32ServiceManager.ServerCheckItem> CheckItemAdd = new List<Win32ServiceManager.ServerCheckItem>();
        List<Win32ServiceManager.ServerCheckItem> CheckItemNow = new List<Win32ServiceManager.ServerCheckItem>();

        public ItemAdd()
        {
            InitializeComponent();
        }
        int AddOrMod = 0;//>0为新增 1为修改
        /// <summary>
        /// 新增或者修改
        /// </summary>
        /// <param name="i">0为新增 1为修改</param>
        public ItemAdd(int i)
        {
            InitializeComponent();
            AddOrMod = i;
        }

        private void ItemAdd_Load(object sender, EventArgs e)
        {
            
        }

        private void ButtonStateCheck()
        {       
            btCheckPort.Enabled = false;
            btCheckkService.Enabled = false;       
             if (DGADD.Rows.Count > 0) { btDelItem.Enabled = true; bt_Save.Enabled = true; } else { btDelItem.Enabled = false; bt_Save.Enabled = false; }        
        }

        private void tb_ServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
        }

        private void tb_AddPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true;
            }
        }


        private void tb_AddPort_MouseClick(object sender, MouseEventArgs e)
        {
            tb_AddPort.Text = "";
        }
        #region 验证IP地址是否合法
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
        #endregion

        private void btCheckPing_Click(object sender, EventArgs e)
        {
            if (isIp(tb_ServerIP.Text.Trim()) == false)
            { MessageBox.Show("Ip地址错误"); return; }

            Win32ServiceManager w = new Win32ServiceManager();
            tb_ServerIP.Enabled = false;
            string s=w.CmdPing(tb_ServerIP.Text.ToString().Trim(),1);
            if (s == "连接")
            {
                GetDGNOW(tb_ServerIP.Text.ToString().Trim());
     
                btCheckkService.Enabled = true;
                btCheckPort.Enabled = true;
           
            }
            else
            {
                
                btCheckkService.Enabled = true;                
                btCheckPort.Enabled = false;
                
            }
            //w.SaveItem(tb_ServerIP.Text.ToString().Trim(), -1, "Ping");

            Win32ServiceManager.ServerCheckItem a = new ServerCheckItem();
            a.ServerIP = tb_ServerIP.Text.ToString().Trim();
            a.CheckType = -1;
            a.CheckItem = "Ping";
            a.Inactive = 1;
            a.PriorityLevel = 0;
            a.TestInterval = 5;
            //foreach (ServerCheckItem aaaa in CheckItemNow)
            //{
            //    if (aaaa.ServerIP == a.ServerIP && aaaa.CheckType == a.CheckType && aaaa.CheckItem == a.CheckItem)
            //    {
            //        MessageBox.Show("重复项目");
            //        return;
            //    }
            //}
            foreach (ServerCheckItem aaaa in CheckItemAdd)
            {
                if (aaaa.ServerIP == a.ServerIP && aaaa.CheckType == a.CheckType && aaaa.CheckItem == a.CheckItem)
                {
                    MessageBox.Show("重复项目");
                    return;
                }
            }
            CheckItemAdd.Add(a);
            GetDGADD(CheckItemAdd);
        }
        private void GetDGNOW(string ServerIP)
        {
            Win32ServiceManager wsm = new Win32ServiceManager();
            List<Win32ServiceManager.ServerCheckItem> list = wsm.GetItem(ServerIP);
            if (list != null)
            {
                CheckItemNow.Clear();
                CheckItemNow = list;
            }

            
            DataTable dt = DataTableExtend.ToDataTable<ServerCheckItem>(list);
            //DataTable dtrecord = wsm.GetRecord();
            DGNOW.SetDataSource(dt);
            if (this.DGNOW.Columns.Count == 0)
            {
                this.DGNOW.AddColumn("ServerID", "ServerID", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGNOW.AddColumn("ServerIP", "ServerIP", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGNOW.AddColumn("CheckType", "CheckType", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGNOW.AddColumn("CheckItem", "CheckItem", 50, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGNOW.AddColumn("Inactive", "Inactive", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGNOW.AddColumn("PriorityLevel", "PriorityLevel", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                // 实现列的锁定功能  
                this.DGNOW.Columns[1].Frozen = true;
                //禁止用户改变DataGridView1所有行的行高
                DGNOW.AllowUserToResizeRows = false;
            }
 

        }
        private void GetDGADD(List<Win32ServiceManager.ServerCheckItem> CheckItemAdd)
        {

            DataTable dt = DataTableExtend.ToDataTable<Win32ServiceManager.ServerCheckItem>(CheckItemAdd);
            //DataTable dtrecord = wsm.GetRecord();
            DGADD.SetDataSource(dt);
            if (this.DGADD.Columns.Count == 0)
            {
                this.DGADD.AddColumn("ServerID", "ServerID", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGADD.AddColumn("ServerIP", "ServerIP", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGADD.AddColumn("CheckType", "CheckType", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGADD.AddColumn("CheckItem", "CheckItem", 50, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGADD.AddColumn("Inactive", "Inactive", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGADD.AddColumn("PriorityLevel", "PriorityLevel", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGADD.AddColumn("TestInterval", "TestInterval", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                // 实现列的锁定功能  
                this.DGADD.Columns[1].Frozen = true;
                //禁止用户改变DataGridView1所有行的行高
                DGADD.AllowUserToResizeRows = false;
            }    
        }
        private void btCheckPort_Click(object sender, EventArgs e)
        {
            if (tb_AddPort.Text.ToString().Trim() == "" || tb_AddPort.Text.ToString().Trim() == string.Empty) { MessageBox.Show("请输入端口号"); return; }
            string ServerIP = tb_ServerIP.Text.Trim();
            int ServerPort = Convert.ToInt32(tb_AddPort.Text.Trim());
            if (ServerPort == 0 || ServerPort > 65533) {MessageBox.Show(""); return;}            
            Win32ServiceManager wm = new Win32ServiceManager();
            string s =wm. CmdPing(ServerIP,1);
            if (s != "连接")
            {
                MessageBox.Show(s);
                return;
            }
            string ssss = "";
            if (rbTcpClient.Checked)
            {
                ssss = wm.TcpClientCheck(ServerIP, ServerPort,1);
            }
            else
            {
                ssss = wm.SocketCheck(ServerIP, ServerPort,1);
            }
            if (ssss != "端口打开")
            {
                MessageBox.Show(ssss);
                return;
            }
                Win32ServiceManager.ServerCheckItem a = new ServerCheckItem();
                a.ServerIP = tb_ServerIP.Text.ToString().Trim();
                a.CheckType = 1;
                a.CheckItem = tb_AddPort.Text.Trim();
                a.Inactive = 1;
                a.PriorityLevel = 0;
                a.TestInterval = 10;
                foreach (ServerCheckItem aaaa in CheckItemNow)
                {
                    if (aaaa.ServerIP == a.ServerIP && aaaa.CheckType == a.CheckType && aaaa.CheckItem == a.CheckItem)
                    {
                    MessageBox.Show("重复项目");
                    return;
                    }
                }
                foreach (ServerCheckItem aaaa in CheckItemAdd)
                {
                    if (aaaa.ServerIP == a.ServerIP && aaaa.CheckType == a.CheckType && aaaa.CheckItem == a.CheckItem)
                    {
                        MessageBox.Show("重复项目");
                        return;
                    }
                }
            CheckItemAdd.Add(a);
                GetDGADD(CheckItemAdd);



       }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            Win32ServiceManager wm = new Win32ServiceManager();
            try 
            {
                wm.SaveItem(CheckItemAdd);
                GetDGADD(CheckItemAdd);
                this.DialogResult= DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                this.DialogResult =DialogResult.Cancel;
                throw ex;
            }
            

        }

        private void tb_AddPort_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
                e.Handled = true;
        }


        private void tb_ServiceName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == 8)
            //    e.Handled = false;
            //else
            //    e.Handled = true;
            //e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void btCheckkService_Click(object sender, EventArgs e)
        {
            if (tb_ServiceName.Text.ToString().Trim() == ""|| tb_ServiceName.Text.ToString().Trim() == string.Empty) { MessageBox.Show("请输入服务名称"); return; }
            Win32ServiceManager wm = new Win32ServiceManager();
            string ServerIP = tb_ServerIP.Text.Trim();
            string ServiceName =tb_ServiceName.Text.Trim();

            Win32ServiceManager.ServerCheckItem a = new ServerCheckItem();
            a.ServerIP = ServerIP;
            a.CheckType = 0;
            a.CheckItem = ServiceName;
            a.Inactive = 1;
            a.PriorityLevel = 0;
            a.TestInterval = 10;


            string s = wm.CmdPing(ServerIP,1);
            if (s != "连接")
            {
                MessageBox.Show(s);
                return;
            }
            Array ssss = wm.GetServiceList(ServerIP, "highrock\\administrator", "@pStRy8214", ServiceName);


            if (ssss.GetValue(0, 2).ToString().Trim() == "找不到" || ssss.GetValue(0, 2).ToString().Trim() == "RPC 服务器不可用。")
            { MessageBox.Show(ssss.GetValue(0, 2).ToString().Trim()); txb_State.Text = ssss.GetValue(0, 2).ToString().Trim(); return ; }

            txb_ServiceName.Text = ssss.GetValue(0, 0).ToString().Trim();
            txb_DisplayName.Text = ssss.GetValue(0, 1).ToString().Trim();
            txb_State.Text = ssss.GetValue(0, 2).ToString().Trim();
            txb_StartMode.Text = ssss.GetValue(0, 3).ToString().Trim();



            foreach (ServerCheckItem aaaa in CheckItemNow)
            {
                if (aaaa.ServerIP == a.ServerIP && aaaa.CheckType == a.CheckType && aaaa.CheckItem == a.CheckItem)
                {
                    MessageBox.Show("重复项目");
                    return;
                }
            }
            foreach (ServerCheckItem aaaa in CheckItemAdd)
            {
                if (aaaa.ServerIP == a.ServerIP && aaaa.CheckType == a.CheckType && aaaa.CheckItem == a.CheckItem)
                {
                    MessageBox.Show("重复项目");
                    return;
                }
            }

            CheckItemAdd.Add(a);
            GetDGADD(CheckItemAdd);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tb_ServerIP.Text = "172.16.1.";
            tb_ServiceName.Text = string.Empty;
            tb_AddPort.Text = string.Empty;
            btCheckkService.Enabled = false;
            btCheckPing.Enabled = false;    
            btCheckPort.Enabled = false;
            CheckItemNow.Clear();
            GetDGADD(CheckItemAdd);
            bt_StartService.Text = string.Empty;
            txb_DisplayName.Text = string.Empty;
            txb_State.Text=string.Empty;
            txb_StartMode.Text= string.Empty;
            bt_StartService.Enabled = false;
            bt_StopService.Enabled = false; 
            bt_PauseService.Enabled = false;
            bt_ResumeService.Enabled = false;
            tb_ServerIP.Enabled = true;
            btCheckPing.Enabled = true;

        }

        private void btDelItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckItemAdd.Count; i++)
            {               
               if (CheckItemAdd[i].ServerIP == DGADD.CurrentRow.Cells["ServerIP"].Value.ToString() && CheckItemAdd[i].CheckType == Convert.ToInt32(DGADD.CurrentRow.Cells["CheckType"].Value) && CheckItemAdd[i].CheckItem == DGADD.CurrentRow.Cells["CheckItem"].Value.ToString().Trim())
                {
                    CheckItemAdd.Remove(CheckItemAdd[i]);
                }
            }
            GetDGADD(CheckItemAdd);
        }
    }
}
