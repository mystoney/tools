using email;
using MyContrals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TX.Framework.Security.Base;
using TX.Framework.WindowUI.Controls;
using static MonitorAndControl.Win32ServiceManager;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;




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
            working = false;
            timer1.Enabled = true;
            timer1.Interval = 3*60000; //设置时间间隔（毫秒为单位）单位Ms
            this.DGMAIN.RowsDefaultCellStyle.BackColor = Color.White;
            this.DGMAIN.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.DGMAIN.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            this.DGMAIN.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.DarkSlateBlue;
            GetDG();
            GetItemList();
            Console.SetOut(new ConsoleTextWriter(textBox1));
        }
        public class ConsoleTextWriter : TextWriter
        {
            private TextBox _textBox;
            public override Encoding Encoding => Encoding.UTF8;

            public ConsoleTextWriter(TextBox textBox)
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


        private void GetTreeListView()
        {




        }

        private void GetDG(DataTable dt)
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

            DataTable dt = DataTableExtend.ToDataTable<ServerCheckItem>(list);


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
            //Action act = new Action(() =>
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        ts[i] = new System.Windows.Forms.Timer();
            //        ts[i].Tick += t_Tick;
            //        ts[i].Interval = 2000;
            //        ts[i].Enabled = true;
            //        MessageBox.Show("MsgBox" + (i + 1));
            //        Thread.Sleep(2000);
            //    }
            //});
            //act.BeginInvoke(null, null);
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
                Task.Run(() =>
                {
                    if (MessageBoxShowHandler != null)
                    {
                        result = /*MessageBoxShowHandler(msg)*/MessageBox.Show(msg, "Task 内部测试", MessageBoxButtons.YesNo);
                    }
                });
                Task.Run(() =>
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
            Console.WriteLine("CurrentId:" + Task.CurrentId + "  aa;" + Thread.CurrentThread.ManagedThreadId.ToString());
            wsm.CheckLine(ServerCheckItem);
            if (ServerCheckItem.CheckType == -1 && ServerCheckItem.CheckResult != "连接")
            {
                messagem = messagem + "\r\n" + "Ping " + ServerCheckItem.ServerIP.ToString() + " 失败，结果为：" + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;

            }
            if (ServerCheckItem.CheckType == 0 && ServerCheckItem.CheckResult != "Running")
            {
                messagem = messagem + "\r\n" + ServerCheckItem.ServerIP.ToString() + "的服务" + ServerCheckItem.CheckItem.ToString() + "无法检测到，结果为：" + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;

            }
            if (ServerCheckItem.CheckType == 1 && ServerCheckItem.CheckResult != "端口打开")
            {
                messagem = messagem + "\r\n" + ServerCheckItem.ServerIP.ToString() + "的端口" + ServerCheckItem.CheckItem.ToString() + "关闭，结果为：" + ServerCheckItem.CheckResult.ToString() + " " + ServerCheckItem.ExecutionTime;
            }

            return messagem;


        }
        private async void  GetItemList()
        {
            if (working == true) { return; }
            working = true;
            Console.WriteLine(DateTime.Now.ToString() + "准备开始");
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
                List<Task> taskList = new List<Task>();
                
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
                        Task task = Task.Run(() =>
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
                    
                    Console.WriteLine(DateTime.Now.ToString() + " 本次需要检测"+taskList.Count+"项");
                    // Wait for all the tasks to finish.
                    await Task.WhenAll(taskList.ToArray());                    

                    // We should never get to this point

                    Console.WriteLine(DateTime.Now.ToString()+ "  WhenAll() has not thrown exceptions.");

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
                DataTable dt = DataTableExtend.ToDataTable<ServerCheckItem>(list);
                string a = "";
                for (int i = 0; i < messaagelist.Count; i++)
                {
                    a = messaagelist[i].ToString().Trim();

                }
                if (a != "")
                {
                    NoErr = 1;
                    string SendMailResult = Emailhelp.SendMailUseZj("stoney_xu@highrock.com.cn", "请检查相关服务器状态", a);
                    Console.WriteLine(DateTime.Now.ToString() + " 发现异常 已发送邮件" + a);
                    Console.Write("\r\n\r\n");
                    Console.Write(new WeCom().SendToWeCom(
                                        a,
                                        "wwed1606c46cbfc117"
                                        , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                                        "1000002", "3"
                                        ));                    
                }
                else
                {                    
                    if (NoErr % 2 == 0 )
                    {
                        string RandomWords=wsm.GetRandomWords();
                        Console.Write("\r\n\r\n");
                        Console.Write(new WeCom().SendToWeCom(
                                            RandomWords,
                                            "wwed1606c46cbfc117"
                                            , "dznOh-xxQax7KI_Pc_ffI_C1WRthahI7CgNPkhpykc0",
                                            "1000002","2"
                                            ));

                    }
                    Console.WriteLine(DateTime.Now.ToString() + " 无异常 未发送电子邮件");
                   
                    NoErr = NoErr + 1;
                }
                GetDG(dt);         
            }
            Console.WriteLine(DateTime.Now.ToString() + " 本次完成\r\n\r\n");
            working = false;
        }

        
        /// <summary>
        /// 测试-发天气预报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Console.Write("\r\n\r\n");
            //Console.Write(new WeCom().SendToWeCom(
            //                    "msginfo",
            //                    "wwed1606c46cbfc117"
            //                    , "6DdwqL47r1J33_yOE5uM2hvBtAQcOxgiWAnkc7VDEVI",
            //                    "1000003"
            //                    )  );
            //Console.Write("\r\n\r\n");

            string a = "https://devapi.qweather.com/v7/weather/now?location=101010100&key=eb52e34d9cbb4dd5a7c1e07f4b731902";

            string 青羊区 = "https://devapi.qweather.com/v7/weather/now?location=101270117&key=eb52e34d9cbb4dd5a7c1e07f4b731902";

            string a1 = HttpUitls.Get(青羊区);
            

            JSON js = new JSON();
            DataTable dt = js.ToDataTableOne(a1);


            Console.Write("\r\n\r\n");

            PythonProgram pp = new PythonProgram();
            //pp.

                //
            //Console.Write(s);
            //code API状态码，具体含义请参考状态码
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


    }
}


