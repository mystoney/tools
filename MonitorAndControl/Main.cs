﻿using email;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private void GetItemList()
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
                string messagem = "";
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
                                messaagelist.Add(messagem);
                            }

                            
                        });
                        // 把task加入到集合中
                        taskList.Add(task);
                    }



                }

                // 等待所有的线程执行完
                //Task.WaitAll(taskList.ToArray());
                //Task.WaitAny(taskList.ToArray());
                try
                {
                    Console.WriteLine(DateTime.Now.ToString() + " 本次需要检测"+taskList.Count+"项");
                    // Wait for all the tasks to finish.
                    //Task.WaitAll(taskList.ToArray());

                     Task.WhenAll(taskList.ToArray());


                    // We should never get to this point

                    Console.WriteLine(DateTime.Now.ToString()+ "  WaitAll() has not thrown exceptions. ");

                    working = false;
                    
                }
                catch (AggregateException e)
                {
                    Console.WriteLine(DateTime.Now.ToString() +  "\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                    for (int j = 0; j < e.InnerExceptions.Count; j++)
                    {
                        Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
                    }
                    working = false;
                }

                //list = wsm.GetItem();
                DataTable dt = DataTableExtend.ToDataTable<ServerCheckItem>(list);
                string a = "";
                for (int i = 0; i < messaagelist.Count; i++)
                {
                    a = messaagelist[i].ToString().Trim();
                }
                if (a != "")
                {
                    string SendMailResult = Emailhelp.SendMailUseZj("stoney_xu@highrock.com.cn", "请检查相关服务器状态", a);
                }

                GetDG(dt);         
            }
            Console.WriteLine(DateTime.Now.ToString() + " 本次完成\n\n\n" );
            working = false;
        }

  


        private void pyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }





    }
}

