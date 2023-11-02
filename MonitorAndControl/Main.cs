using email;
using IronPython.Runtime.Exceptions;
using Microsoft.Office.Interop.Excel;
using Microsoft.Scripting.Utils;
using Microsoft.Win32.TaskScheduler;
using Org.BouncyCastle.Utilities.Encoders;
using Renci.SshNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Win32;
using System.Windows.Forms;
using TX.Framework.Helper;
using static DBConn.DBUtility.AESEncrypt;
using static IronPython.Modules._ast;
using static MonitorAndControl.Win32ServiceManager;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Action = System.Action;
using NPOI; 

namespace MonitorAndControl
{
    public partial class Main : Form
    {
        List<ServerCheckItem> list = new List<ServerCheckItem>();

        private TProcess m_process;


        /// <summary>
        /// 连续5次(1/4小时)没有报错就发送一个笑话
        /// </summary>
        int NoErr = 1;
        string str6oclock = "";

        public Main()
        {
            InitializeComponent();
            m_process = new TProcess();
            m_process.MessageBoxShowHandler = MessageBoxShow;
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerAdd a = new ServerAdd();
            a.ShowDialog();
        }
        bool working = false;

        private void Main_Load(object sender, EventArgs e)
        {
            string a = DBCon.DBUtility.DESEncrypt.Encrypt("root", "123");
            string b = DBCon.DBUtility.DESEncrypt.Encrypt("root", "321");

            working = false;
            timer1.Enabled = true;
            timer1.Interval = 5 * 60000; //设置时间间隔（毫秒为单位）单位Ms
            this.DGMAIN.RowsDefaultCellStyle.BackColor = Color.White;
            this.DGMAIN.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.DGMAIN.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            this.DGMAIN.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.DarkSlateBlue;
            GetDG();
            GetItemList();
            Console.SetOut(new ConsoleTextWriter(textBox1));
            setTaskAtFixedTime();
        }

        public class ConsoleTextWriter : TextWriter
        {
            private System.Windows.Forms.TextBox _textBox;
            public override Encoding Encoding => Encoding.UTF8;

            public ConsoleTextWriter(System.Windows.Forms.TextBox textBox)
            {
                _textBox = textBox;
            }

            public override void Write(char value) //参数必须是char，否则不会进入
            {
                _textBox.Invoke(new Action(() =>
                {
                    _textBox.AppendText(value.ToString());
                }));
            }
        }



        private void GetDG(System.Data.DataTable dt)
        {
            DGMAIN.SetDataSource(dt);
            if (this.DGMAIN.Columns.Count == 0)
            {
                this.DGMAIN.AddColumn("ServerID", "ServerID", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ServerIP", "ServerIP", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("CheckType", "CheckType", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("CheckItem", "CheckItem", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                //this.DGMAIN.AddColumn("Ping", "Ping", 50, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                //this.DGMAIN.AddColumn("ServiceName", "ServiceName", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                //this.DGMAIN.AddColumn("Port", "Port", 50, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("CheckResult", "CheckResult", 200, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("Inactive", "Inactive", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("PriorityLevel", "PriorityLevel", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("TestInterval", "TestInterval", 30, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ExecutionTime", "ExecutionTime", 200, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ExecutionComputer", "ExecutionComputer", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ExecutionIP", "ExecutionIP", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ResultSource", "ResultSource", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);

                // 实现列的锁定功能  
                this.DGMAIN.Columns[1].Frozen = true;
                //禁止用户改变DataGridView1所有行的行高
                DGMAIN.AllowUserToResizeRows = false;
            }


        }
        private void GetDG()
        {
            list.Clear();

            Win32ServiceManager wsm = new Win32ServiceManager();

            list = wsm.GetItem();

            System.Data.DataTable dt = DataTableExtend.ToDataTable<ServerCheckItem>(list);


            //DataTable dtrecord = wsm.GetRecord();
            DGMAIN.SetDataSource(dt);
            if (this.DGMAIN.Columns.Count == 0)
            {
                this.DGMAIN.AddColumn("ServerID", "ServerID", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ServerIP", "ServerIP", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("CheckType", "CheckType", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("CheckItem", "CheckItem", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                //this.DGMAIN.AddColumn("Ping", "Ping", 50, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                //this.DGMAIN.AddColumn("ServiceName", "ServiceName", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                //this.DGMAIN.AddColumn("Port", "Port", 50, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("CheckResult", "CheckResult", 200, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("Inactive", "Inactive", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("PriorityLevel", "PriorityLevel", 20, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("TestInterval", "TestInterval", 30, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ExecutionTime", "ExecutionTime", 200, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ExecutionComputer", "ExecutionComputer", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ExecutionIP", "ExecutionIP", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);
                this.DGMAIN.AddColumn("ResultSource", "ResultSource", 100, true, null, DataGridViewContentAlignment.MiddleLeft, null, true);

                // 实现列的锁定功能  
                this.DGMAIN.Columns[1].Frozen = true;
                //禁止用户改变DataGridView1所有行的行高
                DGMAIN.AllowUserToResizeRows = false;
            }


        }

        public DialogResult MessageBoxShow(string msg)
        {
            return MessageBox.Show(msg, "请检查服务器状态", MessageBoxButtons.YesNo);
        }

        private void 手动检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            GetDG();
        }
        private System.Windows.Forms.Timer[] ts = new System.Windows.Forms.Timer[6];

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }



        private void 服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceManage s = new ServiceManage();
            s.ShowDialog();
        }

        private void 端口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PortManage pm = new PortManage();
            pm.ShowDialog();
        }

        private void pingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }




        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void 添加测试项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemAdd itemAddForm = new ItemAdd();
            itemAddForm.ShowDialog();
            if (itemAddForm.DialogResult == DialogResult.OK)
            {
                GetDG();
            }

        }



        #region 对窗体的唯一实例进行控制
        private static Main instance;
        /// <summary>
        /// 返回该窗体的唯一实例。如果之前该窗体没有被创建，进行创建并返回该窗体的唯一实例。
        /// 此处采用单键模式对窗体的唯一实例进行控制，以便外界窗体或控件可以随时访问本窗体。
        /// </summary>
        public static Main Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Main();
                }
                return instance;
            }
        }
        #endregion
        #region 判断子窗体是否被打开
        /// <summary>
        /// 判断子窗体是否被打开
        /// </summary>
        /// <param name="SubForm">子窗体名称</param>
        /// <param name="m">0为普通窗体，1为ShowDialog模式窗体</param>
        private void ShowForm(Form SubForm, int m = 0)
        {

            foreach (Form ChildForm in MdiChildren)
            {
                if (ChildForm.Name == SubForm.Name)
                {
                    ChildForm.Activate();
                    return;
                }
            }

            if (m == 0)
            {
                SubForm.MdiParent = this;
                SubForm.WindowState = FormWindowState.Maximized;
                SubForm.Show();
            }
            else
            {
                SubForm.ShowDialog();
            }
        }

        #endregion



        private void 发送邮件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string SendMailResult = Emailhelp.SendMailUseZj("stoney_xu@highrock.com.cn", "测试邮件发送-主题", "测试邮件发送-邮件内容");
            MessageBox.Show(SendMailResult);
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        void t_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Enabled = false;
            SendKeys.SendWait("{Enter}");
        }



        class TProcess
        {
            public TProcess()
            {

            }


            public Func<string, DialogResult> MessageBoxShowHandler;

            public void Start()
            {
                Thread m_Th = new Thread(new ThreadStart(() => { MessageBoxShow("Hello Hi 你好！"); }));

                m_Th.IsBackground = true;
                m_Th.Start();
            }

            public void MessageBoxShow(string msg)
            {
                MessageBox.Show("第一");


                DialogResult result = DialogResult.None;
                System.Threading.Tasks.Task.Run(() =>
                {
                    if (MessageBoxShowHandler != null)
                    {
                        result = /*MessageBoxShowHandler(msg)*/MessageBox.Show(msg, "Task 内部测试", MessageBoxButtons.YesNo);
                    }
                });
                System.Threading.Tasks.Task.Run(() =>
                {
                    if (MessageBoxShowHandler != null)
                    {
                        //result = MessageBoxShowHandler(msg);
                        result = (DialogResult)(MessageBoxShowHandler?.Invoke(msg + "1"));
                    }
                });

                while (true)
                {
                    if (result == DialogResult.Yes)
                    {
                        Console.WriteLine("Yes");
                        break;
                    }
                }
            }
        }

        private void 自动检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            GetItemList();
        }
        private string GetItem(ServerCheckItem ServerCheckItem)
        {
            string messagem = "";
            Win32ServiceManager wsm = new Win32ServiceManager();
            //Console.WriteLine("CurrentId:" + System.Threading.Tasks.Task.CurrentId + "  aa;" + Thread.CurrentThread.ManagedThreadId.ToString());
            wsm.CheckLine(ServerCheckItem);
            if (ServerCheckItem.CheckType == -1 && ServerCheckItem.CheckResult != "连接")
            {
                messagem = messagem + "\r\n" + "Ping " + ServerCheckItem.ServerIP.ToString() + " 失败：" + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;

            }
            if (ServerCheckItem.CheckType == 0 && ServerCheckItem.CheckResult != "Running")
            {
                messagem = messagem + "\r\n" + "Windows服务：" + ServerCheckItem.ServerIP.ToString() + " " + ServerCheckItem.CheckItem.ToString() + "：" + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;

            }
            if (ServerCheckItem.CheckType == 1 && ServerCheckItem.CheckResult != "端口打开")
            {
                messagem = messagem + "\r\n" + "端口：" + ServerCheckItem.ServerIP.ToString() + ":" + ServerCheckItem.CheckItem.ToString() + "：" + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;
            }
            if (ServerCheckItem.CheckType == -2 && ServerCheckItem.CheckResult != "正常")
            {
                string[] strArray = ServerCheckItem.CheckItem.ToString().Trim().Split('!');
                string _disksrc = strArray[0];
                string _threshold = strArray[1];
                messagem = messagem + "\r\n" + "Windows磁盘：" + ServerCheckItem.ServerIP + " " + ServerCheckItem.CheckResult.ToString() + ServerCheckItem.ExecutionTime;
            }
            if (ServerCheckItem.CheckType == -3 && ServerCheckItem.CheckResult != "正常")
            {
                string[] strArray = ServerCheckItem.CheckItem.ToString().Trim().Split('!');
                int _port = (int)Convert.ToInt64(strArray[0]);
                string _ServiceName = strArray[1];

                messagem = messagem + "\r\n" + "CentOS服务：" + ServerCheckItem.ServerIP + "服务" + _ServiceName + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;
            }
            if (ServerCheckItem.CheckType == -4 && ServerCheckItem.CheckResult != "正常")
            {
                string[] strArray = ServerCheckItem.CheckItem.ToString().Trim().Split('!');
                int _port = (int)Convert.ToInt64(strArray[0]);
                string _path = strArray[1];
                messagem = messagem + "\r\n" + "CentOS磁盘：" + ServerCheckItem.ServerIP + " /" + _path + " 剩余空间" + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;
            }

            return messagem;


        }
        private async void GetItemList()
        {
            if (working == true) { return; }
            working = true;

            //list.Clear();
            Win32ServiceManager wsm = new Win32ServiceManager();
            list = wsm.GetItem();
            List<string> messaagelist = new List<string>();

            // 使用Task循环创建50个线程
            if (list.Count > 0)
            {
                // 设置线程池中最大的线程数
                ThreadPool.SetMaxThreads(6, 6);
                // 创建Task的集合
                List<System.Threading.Tasks.Task> taskList = new List<System.Threading.Tasks.Task>();

                for (int i = 0; i < list.Count; i++)
                {
                    int k = i;
                    DateTime dtime = list[k].ExecutionTime.ToDateTime().AddMinutes(Convert.ToInt16(list[k].TestInterval));
                    DateTime dtimenow = DateTime.Now;
                    if (dtimenow < dtime)
                    {
                        continue;
                    }
                    else
                    {
                        System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() =>
                        {
                            string messagem1 = GetItem(list[k]);
                            if (messagem1 != "")
                            {
                                //string SendMailResult = Emailhelp.SendMailUseZj("stoney_xu@highrock.com.cn", "请检查相关服务器状态", messagem);
                                messaagelist.Add(messagem1);
                            }
                        });
                        // 把task加入到集合中
                        taskList.Add(task);
                    }
                }
                try
                {
                    Console.WriteLine(DateTime.Now.ToString() + " 准备开始 本次需要检测" + taskList.Count + "项\r\n");
                    // Wait for all the tasks to finish.
                    await System.Threading.Tasks.Task.WhenAll(taskList.ToArray());
                    // We should never get to this point
                    //Console.WriteLine(DateTime.Now.ToString()+ "  WhenAll() has not thrown exceptions.");
                    working = false;
                }
                catch (AggregateException e)
                {
                    Console.WriteLine(DateTime.Now.ToString() + "\nThe following exceptions have been thrown by WhenAll(): (THIS WAS EXPECTED)");
                    for (int j = 0; j < e.InnerExceptions.Count; j++)
                    {
                        Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
                    }
                    working = false;
                }
                System.Data.DataTable dt = DataTableExtend.ToDataTable<ServerCheckItem>(list);
                string a = "";

                for (int i = 0; i < messaagelist.Count; i++)
                {
                    if (a == null) { a = messaagelist[i].ToString().Trim() + "\r\n"; }
                    else { a = a + messaagelist[i].ToString().Trim() + "\r\n"; }
                }
                DateTime now = DateTime.Now;//当前时间
                DateTime BeginOClock = DateTime.Today.AddHours(6.0); //6:00:00
                DateTime EndOClock = DateTime.Today.AddHours(22.0); //22:00:00
                if (now > BeginOClock && now < EndOClock)
                {
                    if (a != "")
                    {
                        NoErr = 1;
                        //string SendMailResult = Emailhelp.SendMailUseZj("stoney_xu@highrock.com.cn", "请检查相关服务器状态", a);
                        //Console.WriteLine(DateTime.Now.ToString() + " 发现异常 已发送邮件\r\n" + a);  
                        if (str6oclock != "")
                        {
                            Console.WriteLine(a);
                            new WeCom().SendToWeCom(
                                                str6oclock + "\r\n" + a,
                                                "wwed1606c46cbfc117"
                                                , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                                                "1000002", "3"
                                                );
                            Console.Write("\r\n");
                            str6oclock = "";
                        }
                        else
                        {
                            Console.WriteLine(a);
                            new WeCom().SendToWeCom(
                                                a,
                                                "wwed1606c46cbfc117"
                                                , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                                                "1000002", "3"
                                                );
                            Console.Write("\r\n");
                        }
                    }
                    else
                    {///stoney a aaaaaaa
                        if (NoErr % 12 == 0)
                        {
                            
                            string Joke = wsm.GetJoke();//取66服务器上的笑话




                            new WeCom().SendToWeCom(
                                                Joke,
                                                "wwed1606c46cbfc117"
                                                , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                                                "1000002", "2"
                                                );
                            Console.Write("\r\n");
                            //Console.Write(new WeCom().SendToWeCom(
                            //                    Joke,
                            //                    "wwed1606c46cbfc117"
                            //                    , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                            //                    "1000002", "3"
                            //                    ));
                        }
                        //Console.WriteLine(DateTime.Now.ToString() + " 无异常 未发送电子邮件\r\n");                   
                        NoErr = NoErr + 1;
                    }
                }
                else
                {
                    if (a != "")
                    {
                        str6oclock = str6oclock + a;
                    }
                }
                GetDG(dt);
            }
            Console.WriteLine(DateTime.Now.ToString() + " 本次完成\r\n");
            working = false;
        }


        /// <summary>
        /// 测试-发天气预报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Win32ServiceManager wsm = new Win32ServiceManager();
            string RandomWords = wsm.GetRandomWords();
            Console.Write("\r\n\r\n");
            Console.Write(new WeCom().SendToWeCom(
                                RandomWords,
                                "wwed1606c46cbfc117"
                                , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                                "1000002", "2"
                                ));
        }
        public class HttpUitls
        {
            public static string Get(string Url)
            {
                //System.GC.Collect();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Proxy = null;
                request.KeepAlive = false;
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.AutomaticDecompression = DecompressionMethods.GZip;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }

                return retString;
            }

            public static string Post(string Url, string Data, string Referer)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Referer = Referer;
                byte[] bytes = Encoding.UTF8.GetBytes(Data);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                Stream myResponseStream = request.GetRequestStream();
                myResponseStream.Write(bytes, 0, bytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
                return retString;
            }

        }


        public class Studet
        {   //code API状态码，具体含义请参考状态码
            //updateTime 当前API的最近更新时间
            //fxLink 当前数据的响应式页面，便于嵌入网站或应用
            //now.obsTime 数据观测时间
            //now.temp 温度，默认单位：摄氏度
            //now.feelsLike 体感温度，默认单位：摄氏度
            //now.icon 天气状况和图标的代码，图标可通过天气状况和图标下载
            //now.text 天气状况的文字描述，包括阴晴雨雪等天气状态的描述
            //now.wind360 风向360角度
            //now.windDir 风向
            //now.windScale 风力等级
            //now.windSpeed 风速，公里 / 小时
            //now.humidity 相对湿度，百分比数值
            //now.precip 当前小时累计降水量，默认单位：毫米
            //now.pressure 大气压强，默认单位：百帕
            //now.vis 能见度，默认单位：公里
            //now.cloud 云量，百分比数值。可能为空
            //now.dew 露点温度。可能为空
            //refer.sources 原始数据来源，或数据源说明，可能为空
            //refer.license 数据许可或版权声明，可能为空
            //public string code { get; set; }
            //public int updateTime { get; set; }
            //public string fxLink { get; set; }
            //public DateTime now.obsTime { get; set; }
            //public string code { get; set; }
            //public int updateTime { get; set; }
            //public string fxLink { get; set; }
            //public DateTime now.obsTime { get; set; }
            //public string code { get; set; }
            //public int updateTime { get; set; }
            //public string fxLink { get; set; }
            //public DateTime now.obsTime { get; set; }
            //public string code { get; set; }
            //public int updateTime { get; set; }
            //public string fxLink { get; set; }
            //public DateTime now.obsTime { get; set; }


        }

        private void 不朽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // PythonApplication1 p = new PythonApplication1();
        }

        /// <summary>
        /// 设置定时器在零点执行任务
        /// </summary>
        private void setTaskAtFixedTime()
        {
            DateTime now = DateTime.Now;
            DateTime zeroOClock = DateTime.Today.AddHours(22.0); //凌晨00:00:00

            if (now > zeroOClock)
            {
                zeroOClock = zeroOClock.AddDays(0.5);
            }


            int msUntilFour = (int)((zeroOClock - now).TotalMilliseconds);




            var t = new System.Threading.Timer(doWeather);//发送天气预报


            t.Change(msUntilFour, Timeout.Infinite);


        }


        /// <summary>
        /// 设置定时器在零点执行任务
        /// </summary>
        private void setTaskAtFixedTime(string WeatherOrIT)
        {
            if (WeatherOrIT == "Weather")
            {
                DateTime now = DateTime.Now;
                DateTime zeroOClock = DateTime.Today.AddHours(0.0); //凌晨00:00:00
                if (now > zeroOClock)
                {
                    zeroOClock = zeroOClock.AddDays(0.9);
                }
                int msUntilFour = (int)((zeroOClock - now).TotalMilliseconds);

                var t = new System.Threading.Timer(doWeather);
                t.Change(msUntilFour, Timeout.Infinite);
            }
            else if (WeatherOrIT == "IT")
            {

            }
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void doWeather(object state)
        {
            //string debugPath = System.Environment.CurrentDirectory;           //此c#项目的debug文件夹路径
            string pyexePath = @"E:\测试\工具\PythonApplication1\weather.exe";   //天气预报
            //python文件所在路径，一般不使用绝对路径，此处仅作为例子，建议转移到debug文件夹下
            Process p = new Process();
            p.StartInfo.FileName = pyexePath;//需要执行的文件路径
            p.StartInfo.UseShellExecute = false; //必需
            p.StartInfo.RedirectStandardOutput = true;//输出参数设定
            p.StartInfo.RedirectStandardInput = true;//传入参数设定
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = "2 3";//参数以空格分隔，如果某个参数为空，可以传入””
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();//关键，等待外部程序退出后才能往下执行}
            Console.Write(output);//输出
            p.Close();
            Console.Write("\r\n发送天气预报\r\n");
            //再次设定
            setTaskAtFixedTime();
        }

        //点击最小化到托盘
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();   //隐藏窗体
                notifyIcon1.Visible = true; //使托盘图标可见
            }
        }
        //双击托盘图标重新显示
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void 加密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //加密连接字符串 string aa = "server=172.16.1.77;User ID=sa;Password=@Fuf%wfY;database=ServiceManage";
            InputPassWord ps = new InputPassWord();
            ps.ShowDialog();
            if (ps.DialogResult != DialogResult.OK) { MessageBox.Show("请重新输入密码"); return; }
            //Clipboard.SetText(ps.stringPassword);
            //MessageBox.Show("密码已复制到剪切板，请继续");            
        }

        private void 访问linuxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Win32ServiceManager wsm = new Win32ServiceManager();
            //DiskInfo diskInfo = new DiskInfo();
            //diskInfo = wsm.LinuxGetFolderDiskInfo("172.16.1.99",2263,"root","Rainoo5683@","data");
            //string a=wsm.LinuxGetServicesInfo("172.16.1.99", 2263, "root", "Rainoo5683@", "mysql");
            string Joke = wsm.GetJoke();
        }

        private void getJokeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Win32ServiceManager wsm = new Win32ServiceManager();
            string RandomWords = wsm.GetJoke();
            new WeCom().SendToWeCom(
                    RandomWords,
                    "wwed1606c46cbfc117"
                    , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                    "1000002", "2"
                    );
        }

        private void nPOIToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
        }
    }
}


