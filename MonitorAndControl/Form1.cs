using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MonitorAndControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string ServerIP = "";//服务器IP
        string ServiceName = "";//服务名称
        



        private void Form1_Load(object sender, EventArgs e)
        {
            bt_StartService.Enabled = false;
            bt_PauseService.Enabled = false;
            bt_ResumeService.Enabled = false;
            bt_StopService.Enabled = false;




            this.DGMAIN.AutoGenerateColumns = false; //不给GridView增加扩展列
                                                     //设置隔行背景色

            this.DGMAIN.RowsDefaultCellStyle.BackColor = Color.White;
            this.DGMAIN.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.DGMAIN.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            this.DGMAIN.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;


            GetGridLine();

        }


        
        private void GetGridLine()
        {
            //string strsql = "SELECT [id],[ServerIP],[ServiceName],ServerPort, FROM [dbo].[MES_line] order by eton_line ";
            //DataTable dt_Line = DBConn.DataAcess.SqlConn.Query(strsql).Tables[0];
            //DGMAIN.DataSource = dt_Line;
            if (this.DGMAIN.Columns.Count == 0)
            {
                this.DGMAIN.AddColumn("id", "ID", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null,true);
                this.DGMAIN.AddColumn("ServerIP", "IP", 120, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ServiceName", "ServiceName", 120, true, null, DataGridViewContentAlignment.MiddleLeft, "#0.00", true);
                this.DGMAIN.AddColumn("ServerPort", "ServerPort", 120, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("LastCheckTime", "CheckTime", 120, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("Ping", "Ping", 120, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ServiceState", "ServiceState", 120, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("PortState", "PortState", 120, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                // 实现列的锁定功能  
                this.DGMAIN.Columns[1].Frozen = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txb_ServiceName.Text = "";
            txb_DisplayName.Text = "";
            txb_StartMode.Text = "";
            txb_State.Text = "";
            ServerIP = combo_ServerIP.Text.ToString();
            ServiceName = combo_ServiceName_User.Text.ToString().Trim();


            string s = CmdPing(ServerIP);
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
            GetValue(ServerIP, ServiceName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServerIP = combo_ServerIP.Text.ToString();
            ServiceName = combo_ServiceName_User.Text.ToString().Trim();
            if (ServerIP == "" || ServiceName == "")
            {
                return;
            }

            Win32ServiceManager s = new Win32ServiceManager(ServerIP, "highrock\\administrator", "@pStRy8214");

            s.StartService(ServiceName);
            GetValue(ServerIP, ServiceName);



        }

        private void GetValue(String ServerIP,string ServiceName)
        {
            Win32ServiceManager s = new Win32ServiceManager(ServerIP, "highrock\\administrator", "@pStRy8214");
            //string ServerName1 = "SQLSERVERAGENT";
            Array aaaa = s.GetServiceList(ServiceName);

            //string a=s.GetServiceList(ServerName1).ToString();
            if (aaaa.GetValue(0, 0).ToString().Trim() == "找不到")
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
            }
            else if (aaaa.GetValue(0, 2).ToString() == "Running")
            {
                bt_StartService.Enabled = false;
                bt_PauseService.Enabled = true;
                bt_ResumeService.Enabled = false;
                bt_StopService.Enabled = true;
            }
            else if (aaaa.GetValue(0, 2).ToString() == "Stopped"|| aaaa.GetValue(0, 2).ToString() == "Stop Pending")
            {
                bt_StartService.Enabled = true;
                bt_PauseService.Enabled = false;
                bt_ResumeService.Enabled = false;
                bt_StopService.Enabled = false;
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
            GetValue(ServerIP, ServiceName);


        }






        /////////
        ///

        private Action<string, int> m_checker = null;

        private void TcpClientCheck(string ip, int port)
        {
            IPAddress ipa = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(ipa, port);
            TcpClient tcp = null;

            try
            {
                tcp = new TcpClient();
                tcp.Connect(point);
                MessageBox.Show("端口打开");
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算机端口检测失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (tcp != null)
                {
                    tcp.Close();
                }
            }
        }

        private void SocketCheck(string ip, int port)
        {
            Socket sock = null;

            try
            {
                IPAddress ipa = IPAddress.Parse(ip);
                IPEndPoint point = new IPEndPoint(ipa, port);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(point);
                MessageBox.Show("端口打开");
            }
            catch (SocketException ex)
            {
                MessageBox.Show("计算机端口检测失败，错误消息为：" + ex.Message);
            }
            finally
            {
                if (sock != null)
                {
                    sock.Close();
                    sock.Dispose();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTcpClient.Checked)
            {
                m_checker = TcpClientCheck;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSocket.Checked)
            {
                m_checker = SocketCheck;
            }
        }

        private void btnCheck_Click_1(object sender, EventArgs e)
        {
            if (m_checker != null)
            {
                m_checker(txtIP.Text, Int32.Parse(txtPort.Text));
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = CmdPing("xxerpsvr");
                MessageBox.Show(s);




        }



        /////////



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

            else if (strRst.IndexOf("Destination host unreachable.") != -1 )

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


        //设置单元格颜色
        private void DGVMain_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex > 0)//如果没有这一句渲染是从-1开始的会造成单元格全是黑色
            {
                DataGridViewColumn ThisCl = DGMAIN.Columns[e.ColumnIndex];
                if (ThisCl.Name.Equals("XianshiChengben") || ThisCl.Name.Equals("XianshiLirun") || ThisCl.Name.Equals("XianshiLirunLv"))
                {
                    e.CellStyle.ForeColor = Color.Brown;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ServerIP = combo_ServerIP.Text.ToString();
            ServiceName = combo_ServiceName_User.Text.ToString().Trim();

            if (ServerIP == "")
            {
                return;
            }
            else
            {
                if (R_Service.Checked) { }
            
            }
            StringBuilder cmd = new StringBuilder();
            cmd.Clear();
            cmd.AppendLine(" INSERT INTO [dbo].[Service_ServerList] ");
            cmd.AppendLine("            ([ServerIP] ");
            cmd.AppendLine("            ,[CheckItem] ");
            cmd.AppendLine("            ,[ServiceName] ");
            cmd.AppendLine("            ,[ServerPort]) ");
            cmd.AppendLine("      VALUES ");
            cmd.AppendLine("            (<ServerIP, nvarchar(50),> ");
            cmd.AppendLine("            ,<CheckItem, int,> ");
            cmd.AppendLine("            ,<ServiceName, nvarchar(50),> ");
            cmd.AppendLine("            ,<ServerPort, int,>) ");



        }

        private void R_Service_CheckedChanged(object sender, EventArgs e)
        {
            if (R_Service.Checked) { tb_AddPort.Enabled = false;cb_AddServiceName.Enabled = true; } else { tb_AddPort.Enabled = true; cb_AddServiceName.Enabled = false; }
        }

        private void R_Port_CheckedChanged(object sender, EventArgs e)
        {
            if (R_Service.Checked) { tb_AddPort.Enabled = true; cb_AddServiceName.Enabled = false; } else { tb_AddPort.Enabled = false; cb_AddServiceName.Enabled = true; }
        }
         
    }
}
