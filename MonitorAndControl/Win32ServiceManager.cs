﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TX.Framework.Helper;
using TX.Framework.Security.Base;


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
            if (userName != null && userName.Length > 0)
            {
                ConnectionOptions connectionOptions = new ConnectionOptions();
                connectionOptions.Username = userName;
                connectionOptions.Password = password;
                ManagementScope managementScope = new ManagementScope("\\\\" + host + "\\root\\cimv2", connectionOptions);
                this.managementClass.Scope = managementScope;
            }
        }
        // 验证是否能连接到远程计算机
        public static bool RemoteConnectValidate(string host, string userName, string password)
        {
            ConnectionOptions connectionOptions = new ConnectionOptions();
            connectionOptions.Username = userName;
            connectionOptions.Password = password;
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

            Process p = new Process(); p.StartInfo.FileName = "cmd.exe";//设定程序名

            p.StartInfo.UseShellExecute = false; //关闭Shell的使用

            p.StartInfo.RedirectStandardInput = true;//重定向标准输入

            p.StartInfo.RedirectStandardOutput = true;//重定向标准输出

            p.StartInfo.RedirectStandardError = true;//重定向错误输出

            p.StartInfo.CreateNoWindow = true;//设置不显示窗口

            string pingrst; p.Start(); p.StandardInput.WriteLine("ping " + strIp);

            p.StandardInput.WriteLine("exit");

            string strRst = p.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("无法访问目标主机") != -1)

            {

                pingrst = "无法到达目的主机";

            }

            else if (strRst.IndexOf("(0% loss)") != -1)

            {

                pingrst = "连接";

            }
            else if (strRst.IndexOf("(0% 丢失)") != -1)
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

            //记录时间



            p.Close();

            //Win32ServiceManager.ServerCheckItem a = new ServerCheckItem();
            //a.ServerIP = strIp;
            //a.CheckType = -1;
            //a.CheckItem = "Ping";
            //a.Inactive = 1;
            //a.PriorityLevel = 0;
            //a.ResultSource = 1;
            //a.CheckResult = pingrst;
            //a.ExecutionComputer = DBConn.DataAcess.SqlConn.GetMachineName();
            //a.ExecutionIP = DBConn.DataAcess.SqlConn.GetLocalIP();
            //a.ExecutionTime = DateTime.Now.ToString();
            //SaveRecord(a);



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
            cmd.AppendLine(" SELECT id,ServerIP,CheckType,CheckItem,'' as CheckResult,'' as ExecutionTime,'' as ExecutionComputer,'' as ExecutionIP ");
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
                cmd.AppendLine("            ,[CheckItem] ");
                cmd.AppendLine("            ,[ExecutionTime] ");
                cmd.AppendLine("            ,[ExecutionComputer] ");
                cmd.AppendLine("            ,[ExecutionIP] ");
                cmd.AppendLine("            ,[ResultSource] ");
                cmd.AppendLine("            ,[CheckResult]) ");
                cmd.AppendLine("      VALUES ");
                cmd.AppendLine("            ("+ dt.Rows[j]["id"].ToString().Trim() + ",'" + ServerIP + "' ");
                cmd.AppendLine("            ," + dt.Rows[j]["CheckType"].ToString().Trim() + " ");
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


                    Array aaaa = GetServiceList(ServerIP, "highrock\\administrtor", "@pStRy8214", dt.Rows[j]["CheckItem"].ToString().Trim());
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
        public Array GetServiceList(string ServerIP, string ServerUserName, string ServerPassWord, string CheckItem)
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
                        cmd1.AppendLine("            ,[CheckItem] ");
                        cmd1.AppendLine("            ,[Inactive] ");
                        cmd1.AppendLine("            ,[PriorityLevel]) ");
                        cmd1.AppendLine("      VALUES ");
                        cmd1.AppendLine("            ('" + CheckItemList[i].ServerIP + "' ");
                        cmd1.AppendLine("            ," + CheckItemList[i].CheckType + " ");
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
            string cmd = "select Service_ServerList.id as ServerID, Service_ServerList.ServerIP,Service_ServerList.CheckType,Service_ServerList.CheckItem,Inactive,PriorityLevel,TestInterval,CheckResult,case  when ExecutionTime is NULL then  '2023-01-01 00:00:00.000' else ExecutionTime end as ExecutionTime,ExecutionComputer ,ExecutionIP,ResultSource  \r\nfrom Service_ServerList left join  (select * from(SELECT ROW_NUMBER() over(partition by serverlistid order by id desc) as rowNum ,[ServerListID],[ServerIP],[CheckType],[CheckItem],[CheckResult],[ExecutionTime],[ExecutionComputer],[ExecutionIP],[ResultSource] FROM Service_Record) temp\r\nwhere temp.rowNum = 1) a on Service_ServerList.id = a.ServerListID where inactive=1 order by serverip,checktype,prioritylevel";
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
                string s=CmdPing(ServerCheckItem.ServerIP,0);
                ServerCheckItem.CheckResult = s;


            }
            else if (ServerCheckItem.CheckType == 0)
            {
                Array s = GetServiceList(ServerCheckItem.ServerIP, "highrock\\administrator", "@pStRy8214", ServerCheckItem.CheckItem);
                ServerCheckItem.CheckResult = s.GetValue(0, 2).ToString().Trim();
            }
            else
            {
                string s = TcpClientCheck(ServerCheckItem.ServerIP,Convert.ToInt32(ServerCheckItem.CheckItem),0);
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




        


        }




    }


