using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        public string stringUsername = "";
        public string stringPassword = "";
        
        private void InputPassWord_Load(object sender, EventArgs e)
        {
            stringUsername = "";
            stringPassword = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "" && textBox2.Text.Trim()!="")
            {
                stringUsername= DBCon.DBUtility.DESEncrypt.Encrypt(textBox2.Text.Trim());
                stringPassword = DBCon.DBUtility.DESEncrypt.Encrypt(textBox1.Text.Trim());
                //stringPassword = textBox1.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
            else { MessageBox.Show("请输入用户名和密码，密码不能为空"); this.DialogResult = DialogResult.No; }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            
        }
    }
}
