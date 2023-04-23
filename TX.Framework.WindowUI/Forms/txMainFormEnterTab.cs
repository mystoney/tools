using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TX.Framework.WindowUI.Forms;

namespace MES.form
{
    public partial class txMainFormEnterTab : MainForm
         {
        public txMainFormEnterTab()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
               
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txMainFormEnterTab_Load(object sender, EventArgs e)
        {

        }

        private void txButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("11111");
        }

        private void txComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
