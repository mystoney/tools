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
    public partial class InputPassWord : Form
    {
        public InputPassWord()
        {
            InitializeComponent();
        }
        public InputPassWord(string host)
        {
            InitializeComponent();
            tx_Host.Text = host;
            tx_Host.Enabled = false;
        }


        public string stringUsername = "";
        public string stringPassword = "";
        
        private void InputPassWord_Load(object sender, EventArgs e)
        {       

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim()!="" && tx_Host.Text.Trim()!="")
            {
                stringUsername= DBCon.DBUtility.DESEncrypt.Encrypt(textBox2.Text.Trim(),System.Text.RegularExpressions.Regex.Replace(tx_Host.Text.Trim(), @"[^0-9]+", ""));
                stringPassword = DBCon.DBUtility.DESEncrypt.Encrypt(textBox1.Text.Trim(),System.Text.RegularExpressions.Regex.Replace(tx_Host.Text.Trim(), @"[^0-9]+", ""));
                //stringPassword = textBox1.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
            else { MessageBox.Show("请点击加密"); this.DialogResult = DialogResult.No; }

            //Clipboard.SetText(ps.stringPassword);
            //MessageBox.Show("密码已复制到剪切板，请继续"); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isIp(tx_Host.Text.Trim()) == false) {MessageBox.Show("请输入正确的主机IP");return; }
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "")
            {
                stringUsername = DBCon.DBUtility.DESEncrypt.Encrypt(textBox2.Text.Trim(), System.Text.RegularExpressions.Regex.Replace(tx_Host.Text.Trim(), @"[^0-9]+", ""));
                label4.Text = stringUsername;
                stringPassword = DBCon.DBUtility.DESEncrypt.Encrypt(textBox1.Text.Trim(), System.Text.RegularExpressions.Regex.Replace(tx_Host.Text.Trim(), @"[^0-9]+", ""));
                label5.Text = stringPassword; 
            }
            else { MessageBox.Show("请输入主机IP，用户名和密码，密码不能为空"); return; }
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

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(stringUsername);
            MessageBox.Show("用户名已复制到剪切板，请继续"); 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(stringPassword);
            MessageBox.Show("密码已复制到剪切板，请继续");
        }
    }
}
