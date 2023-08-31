using Azure.Core;
using Mono.Unix.Native;
using MyContrals;
using Python.Runtime;
using Renci.SshNet.Messages;
using Renci.SshNet.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using TX.Framework.DataContract;
using TX.Framework.Helper;
using TX.Framework.Security.Base;
using static IronPython.Modules._ast;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using IronPython.Runtime;


namespace MonitorAndControl
{
    public class Win32ServiceManager
    {
        private string strPath;
        private ManagementClass managementClass;
        public Win32ServiceManager() : this(".", null, null)
        {
        }

        #region Windows服务
        public Win32ServiceManager(string host, string userName, string password)
        {    
            
            this.strPath = "\\\\" + host + "\\root\\cimv2:Win32_Service";
            this.managementClass = new ManagementClass(strPath);
            string key=System.Text.RegularExpressions.Regex.Replace(host, @"[^0-9]+", "");
            if (userName != null && userName.Length > 0)
            {
                ConnectionOptions connectionOptions = new ConnectionOptions();
                
                connectionOptions.Username = DBCon.DBUtility.DESEncrypt.Decrypt(userName, key);
                connectionOptions.Password = DBCon.DBUtility.DESEncrypt.Decrypt(password, key);

                
                ManagementScope managementScope = new ManagementScope("\\\\" + host + "\\root\\cimv2", connectionOptions);
                this.managementClass.Scope = managementScope;
            }
        }
        // 验证是否能连接到远程计算机
        public static bool RemoteConnectValidate(string host, string userName, string password)
        {
            string key = System.Text.RegularExpressions.Regex.Replace(host, @"[^0-9]+", "");
            ConnectionOptions connectionOptions = new ConnectionOptions();
            
            connectionOptions.Username = DBCon.DBUtility.DESEncrypt.Decrypt(userName, key);
            connectionOptions.Password = DBCon.DBUtility.DESEncrypt.Decrypt(password, key);
            ManagementScope managementScope = new ManagementScope("" + host + "//root//cimv2", connectionOptions);
            try
            {
                managementScope.Connect();
            }
            catch
            {
            }
            return managementScope.IsConnected;
        }
        // 获取指定服务属性的值
        public object GetServiceValue(string serviceName, string propertyName)
        {
            ManagementObject mo = this.managementClass.CreateInstance();
            mo.Path = new ManagementPath(this.strPath + ".Name=/" + serviceName + " / ");
            return mo[propertyName];
        }
        // 获取所连接的计算机的所有服务数据
        public string[,] GetServiceList()
        {
            string[,] services = new string[this.managementClass.GetInstances().Count, 4];
            int i = 0;
            foreach (ManagementObject mo in this.managementClass.GetInstances())
            {
                services[i, 0] = (string)mo["Name"];
                services[i, 1] = (string)mo["DisplayName"];
                services[i, 2] = (string)mo["State"];
                services[i, 3] = (string)mo["StartMode"];

                i++;
            }
            //if (services[i, 2] != "Running")
            //{
                
            //    StartService(services[i, 0]);

            //    services[i, 2] = "已尝试重启";
            //}
            return services;
        }
        // 获取所连接的计算机的指定服务数据
        public string[,] GetServiceList(string serverName)
        {
            return GetServiceList(new string[] { serverName });
        }

        // 获取所连接的计算机的的指定服务数据
        public string[,] GetServiceList(string[] serverNames)
        {
            string[,] services = new string[serverNames.Length, 4];


            try
            {
                ManagementObject mo = this.managementClass.CreateInstance();
                for (int i = 0; i < serverNames.Length; i++)
                {
                    //mo.Path = new ManagementPath(this.strPath + ".Name='" + serverNames[i]+ "'");


                    mo.Path = new ManagementPath(this.strPath + ".Name=\"" + serverNames[i] + "\"");
                    services[i, 0] = (string)mo["Name"];
                    services[i, 1] = (string)mo["DisplayName"];
                    services[i, 2] = (string)mo["State"];
                    services[i, 3] = (string)mo["StartMode"];

                    if (services[i, 2] != "Running")
                    {
                        StartService(services[i, 0]);

                        services[i, 2] = "已重新启动";
                    }
                    i++;
                }

            }
            catch (Exception e)
            {

                //throw e;
                services[0, 2] = e.Message.ToString();



            }




            return services;
        }
        // 停止指定的服务
        public string StartService(string serviceName)
        {
            string strRst = null;
            ManagementObject mo = this.managementClass.CreateInstance();
            mo.Path = new ManagementPath(this.strPath + ".Name=\"" + serviceName + "\"");
            try
            {
                if ((string)mo["State"] == "Stopped")//!(bool)mo["AcceptStop"]
                    mo.InvokeMethod("StartService", null);
            }
            catch (ManagementException e)
            {
                strRst = e.Message;
            }
            return strRst;
        }
        // 暂停指定的服务
        public string PauseService(string serviceName)
        {
            string strRst = null;
            ManagementObject mo = this.managementClass.CreateInstance();
            mo.Path = new ManagementPath(this.strPath + ".Name=\"" + serviceName + "\"");
            try
            {
                //判断是否可以暂停
                if ((bool)mo["acceptPause"] && (string)mo["State"] == "Running")
                    mo.InvokeMethod("PauseService", null);
            }
            catch (ManagementException e)
            {
                strRst = e.Message;
            }
            return strRst;
        }
        // 恢复指定的服务
        public string ResumeService(string serviceName)
        {
            string strRst = null;
            ManagementObject mo = this.managementClass.CreateInstance();
            mo.Path = new ManagementPath(this.strPath + ".Name=\"" + serviceName + "\"");
            try
            {
                //判断是否可以恢复
                if ((bool)mo["acceptPause"] && (string)mo["State"] == "Paused")
                    mo.InvokeMethod("ResumeService", null);
            }
            catch (ManagementException e)
            {
                strRst = e.Message;
            }
            return strRst;
        }
        // 停止指定的服务
        public string StopService(string serviceName)
        {
            string strRst = null;
            ManagementObject mo = this.managementClass.CreateInstance();
            mo.Path = new ManagementPath(this.strPath + ".Name=\"" + serviceName + "\"");
            try
            {
                //判断是否可以停止
                if ((bool)mo["AcceptStop"])//(string)mo["State"]=="Running"
                    mo.InvokeMethod("StopService", null);
            }
            catch (ManagementException e)
            {
                strRst = e.Message;
            }
            return strRst;
        }
        #endregion

        #region Ping检测
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIp">服务器IP地址</param>
        /// <param name="ResultSource">0自动，1手动</param>
        /// <returns></returns>
        public string CmdPing(string strIp,int ResultSource)
        {
            Process p = new Process(); 
            p.StartInfo.FileName = "cmd.exe";//设定程序名
            p.StartInfo.UseShellExecute = false; //关闭Shell的使用
            p.StartInfo.RedirectStandardInput = true;//重定向标准输入
            p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
            p.StartInfo.RedirectStandardError = true;//重定向错误输出
            p.StartInfo.CreateNoWindow = true;//设置不显示窗口

            string pingrst; 
            p.Start(); 
            p.StandardInput.WriteLine("ping " + strIp);
            p.StandardInput.WriteLine("exit");

            string strRst = p.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("无法访问目标主机") != -1) {pingrst = "无法到达目的主机";}
            else if (strRst.IndexOf("(0% loss)") != -1){pingrst = "连接";}
            else if (strRst.IndexOf("(0% 丢失)") != -1){pingrst = "连接";}
            else if (strRst.IndexOf("Destination host unreachable.") != -1){pingrst = "无法到达目的主机";}
            else if (strRst.IndexOf("一般故障。") != -1){pingrst = "无法到达目的主机";}
            else if (strRst.IndexOf("Request timed out.") != -1){pingrst = "超时";}
            else if (strRst.IndexOf("请求超时。") != -1){pingrst = "超时";}
            else if (strRst.IndexOf("Unknown host") != -1){pingrst = "无法解析主机";}
            else if (strRst.IndexOf("请求找不到主机") != -1){pingrst = "无法解析主机";}
            else{pingrst = strRst;}
            //记录时间
            p.Close();

            if (pingrst != "连接") 
            {
                Thread.Sleep(5000);
                Process p1 = new Process();
                p1.StartInfo.FileName = "cmd.exe";//设定程序名
                p1.StartInfo.UseShellExecute = false; //关闭Shell的使用
                p1.StartInfo.RedirectStandardInput = true;//重定向标准输入
                p1.StartInfo.RedirectStandardOutput = true;//重定向标准输出
                p1.StartInfo.RedirectStandardError = true;//重定向错误输出
                p1.StartInfo.CreateNoWindow = true;//设置不显示窗口
                
                p1.Start();
                p1.StandardInput.WriteLine("ping " + strIp);
                p1.StandardInput.WriteLine("exit");

                string strRst1 = p1.StandardOutput.ReadToEnd();
                if (strRst.IndexOf("无法访问目标主机") != -1) { pingrst = "无法到达目的主机"; }
                else if (strRst.IndexOf("(0% loss)") != -1) { pingrst = "连接"; }
                else if (strRst.IndexOf("(0% 丢失)") != -1) { pingrst = "连接"; }
                else if (strRst.IndexOf("Destination host unreachable.") != -1) { pingrst = "无法到达目的主机"; }
                else if (strRst.IndexOf("一般故障。") != -1) { pingrst = "无法到达目的主机"; }
                else if (strRst.IndexOf("Request timed out.") != -1) { pingrst = "超时"; }
                else if (strRst.IndexOf("请求超时。") != -1) { pingrst = "超时"; }
                else if (strRst.IndexOf("Unknown host") != -1) { pingrst = "无法解析主机"; }
                else if (strRst.IndexOf("请求找不到主机") != -1) { pingrst = "无法解析主机"; }
                else { pingrst = strRst; }
                //记录时间
                p.Close();
            }
            return pingrst;

        }
        #region Ping显示结果
        //private void displayReply(PingReply reply) //显示结果

        //{

        //    Ping p1 = new Ping(); //只是演示，没有做错误处理

        //    PingReply reply = p1.Send("172.16.1.96");

        //    StringBuilder sbuilder;

        //    if (reply.Status == IPStatus.Success)

        //    {

        //        sbuilder = new StringBuilder();

        //        sbuilder.Append(string.Format("Address: {0} ", reply.Address.ToString()));

        //        sbuilder.Append(string.Format("RoundTrip time: {0} ", reply.RoundtripTime));

        //        sbuilder.Append(string.Format("Time to live: {0} ", reply.Options.Ttl));

        //        sbuilder.Append(string.Format("Don't fragment: {0} ", reply.Options.DontFragment));

        //        sbuilder.Append(string.Format("Buffer size: {0} ", reply.Buffer.Length));

        //        response.write(sbuilder.ToString());

        //    }

        //    else if (reply.Status == IPStatus.TimeOut)

        //    {

        //        response.write("超时");

        //    }
        //    else
        //    {

        //        response.write("失败");

        //    }

        //}
        #endregion
        #endregion

        #region 查询一个IP的所有需要检查的项目
        public Boolean CheckServer_single(string ServerIP)
        {
            //StringBuilder cmd = new StringBuilder();
            //cmd.Clear();//这个IP所有要检查的服务和端口
            //cmd.AppendLine(" SELECT id,ServerIP,CheckType,CheckItem,Inactive,PriorityLevel ");
            //cmd.AppendLine(" ,case when CheckType=1 then CheckItem  else '' end as ServerPort ");
            //cmd.AppendLine(" ,case when  CheckType=0 then CheckItem else '' end as ServiceName  ");
            //cmd.AppendLine(" ,case when  CheckType=-1 then CheckItem else '' end as Ping  ");
            //cmd.AppendLine(" FROM [ServiceManage].[dbo].[Service_ServerList]  ");
            //cmd.AppendLine(" where inactive=1 and serverip='172.16.1.96' ");
            //cmd.AppendLine(" order by CheckType, CheckItem,PriorityLevel ");

            StringBuilder cmd = new StringBuilder();
            cmd.Clear();//这个IP所有要检查的服务和端口
            cmd.AppendLine(" SELECT id,ServerIP,CheckType,CheckItem,SvrUser,SvrPwd,'' as CheckResult,'' as ExecutionTime,'' as ExecutionComputer,'' as ExecutionIP ");
            cmd.AppendLine(" FROM [ServiceManage].[dbo].[Service_ServerList]  ");
            cmd.AppendLine(" where inactive=1 and serverip='" + ServerIP + "' ");
            cmd.AppendLine(" order by CheckType, CheckItem,PriorityLevel ");




            DataTable dt = new DataTable();
            dt = DBConn.DataAcess.SqlConn.Query(cmd.ToString()).Tables[0];
            if (dt.Rows.Count == 0) { return true; }

            ArrayList SQLlist = new ArrayList();

            //1 ping
            string s = CmdPing(ServerIP,1);


            //这里先写入一行ping的日志


            for (int j = 0; j < dt.Rows.Count; j++)
            {
                
                StringBuilder cmd_ping = new StringBuilder();
                cmd.Clear();
                cmd.AppendLine(" INSERT INTO Service_Record ");
                cmd.AppendLine("            ([ServerListID],[ServerIP] ");
                cmd.AppendLine("            ,[CheckType] ");
                cmd.AppendLine("            ,[SvrUser] ");
                cmd.AppendLine("            ,[SvrPwd] ");
                cmd.AppendLine("            ,[CheckItem] ");
                cmd.AppendLine("            ,[ExecutionTime] ");
                cmd.AppendLine("            ,[ExecutionComputer] ");
                cmd.AppendLine("            ,[ExecutionIP] ");
                cmd.AppendLine("            ,[ResultSource] ");
                cmd.AppendLine("            ,[CheckResult]) ");
                cmd.AppendLine("      VALUES ");
                cmd.AppendLine("            ("+ dt.Rows[j]["id"].ToString().Trim() + ",'" + ServerIP + "' ");
                cmd.AppendLine("            ," + dt.Rows[j]["CheckType"].ToString().Trim() + " ");
                cmd.AppendLine("            ," + dt.Rows[j]["SvrUser"].ToString().Trim() + " ");
                cmd.AppendLine("            ," + dt.Rows[j]["SvrPwd"].ToString().Trim() + " ");
                cmd.AppendLine("            ,'" + dt.Rows[j]["CheckItem"].ToString().Trim() + "' ");
                cmd.AppendLine("            ,getdate()");
                cmd.AppendLine("            ,'" + DBConn.DataAcess.SqlConn.GetMachineName() + "' ");
                cmd.AppendLine("            ,'" + DBConn.DataAcess.SqlConn.GetLocalIP() + "'");
                cmd.AppendLine("            ,0");
                string ResultString = "";//查询到的结果CheckResult
                if (Convert.ToInt16(dt.Rows[j]["CheckType"]) == -1)//如果是ping
                {
                    ResultString = s;
                }
                else if (Convert.ToInt16(dt.Rows[j]["CheckType"]) == 0)//如果是Service
                {


                    Array aaaa = GetServiceList(ServerIP, dt.Rows[j]["SvrUser"].ToString().Trim(), dt.Rows[j]["SvrPwd"].ToString().Trim(), dt.Rows[j]["CheckItem"].ToString().Trim());
                    ResultString = aaaa.GetValue(0, 2).ToString().Trim();
                    
                }
                else if (Convert.ToInt16(dt.Rows[j]["CheckType"]) == 1)//如果是Port
                {
                    ResultString = TcpClientCheck(ServerIP, Convert.ToInt32(dt.Rows[j]["CheckItem"].ToString().Trim()),1);
                }
                cmd.AppendLine("            ,'" + ResultString + "') ");
                SQLlist.Add(cmd.ToString());
            }


            try
            {
                DBConn.DataAcess.SqlConn.ExecuteSqlTran(SQLlist);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        #endregion

        #region 获取服务的运行状态
        /// <summary>
        /// 获取服务的运行状态
        /// </summary>
        /// <param name="ServerIP">服务器地址</param>
        /// <param name="ServerUserName">用户名</param>
        /// <param name="ServerPassWord">密码</param>
        /// <param name="CheckItem">服务名称</param>
        /// <returns>Array</returns>
        public Array GetServiceList(string ServerIP,string ServerUserName, string ServerPassWord,   string CheckItem)
        {
            Win32ServiceManager servicecheck = new Win32ServiceManager(ServerIP, ServerUserName, ServerPassWord);
            Array aaaa = servicecheck.GetServiceList(CheckItem);
            return aaaa;
        }
        #endregion

        #region 端口检测
        public string TcpClientCheck(string ip, int port,int ResultSource)
        {
            IPAddress ipa = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(ipa, port);
            TcpClient tcp = null;
            Win32ServiceManager.ServerCheckItem a = new ServerCheckItem();
            a.ServerIP = ip;
            a.CheckType = 1;
            a.CheckItem = port.ToString();
            a.Inactive = 1;
            a.PriorityLevel = 0;
            a.ResultSource = ResultSource;
            
            a.ExecutionComputer = DBConn.DataAcess.SqlConn.GetMachineName();
            a.ExecutionIP = DBConn.DataAcess.SqlConn.GetLocalIP();
            a.ExecutionTime = DateTime.Now.ToString();
            
            try
            {
                tcp = new TcpClient();
                tcp.Connect(point);
                a.CheckResult = "端口打开";
                SaveRecord(a);
                return "端口打开";
            }
            catch (Exception ex)
            {
                SaveRecord(a);
                return "计算机端口检测失败，错误消息为：" + ex.Message;
            }
            finally
            {
                if (tcp != null)
                {
                    tcp.Close();
                }
            }
        }

        public string SocketCheck(string ip, int port, int ResultSource)
        {
            Socket sock = null;
            Win32ServiceManager.ServerCheckItem a = new ServerCheckItem();
            a.ServerIP = ip;
            a.CheckType = 1;
            a.CheckItem = port.ToString();
            a.Inactive = 1;
            a.PriorityLevel = 0;
            a.ResultSource = ResultSource;

            a.ExecutionComputer = DBConn.DataAcess.SqlConn.GetMachineName();
            a.ExecutionIP = DBConn.DataAcess.SqlConn.GetLocalIP();
            a.ExecutionTime = DateTime.Now.ToString();
            try
            {
                IPAddress ipa = IPAddress.Parse(ip);
                IPEndPoint point = new IPEndPoint(ipa, port);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(point);
                a.CheckResult = "端口打开";
                SaveRecord(a);
                return "端口打开";
            }
            catch (SocketException ex)
            {
                SaveRecord(a);
                return "计算机端口检测失败，错误消息为：" + ex.Message;
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

        #endregion

        #region 保存需要检查的项目
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServerIP"></param>
        /// <param name="CheckType"></param>
        /// <param name="CheckItem"></param>
        /// <returns>返回id</returns>
        public int SaveItem(string ServerIP, int CheckType, string CheckItem)
        {
            //先查一下有没有重复行
            string ss = "SELECT  id,Inactive  FROM Service_ServerList where ServerIP = '" + ServerIP + "' and CheckType = '" + CheckType + "' and CheckItem = '" + CheckItem + "'";
            DataTable dt = DBConn.DataAcess.SqlConn.Query(ss).Tables[0];

            if (dt.Rows.Count > 0) //如果有，就激活
            {
                string updatestring = "UPDATE Service_ServerList SET Inactive=1 WHERE id=" + dt.Rows[0]["id"].ToString().Trim();
                return Convert.ToInt32(dt.Rows[0]["id"].ToString().Trim()); 
            }
            else //如果没有就添加
            {
                StringBuilder cmd1 = new StringBuilder();
                cmd1.Clear();
                cmd1.AppendLine(" INSERT INTO [dbo].[Service_ServerList] ");
                cmd1.AppendLine("            ([ServerIP] ");
                cmd1.AppendLine("            ,[CheckType] ");
                cmd1.AppendLine("            ,[CheckItem] ");
                cmd1.AppendLine("            ,[Inactive] ");
                cmd1.AppendLine("            ,[PriorityLevel]) ");
                cmd1.AppendLine("      VALUES ");
                cmd1.AppendLine("            ('" + ServerIP + "' ");
                cmd1.AppendLine("            ," + CheckType + " ");
                cmd1.AppendLine("            ,'" + CheckItem + "' ");
                cmd1.AppendLine("            ,1 ");
                cmd1.AppendLine("            ,0) ");
                DBConn.DataAcess.SqlConn.Query(cmd1.ToString());
                DataTable dt1 = DBConn.DataAcess.SqlConn.Query(ss).Tables[0];
                return Convert.ToInt32(dt1.Rows[0]["id"].ToString().Trim());
            }
        }
        #endregion

        #region 保存需要检查的项目-传入List<ServerCheckItem>
        /// <summary>
        /// 保存需要检查的项目-传入List<ServerCheckItem>
        /// </summary>
        /// <param name="CheckItem">"List<ServerCheckItem>"</param>
        /// <returns>boolean</returns>
        public Boolean SaveItem(List<ServerCheckItem> CheckItemList)
        {
            try
            {
                for (int i = 0; i < CheckItemList.Count; i++)
                {
                    //先查一下有没有重复行
                    string ss = "SELECT  id,Inactive  FROM Service_ServerList where ServerIP = '" + CheckItemList[i].ServerIP + "' and CheckType = '" + CheckItemList[i].CheckType + "' and CheckItem = '" + CheckItemList[i].CheckItem + "'";
                    DataTable dt = DBConn.DataAcess.SqlConn.Query(ss).Tables[0];

                    if (dt.Rows.Count > 0) //如果有，就激活
                    {
                        string updatestring = "UPDATE Service_ServerList SET Inactive=1 WHERE id=" + dt.Rows[0]["id"].ToString().Trim();
                        CheckItemList[i].ServerID = Convert.ToInt32(dt.Rows[0]["id"].ToString().Trim());
                    }
                    else //如果没有就添加
                    {
                        StringBuilder cmd1 = new StringBuilder();
                        cmd1.Clear();
                        cmd1.AppendLine(" INSERT INTO [dbo].[Service_ServerList] ");
                        cmd1.AppendLine("            ([ServerIP] ");
                        cmd1.AppendLine("            ,[CheckType] ");
                        cmd1.AppendLine("            ,[SvrUser] ");
                        cmd1.AppendLine("            ,[SvrPwd] ");
                        cmd1.AppendLine("            ,[CheckItem] ");
                        cmd1.AppendLine("            ,[Inactive] ");
                        cmd1.AppendLine("            ,[PriorityLevel]) ");
                        cmd1.AppendLine("      VALUES ");
                        cmd1.AppendLine("            ('" + CheckItemList[i].ServerIP + "' ");
                        cmd1.AppendLine("            ," + CheckItemList[i].CheckType + " ");
                        cmd1.AppendLine("            ,'" + CheckItemList[i].SvrUser + "' ");
                        cmd1.AppendLine("            ,'" + CheckItemList[i].SvrPwd + "' ");
                        cmd1.AppendLine("            ,'" + CheckItemList[i].CheckItem + "' ");
                        cmd1.AppendLine("            ,1 ");
                        cmd1.AppendLine("            ,0) ");
                        DBConn.DataAcess.SqlConn.Query(cmd1.ToString());
                        DataTable dt1 = DBConn.DataAcess.SqlConn.Query(ss).Tables[0];
                        CheckItemList[i].ServerID = Convert.ToInt32(dt1.Rows[0]["id"].ToString().Trim());
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        #endregion

        #region 获取现有的所有记录

        /// <summary>
        /// 获取现有的所有记录
        /// </summary>
        /// <returns>datatable</returns>
        public DataTable GetRecord()
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Clear();
            cmd.AppendLine(" /****** SSMS 的 SelectTopNRows 命令的脚本  ******/ ");
            cmd.AppendLine(" SELECT a.[ServerIP] ");
            cmd.AppendLine("       ,a.[CheckType] ");
            cmd.AppendLine("       ,a.[CheckItem] ");
            cmd.AppendLine(" 	  ,case when a.[CheckType]=-1 then a.[CheckItem] else '' end as Ping  ");
            cmd.AppendLine(" 	  ,case when a.[CheckType]=0 then a.[CheckItem] else '' end as ServiceName ");
            cmd.AppendLine(" 	  ,case when a.[CheckType]=1 then a.[CheckItem] else '' end as Port ");
            cmd.AppendLine("       ,a.[CheckResult] ");
            cmd.AppendLine(" 	  ,b.Inactive ");
            cmd.AppendLine(" 	  ,b.PriorityLevel ");
            cmd.AppendLine(" 	  ,b.TestInterval ");
            cmd.AppendLine("       ,a.[ExecutionTime] ");
            cmd.AppendLine("       ,a.[ExecutionComputer] ");
            cmd.AppendLine("       ,a.[ExecutionIP] ");
            cmd.AppendLine("       ,a.[ResultSource] ");
            cmd.AppendLine("   FROM Service_Record a ");
            cmd.AppendLine("   left join Service_ServerList b  ");
            cmd.AppendLine("   on a.ServerIP=b.ServerIP and a.CheckType=b.CheckType and a.CheckItem=b.CheckItem ");
            cmd.AppendLine("   where b.Inactive=1 ");
            cmd.AppendLine("   order by a.ServerIP,a.CheckType,a.CheckItem,b.PriorityLevel ");
            cmd.AppendLine("   ");
            return DBConn.DataAcess.SqlConn.Query(cmd.ToString()).Tables[0];           
        }
        #endregion

        #region 单个IP所有要检测的项目
        /// <summary>
        /// 单个IP所有要检测的项目
        /// </summary>
        /// <param name="ServerIP"></param>
        /// <returns>DataTable</returns>
        public DataTable GetRecord(string ServerIP)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Clear();
            cmd.AppendLine(" /****** SSMS 的 SelectTopNRows 命令的脚本  ******/ ");
            cmd.AppendLine(" SELECT a.[ServerIP] ");
            cmd.AppendLine("       ,a.[CheckType] ");
            cmd.AppendLine("       ,a.[CheckItem] ");
            cmd.AppendLine(" 	  ,case when a.[CheckType]=-1 then a.[CheckItem] else '' end as Ping  ");
            cmd.AppendLine(" 	  ,case when a.[CheckType]=0 then a.[CheckItem] else '' end as ServiceName ");
            cmd.AppendLine(" 	  ,case when a.[CheckType]=1 then a.[CheckItem] else '' end as Port ");
            cmd.AppendLine("       ,a.[CheckResult] ");
            cmd.AppendLine(" 	  ,b.Inactive ");
            cmd.AppendLine(" 	  ,b.PriorityLevel ");
            cmd.AppendLine(" 	  ,b.TestInterval ");
            cmd.AppendLine("       ,a.[ExecutionTime] ");
            cmd.AppendLine("       ,a.[ExecutionComputer] ");
            cmd.AppendLine("       ,a.[ExecutionIP] ");
            cmd.AppendLine("       ,a.[ResultSource] ");
            cmd.AppendLine("   FROM Service_Record a ");
            cmd.AppendLine("   left join Service_ServerList b  ");
            cmd.AppendLine("   on a.ServerIP=b.ServerIP and a.CheckType=b.CheckType and a.CheckItem=b.CheckItem ");
            cmd.AppendLine("   where b.Inactive=1 and a.ServerIP='"+ ServerIP + "'");
            cmd.AppendLine("   order by a.ServerIP,a.CheckType,a.CheckItem,b.PriorityLevel ");
            cmd.AppendLine("   ");
            return DBConn.DataAcess.SqlConn.Query(cmd.ToString()).Tables[0];
        }
        #endregion

        #region 获取现有需要检测的项目并查询
        /// <summary>
        /// 获取现有需要检测的项目并查询
        /// </summary>
        /// <returns></returns>
        public List<ServerCheckItem> GetItem()
        {
            
            string cmd = "select Service_ServerList.id as ServerID, Service_ServerList.ServerIP,Service_ServerList.CheckType,Service_ServerList.SvrUser,Service_ServerList.SvrPwd,Service_ServerList.CheckItem,Inactive,PriorityLevel,TestInterval,CheckResult,case  when ExecutionTime is NULL then  '2023-01-01 00:00:00.000' else ExecutionTime end as ExecutionTime,ExecutionComputer ,ExecutionIP,ResultSource  \r\nfrom Service_ServerList left join  (select * from(SELECT ROW_NUMBER() over(partition by serverlistid order by id desc) as rowNum ,[ServerListID],[ServerIP],[CheckType],[CheckItem],[CheckResult],[ExecutionTime],[ExecutionComputer],[ExecutionIP],[ResultSource] FROM Service_Record) temp\r\nwhere temp.rowNum = 1) a on Service_ServerList.id = a.ServerListID where inactive=1 order by serverip,checktype,prioritylevel";
            DataTable dt= DBConn.DataAcess.SqlConn.Query(cmd).Tables[0];
            
            List<ServerCheckItem> serverCheckItems = new List<ServerCheckItem>();
            if (dt.Rows.Count> 0)
            {   
                serverCheckItems = dt.ToDataList<ServerCheckItem>();
                //for (int i = 0; i < serverCheckItems.Count-1; i++)
                //{
                //    serverCheckItems[i] = CheckLine(serverCheckItems[i]);
                //}
            }
            return serverCheckItems;
        }
        #endregion

        #region 获取ServerIP现有需要检测的项目
        /// <summary>
        /// 获取ServerIP现有需要检测的项目
        /// </summary>
        /// <returns></returns>
        public List<ServerCheckItem> GetItem(string ServerIP)
        {
            string cmd = "SELECT id as ServerID      ,ServerIP      ,CheckType      ,CheckItem      ,Inactive      ,PriorityLevel      ,TestInterval,'' as CheckResult,'' as ExecutionTime,'' as ExecutionComputer ,'' as ExecutionIP,0 as ResultSource  FROM Service_ServerList where inactive=1 and ServerIP='"+ ServerIP + "' order by serverip,checktype,prioritylevel";
            DataTable dt = DBConn.DataAcess.SqlConn.Query(cmd).Tables[0];
            List<ServerCheckItem> serverCheckItems = dt.ToDataList<ServerCheckItem>();

            return serverCheckItems;
        }
        #endregion

        #region 检测一行
        /// <summary>
        /// 检测一行
        /// </summary>
        /// <param name="ServerCheckItem"></param>
        /// <param name="ResultSource">0自动，1手动</param>
        /// <returns></returns>
        public ServerCheckItem CheckLine(ServerCheckItem ServerCheckItem)
        {
            if (ServerCheckItem.CheckType == -1)
            {
                string s = CmdPing(ServerCheckItem.ServerIP, 0);
                ServerCheckItem.CheckResult = s;


            }
            else if (ServerCheckItem.CheckType == 0)
            {
                Array s = GetServiceList(ServerCheckItem.ServerIP, ServerCheckItem.SvrUser, ServerCheckItem.SvrPwd, ServerCheckItem.CheckItem);
                ServerCheckItem.CheckResult = s.GetValue(0, 2).ToString().Trim();
            }
            else if (ServerCheckItem.CheckType == -2)
            {
                string[] strArray = ServerCheckItem.CheckItem.ToString().Trim().Split('!');
                string _disksrc = strArray[0];
                string _threshold = strArray[1];
                string message = GetDiskSize(ServerCheckItem.ServerIP, ServerCheckItem.SvrUser, ServerCheckItem.SvrPwd, _disksrc, _threshold);
                if (message == "")
                {
                    message = "正常";
                }
                ServerCheckItem.CheckResult = message;
            }
            else if (ServerCheckItem.CheckType == -3)
            {

                string[] strArray = ServerCheckItem.CheckItem.ToString().Trim().Split('!');
                int _port = (int)Convert.ToInt64(strArray[0]);
                string _ServiceName = strArray[1];
                string ServiceStatus = LinuxGetServicesInfo(ServerCheckItem.ServerIP, _port, ServerCheckItem.SvrUser, ServerCheckItem.SvrPwd, _ServiceName);
                ServerCheckItem.CheckResult = ServiceStatus;
                //a = "正常";           
                //a = "异常 已恢复";
                //a = "异常 未能恢复";
                //a =  "异常 没有找到这个服务"
                
            }
            else if (ServerCheckItem.CheckType == -4)
            {
                string[] strArray = ServerCheckItem.CheckItem.ToString().Trim().Split('!');
                int _port = (int)Convert.ToInt64(strArray[0]);
                string _path = strArray[1];
                DiskInfo diskInfo = new DiskInfo();
                diskInfo = LinuxGetFolderDiskInfo(ServerCheckItem.ServerIP,_port,ServerCheckItem.SvrUser,ServerCheckItem.SvrPwd,_path);
                
                
                string result = System.Text.RegularExpressions.Regex.Replace(diskInfo.Use.ToString(), @"[^0-9]+", "");
                float b = Convert.ToSingle(result);
                if (b < 15)
                {
                    ServerCheckItem.CheckResult = diskInfo.Use.ToString();
                }
                else
                {
                    ServerCheckItem.CheckResult = "正常";
                }

            }
            else
            {
                string s = TcpClientCheck(ServerCheckItem.ServerIP, Convert.ToInt32(ServerCheckItem.CheckItem), 0);
                ServerCheckItem.CheckResult = s;
            }
            ServerCheckItem.ExecutionComputer = DBConn.DataAcess.SqlConn.GetMachineName();
            ServerCheckItem.ExecutionIP = DBConn.DataAcess.SqlConn.GetLocalIP();
            ServerCheckItem.ExecutionTime = DateTime.Now.ToString();
            ServerCheckItem.ResultSource = 0;
            SaveRecord(ServerCheckItem);
            try
            {                
                return ServerCheckItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public ServerCheckItem SaveRecord(ServerCheckItem ServerCheckItem)
        {
            StringBuilder cmd = new StringBuilder();
            cmd.Clear();
            cmd.AppendLine(" INSERT INTO [dbo].[Service_Record] ");
            cmd.AppendLine("            ([ServerListID] ");
            cmd.AppendLine("            ,[ServerIP] ");
            cmd.AppendLine("            ,[CheckType] ");
            cmd.AppendLine("            ,[CheckItem] ");
            cmd.AppendLine("            ,[CheckResult] ");
            cmd.AppendLine("            ,[ExecutionTime] ");
            cmd.AppendLine("            ,[ExecutionComputer] ");
            cmd.AppendLine("            ,[ExecutionIP] ");
            cmd.AppendLine("            ,[ResultSource]) ");
            cmd.AppendLine("      VALUES ");
            cmd.AppendLine("            (" + ServerCheckItem.ServerID + "");
            cmd.AppendLine("            ,'" + ServerCheckItem.ServerIP + "' ");
            cmd.AppendLine("            ," + ServerCheckItem.CheckType + " ");
            cmd.AppendLine("            ,'" + ServerCheckItem.CheckItem + "'  ");
            cmd.AppendLine("            ,'" + ServerCheckItem.CheckResult + "' ");
            cmd.AppendLine("            ,'" + ServerCheckItem.ExecutionTime + "' ");
            cmd.AppendLine("            ,'" + ServerCheckItem.ExecutionComputer + "' ");
            cmd.AppendLine("            ,'" + ServerCheckItem.ExecutionIP + "'  ");
            cmd.AppendLine("            ,'" + ServerCheckItem.ResultSource + "' ) ");
            try
            {
                DBConn.DataAcess.SqlConn.Query(cmd.ToString());
                return ServerCheckItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion

        /// <summary>
        /// 从RandomWords表里随机获取一行文字
        /// </summary>
        /// <returns></returns>
        public string GetRandomWords()
        {
            string cmd = "SELECT TOP 1 Words FROM RandomWords ORDER BY NEWID()";
            DataTable dt= DBConn.DataAcess.SqlConn.Query(cmd.ToString()).Tables[0];
            if (dt.Rows.Count == 0){
                return "没毛病";
            }else{
                return dt.Rows[0]["words"].ToString();
            }
        }

         

        public class ServerCheckItem
        {
            //int _ServerID;
            //string _ServerIP;
            //int _CheckType;
            //string _CheckItem;
            //int _Inactive;
            //int _PriorityLevel;
            //int _TestInterval;
            //string _CheckResult;
            //string _ExecutionTime;
            //string _ExecutionComputer;
            //string _ExecutionIP;
            //string _ResultSource;

            public int ServerID
            {
                get;
                set;

            }

            public string ServerIP
            {
                get;
                set;
            }

            public int CheckType
            {
                get;
                set;
            }

            public string CheckItem
            {
                get;
                set;
            }

            public string SvrUser
            {
                get;
                set;
            }

            public string SvrPwd
            {
                get;
                set;
            }

            public int Inactive
            {
                get;
                set;
            }

            public int PriorityLevel
            {
                get;
                set;
            }

            public int TestInterval
            {
                get;
                set;
            }

            public string CheckResult
            {
                get;
                set;
            }

            public string ExecutionTime
            {
                get;
                set;
            }

            public string ExecutionComputer
            {
                get;
                set;
            }

            public string ExecutionIP
            {
                get;
                set;
            }

            public int ResultSource
            {
                get;
                set;
            }


        }


        #region 监控磁盘大小

        /// <summary>
        /// 监控磁盘空间，到达阈值报警
        /// </summary>
        /// <param name="_ip">IP地址</param>
        /// <param name="_username">用户</param>
        /// <param name="_password">密码</param>
        /// <param name="_disksrc">盘符 只写盘符不需要冒号</param>
        /// <param name="_threshold">阈值 小于多少GB报警</param>
        /// <returns></returns>
        private string GetDiskSize(string _ip, string _username, string _password, string _disksrc,string _threshold)
        {
            //ip = textBoxSrcPath.Text;//"172.16.5.214";
            //disksrc = textBoxDirPath.Text + ":";//"D:";
            //username = txtUsername.Text;//"Administrator"; 
            //password = txtPassword.Text;//"password";

            string ip = _ip;//"172.16.5.214";

            string disksrc = _disksrc + ":";//"D:";

            string key = System.Text.RegularExpressions.Regex.Replace(_ip, @"[^0-9]+", "");

            string username = DBCon.DBUtility.DESEncrypt.Decrypt(_username.Trim(), key);//"Administrator"; //

            string password = DBCon.DBUtility.DESEncrypt.Decrypt(_password.Trim(), key);//"password";

            int threshold = Convert.ToInt16(_threshold);

            long freesize = 0, size = 0;

            long gb = 1024 * 1024 * 1024;

            ConnectionOptions connectionOptions = new ConnectionOptions();

            connectionOptions.Username = username;

            connectionOptions.Password = password;

            connectionOptions.Timeout = new TimeSpan(1, 1, 1, 1);//连接时间



            //ManagementScope 的服务器和命名空间。

            string path = string.Format("\\\\{0}\\root\\cimv2", ip);

            //表示管理操作的范围（命名空间）,使用指定选项初始化ManagementScope 类的、表示指定范围路径的新实例。

            ManagementScope scope = new ManagementScope(path, connectionOptions);

            scope.Connect();

            //查询字符串，某磁盘上信息

            string strQuery = string.Format("select * from Win32_LogicalDisk where deviceid='{0}'", disksrc);


            ObjectQuery query = new ObjectQuery(strQuery);

            //查询ManagementObjectCollection返回结果集
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject m in searcher.Get())
            {
                if (m["Name"].ToString() == disksrc)
                {   //通过m["属性名"]
                    freesize = Convert.ToInt64(m["FreeSpace"]) / gb;
                    size = Convert.ToInt64(m["Size"]) / gb;
                }
            }
            string lbMsg = freesize.ToString();
            string messagea = "";
            if (freesize <= threshold)
            {

                messagea = messagea + "可用空间" + freesize + "GB";
            }

            return messagea;

        }











        ///  
        /// 获取指定驱动器的空间总大小(单位为B)
        ///  
        ///  只需输入代表驱动器的字母即可 （大写）
        ///   
        public static long GetHardDiskSpace(string str_HardDiskName)
        {
            long totalSize = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                }
            }
            return totalSize;
        }

        ///  
        /// 获取指定驱动器的剩余空间总大小(单位为B)
        ///  
        ///  只需输入代表驱动器的字母即可 
        ///   
        public static long GetHardDiskFreeSpace(string str_HardDiskName)
        {
            long freeSpace = new long();
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                }
            }
            return freeSpace;
        }

        #endregion 监控磁盘大小

        #region C#在Linux下获取服务信息
        public string LinuxGetServicesInfo(string _hostname, int _port, string _username, string _password, string _ServiceName)
        {
            string a = "";
            string key = System.Text.RegularExpressions.Regex.Replace(_hostname, @"[^0-9]+", "");
            string username = DBCon.DBUtility.DESEncrypt.Decrypt(_username,key);
            string password = DBCon.DBUtility.DESEncrypt.Decrypt(_password, key);

            using (var client = new Renci.SshNet.SshClient(_hostname, _port, username, password))
            {

                // 连接到 SSH 服务器
                client.Connect();
                
                // 在此处执行 SSH 操作...                
                try
                {
                    if (string.IsNullOrEmpty(_ServiceName))
                    {
                        return a;
                    }

                    var commandResult_ServiceStatus = client.RunCommand(string.Format("systemctl status {0}", _ServiceName));

                    string m3 = commandResult_ServiceStatus.Result;
                    if (m3 == "")
                    {
                        a = "异常 没有找到这个服务";
                    }
                    else
                    {

                        string[] arr = m3.Split('\n');
                        if (arr.Length == 0)
                        {
                            a = "获取状态失败";
                        }
                        else 
                        {
                            string ServiceStatus = MidStrEx(arr[2].Trim(), " (", ") ");
                            if (ServiceStatus == "running")
                            {
                                a = "正常";
                            }
                            else
                            {
                                var commandResult_ServiceRestartStatus1=client.RunCommand(string.Format("systemctl restart {0}", _ServiceName));
                                Thread.Sleep(3000);
                                var commandResult_ServiceRestartStatus = client.RunCommand(string.Format("systemctl status {0}", _ServiceName));
                                string m4 = commandResult_ServiceRestartStatus.Result;
                                string[] arr_r = m4.Split('\n');
                                string ServiceRestartStatus = MidStrEx(arr_r[2].Trim(), " (", ") ");
                                
                                if (ServiceRestartStatus == "running")
                                {
                                    a = "异常 已恢复";
                                }
                                else
                                {
                                    a = "异常 未能恢复";
                                }
                            }
                        }
                     
                    }

                    return a;
                }
                catch (Exception ex)
                {
                    // 断开 SSH 连接
                    client.Disconnect();
                    //logger.Error(ex);

                    return ex.ToString();
                }
            }


        }
        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                //Log.WriteLog("MidStrEx Err:" + ex.Message);
            }
            return result;
        }
        #endregion



        #region C#在Linux下获取文件夹信息（所在磁盘总大小，使用空间，已用空间，使用率）

        public DiskInfo LinuxGetFolderDiskInfo(string hostname, int port, string username, string password, string path)
        {
            using (var client = new Renci.SshNet.SshClient(hostname, port, DBCon.DBUtility.DESEncrypt.Decrypt(username, System.Text.RegularExpressions.Regex.Replace(hostname, @"[^0-9]+", "")), DBCon.DBUtility.DESEncrypt.Decrypt(password, System.Text.RegularExpressions.Regex.Replace(hostname, @"[^0-9]+", ""))))
            {
                // 连接到 SSH 服务器
                client.Connect();
                DiskInfo disk = new DiskInfo();
                // 在此处执行 SSH 操作...                
                try
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        return disk;
                    }
                    if (!path.StartsWith("/"))
                    {
                        path = "/" + path;
                    }
                    var commandResult_ls = client.RunCommand("ls -l");
                    string shellPathLine = string.Format("cd {0}", path);
                    string printLine = " awk '{print $2,$3,$4,$5}'";
                    string shellLine = string.Format("df -k {0} |", path) + printLine;
                    var commandResult_df_k = client.RunCommand(string.Format("df -k {0} |", path) + printLine);
                    string m3 = commandResult_df_k.Result;

                    string[] arr= m3.Split('\n');
                    if (arr.Length == 0)
                    {
                        return disk;
                    }
                    string[] resultArray = arr[1].TrimStart().TrimEnd().Split(' ');
                    if (resultArray == null || resultArray.Length == 0)
                    {
                        return disk;
                    }
                    disk.TotalSize = Convert.ToInt64(resultArray[0]);
                    disk.UsedSize = Convert.ToInt64(resultArray[1]);
                    disk.AvailableSize = Convert.ToInt64(resultArray[2]);
                    disk.Use = resultArray[3];
                    return disk;
                }
                catch (Exception ex)
                {
                    // 断开 SSH 连接
                    client.Disconnect();
                    //logger.Error(ex);
                    return disk;
                }
            }      
        }
     
        /// <summary>
        /// Linux磁盘信息
        /// </summary>
        public class DiskInfo
        {
            public long TotalSize { get; set; }

            public long UsedSize { get; set; }

            public long AvailableSize { get; set; }

            public string Use { get; set; }
        }

        #endregion


        #region HTTP
        /// <summary>
        /// 调取笑话
        /// </summary>
        /// <returns></returns>
        public string GetJoke()
        {
            string resultstr = "";
            // 创建一个Web请求
            HttpWebRequest request = WebRequest.Create("http://kr1.sickkle.com/joke") as HttpWebRequest;

            // 获取Web服务器输出的数据
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // 取得输出流
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string a = reader.ReadToEnd();

                


                JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类

                Joke list = js.Deserialize<Joke>(a);    //将json数据转化为对象类型并赋值给list
                string msg = list.msg;
                string sign = list.sign;
                byte[] Text = System.Text.Encoding.Default.GetBytes(msg);
                byte[] key = System.Text.Encoding.Default.GetBytes("5a75d5ec839a8f1ed686f0ddb67d5f09");
                byte[] iv = System.Text.Encoding.Default.GetBytes("f244ef6f0accec87");
                byte[] b = DBConn.DBUtility.AESEncrypt.Decrypt(a, "5a75d5ec839a8f1ed686f0ddb67d5f09", "f244ef6f0accec87");
                resultstr = System.Text.Encoding.Default.GetString(b);
            }
            return resultstr;


            
                            //{ "msg":"efcf72a487f9855214d40286e7eef451399d8d0b4e8c10394bf508d1b83a4d4c1df069e769851d06edd2c54b094388e810b131f47c853807ef578bd097cc69ecc8fbd8e5b80c6095fa06030f47036fdb929e73c428f530b7751bc66d9eb99bc035a0006af81eb3d2e08774d549","sign":"dc3479ff294fbb89de0beeb38747636f"}
                            
                            //用这个 md5(msg+aes_iv)就能算出签名，和返回的sign比一比是不是一样
                            //如果是一样，就用AES解密msg，就能得到原文了
                            //AES256，密钥和向量刚发给你了，我再发一遍
                            //aes_key = '5a75d5ec839a8f1ed686f0ddb67d5f09'
                            //aes_iv = 'f244ef6f0accec87'
                            //md5(Stoney)
                            //向量是md5(Yves)的前16位

        }
  
        public class Joke
        {
            public string msg { get; set; }

            public string sign { get; set; }
            
        }
     
        #endregion

    }




}


