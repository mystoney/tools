using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HRBase;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Data.OleDb;
using System.Runtime.Remoting.Messaging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NPOI.OpenXmlFormats.Dml.Chart;
using static NPOI.Form1;
using System.Collections;


namespace NPOI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet();
        private void Button5_Click(object sender, EventArgs e)
        {

        }




        /// <summary>
        /// 返回信息的类
        /// </summary>
        public class Return_Message
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public Return_Message()
            {
            }


            /// <summary>
            /// 返回的状态
            /// </summary>
            public Return_State State { get; set; }

            /// <summary>
            /// 返回的值的JSON对象
            /// </summary>
            public string Return_Value { get; set; }
            /// <summary>
            /// 返回的信息
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// 返回信息类中信息状态用的枚举
            /// </summary>
            public enum Return_State
            {
                /// <summary>
                /// OK
                /// </summary>
                OK = 1,
                /// <summary>
                /// Error
                /// </summary>
                Error = 2
            }

        }

        public class T_Style_Item_Option
        {
            public int id;
            public string Style_No;
            public string Item_No;
            public string Option_No;
            public string Name;
            public string Input_Content;
            public decimal Price;
            public int CheckState;
            public int Input_type;
        }
        public class T_Style_Item
        {
            public List<T_Style_Item_Option> T_Style_Item_Option;
            public int Id;
            public string Style_No;
            public string Item_No;
            public string Name;
            public string Group_No;
            public decimal CheckState;
        }
        public class Rootobject
        {
            public List<T_Style_Item> T_Style_Item;
            public int Id;
            public string Style_No;
            public string Product_Category;
            public DateTime Input_Date;
            public int Input_Mod;
            public decimal Price;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var wb = new XSSFWorkbook();
            var sheet = wb.CreateSheet("Sheet1");
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));
            var row = sheet.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.SetCellValue("合并了4个单元格");

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "xlsx文件(*.)|*.xlsx"; ; //设置文件类型
            dialog.FileName = "test.xlsx";
            dialog.DefaultExt = "xlsx"; //设置默认格式（可以不设）
            dialog.AddExtension = true; //设置自动在文件名中添加扩展名
            dialog.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录 

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string save_filename = dialog.FileName; //获得文件路径 
                                                        //举例子，写入一个二进制文件
                BinaryWriter bw = new BinaryWriter(File.Create(save_filename));
                bw.Write(1);
                
                bw.Close();
                MessageBox.Show("保存成功!");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string JasonResult = $@"http://172.16.1.83:8007/api/t_style/" + "ULM3003";

            Return_Message Return_Message1 = new Return_Message();
            Return_Message1 = Helper.Json.JsonHelper.DeserializeJsonToObject<Return_Message>(Helper.Http.Http.HttpGet(JasonResult));

            if (Return_Message1.State == Return_Message.Return_State.Error)
            {
                throw new Exception(Return_Message1.Message);
            }

            Rootobject ItemOptionList = Helper.Json.JsonHelper.DeserializeJsonToObject<Rootobject>(Return_Message1.Return_Value);
            //DataSet ItemOptionList = Helper.Json.JsonHelper.DeserializeJsonToObject<DataSet>(Return_Message1.Return_Value);




            #region 选择保存Excel文件的目录
            System.Windows.Forms.SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.Filter = "Excel文件(*.xlsx)|*.xlsx";
            SaveFileDialog1.FileName = "ULM3003" + ".xlsx";
            FolderBrowserDialog fs = new FolderBrowserDialog();
            if (fs.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string savePath;
            savePath = fs.SelectedPath + "\\" + SaveFileDialog1.FileName;
            #endregion





            //// 创建一个新的Excel文件
            //IWorkbook workbook = new XSSFWorkbook();

            //// 创建一个工作表
            //ISheet worksheet = workbook.CreateSheet("工作表名称");

            //// 写入单元格的值
            //IRow row = worksheet.CreateRow(0);
            //ICell cell = row.CreateCell(0);
            //cell.SetCellValue("Hello World");

            //// 保存Excel文件
            //using (FileStream file = new FileStream("文件路径/文件名.xlsx", FileMode.Create, FileAccess.Write))
            //{
            //    workbook.Write(file);
            //}







        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
            ofd.Filter = "Excel表格|*.xlsx|Excel97-2003表格|*.xls|所有文件|*.*";//打开文件对话框筛选器，默认显示文件类型
            string strPath;//定义文件路径
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    strPath = ofd.FileName;
                    using (FileStream file = new FileStream(strPath, FileMode.Open, FileAccess.Read))
                    {
                        IWorkbook workbook = new XSSFWorkbook(file);

                        // 选择工作表
                        ISheet worksheet = workbook.GetSheet("工作表名称");

                        // 读取单元格的值
                        IRow row = worksheet.GetRow(0);
                        ICell cell = row.GetCell(0);
                        string cellValue = cell.StringCellValue;

                        // 读取整个工作表的数据
                        List<List<string>> data = new List<List<string>>();
                        for (int rowIndex = 0; rowIndex <= worksheet.LastRowNum; rowIndex++)
                        {
                            row = worksheet.GetRow(rowIndex);
                            List<string> rowData = new List<string>();
                            for (int columnIndex = 0; columnIndex < row.LastCellNum; columnIndex++)
                            {
                                cell = row.GetCell(columnIndex);
                                string value = cell?.StringCellValue ?? "";
                                rowData.Add(value);
                            }
                            data.Add(rowData);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);//捕捉异常 
                }
            }
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Clear();


            OpenFileDialog ofd = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
            ofd.Filter = "Excel表格|*.xlsx|Excel97-2003表格|*.xls|所有文件|*.*";//打开文件对话框筛选器，默认显示文件类型
            string strPath;//定义文件路径
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    strPath = ofd.FileName;
                    TextBox28.Text = ofd.FileName; ;

                    DataColumn dc_OperationNo = new DataColumn();
                    dc_OperationNo.ColumnName = "OperationNo";

                    Helper.Excel.ExcelHelper excelHelper = new Helper.Excel.ExcelHelper(ofd.FileName);
                    ds = excelHelper.exceltoDataSet();
                    ds.Tables[0].Columns.Add(dc_OperationNo);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);//捕捉异常 
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            //打开或创建工作簿
            HSSFWorkbook workbook = new HSSFWorkbook();
            //获取Sheet1工作表
            HSSFSheet sheet1 = (HSSFSheet)workbook.CreateSheet("Sheet1");
            //写入数据
            //设置样式
            //保存工作簿到文件
            using (FileStream file = new FileStream("test.xls", FileMode.Create))
            {
                OpenFileDialog ofd = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
                ofd.Filter = "Excel表格|*.xlsx|Excel97-2003表格|*.xls|所有文件|*.*";//打开文件对话框筛选器，默认显示文件类型
                string strPath;//定义文件路径
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        strPath = ofd.FileName;
                        workbook.Write(file);
                    }
                    catch { }
                }
            }
        }
    }
}
