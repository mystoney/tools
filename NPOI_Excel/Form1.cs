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
            public List <T_Style_Item_Option> T_Style_Item_Option;            
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
                bw.Write();
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


        private int ToExcel(string savePath,)
        {
            // 创建一个 DataTable 对象来存储数据
            DataTable dataTable = new DataTable("MyData");
            // 添加列到 DataTable
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            // 向 DataTable 中添加数据行
            dataTable.Rows.Add("John Doe", 30);
            dataTable.Rows.Add("Jane Smith", 25);
            // 使用 NPOI 组件导出 Excel 文件
            IWorkbook workbook = new HSSFWorkbook();
            ISheet worksheet = workbook.CreateSheet("MySheet");
            int row = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                IRow newRow = worksheet.CreateRow(row);
                newRow.CreateCell(0).SetCellValue(dataRow["Name"].ToString());
                newRow.CreateCell(1).SetCellValue(Convert.ToInt32(dataRow["Age"]));
                row++;
            }
            // 将 Excel 文件保存到磁盘
            string fileName = @"C:\temp\MyExcelFile.xls";
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
            // 释放资源
            workbook.Dispose();
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
                    OperationBLL ob = new OperationBLL();
                    Int64 MaxOperationNo = ob.GetMaxOperationNo();
                    for (int p = ds.Tables[0].Rows.Count - 1; p >= 0; p--)
                    {
                        string OperationNo = string.Format("{0:d10}", MaxOperationNo);
                        ds.Tables[0].Rows[p]["OperationNo"] = "OP" + ds.Tables[0].Rows[p]["OperationType"].ToString().Trim() + OperationNo;
                        if (ds.Tables[0].Rows[p][0] is DBNull)
                            ds.Tables[0].Rows[p].Delete();
                        else if (ds.Tables[0].Rows[p][0].ToString().Trim() == "")
                            ds.Tables[0].Rows[p].Delete();
                        MaxOperationNo = MaxOperationNo + 1;
                    }
                    ds.Tables[0].AcceptChanges();
                    Grid_detail.DataSource = ds.Tables[0];
                    Grid_detail.AllowUserToAddRows = false;
                    Grid_detail.AllowUserToDeleteRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);//捕捉异常 
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Styleno == "" || Combination_no == "")
            {
                return;
            }
            try
            {
                OperationBLL ob = new OperationBLL();
                OplistNo = ob.NewOperationList(Convert.ToInt32(Combination_no), com_memo_no, Grid_detail);

                if (OplistNo == 0)
                {
                    MessageBox.Show("发生错误");

                }
                else
                {
                    string s_ToCaobo = ob.OperationToCaobo(OplistNo);
                    string s_ToJingYuan = ob.OperationToJingYuan(OplistNo);
                    if (s_ToCaobo == "1" && s_ToJingYuan == "1")
                    {
                        MessageBox.Show("完成");
                        ds.Tables.Clear();

                    }
                    else if (s_ToCaobo == "1")
                    {
                        MessageBox.Show("推送至JingYuan错误：" + s_ToJingYuan);

                    }
                    else if (s_ToJingYuan == "1")
                    {
                        MessageBox.Show("推送至生产线PAD错误：" + s_ToCaobo);

                    }
                    else
                    {
                        MessageBox.Show("推送错误,请联系管理员：" + s_ToCaobo + " " + s_ToJingYuan);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//捕捉异常    

            }
        }
    }
}
