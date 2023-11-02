using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using System.IO;
using System.Text.RegularExpressions;
using Helper.Excel;

namespace NPOI_Excel
{
    public partial class FormNPOI : Form
    {
        string FilePath = "";//单步处理的文件路径
                             //获取单个Excel表的数据


        public static DataTable dataTable = new DataTable();
        public static DataTable dtsource = new DataTable();
        public static DataTable dttarget = new DataTable();
        //记录单个Excel中需要异常值处理的行数
        public static List<int> listReplaceRows = new List<int>();

        //文件夹中文件名集合
        string[] FilesInFolder = { };




        public FormNPOI()
        {
            InitializeComponent();
        }

        private void FormNPOI_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            //获取文件夹中的所有表的路径
            string FolderPath = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                FolderPath = folderBrowserDialog.SelectedPath;
            }
            else
            {
            }
            if (FolderPath != string.Empty)
            {
                txtFolerPath.Text = FolderPath;
                FilesInFolder = Directory.GetFiles(FolderPath);
            }
            else
            {
                MessageBox.Show("请选择文件夹");
            }

        }

        private void btnOneKey_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < FilesInFolder.Length; k++)
            {
                string filepath = FilesInFolder[k];
                DataTable dtOKTemp = new DataTable();
                dtOKTemp = ExcelToDataTable(filepath, true);


                dtOKTemp = DataProcess(dtOKTemp);

                //每个文件的导出
                bool flagExpert = false;
                flagExpert = DataTableToExcel(dtOKTemp, filepath);
                if (flagExpert == true)
                {
                    //导出成功,知道最后一个，提示本文件夹中所有表格都导出成功
                    if (k == FilesInFolder.Length - 1)
                    {
                        MessageBox.Show("本文件夹中所有的表格处理、导出完成！");
                    }
                }
                else
                {
                    MessageBox.Show(filepath + "导出失败");
                }
            }

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
            openFileDialog1.Filter = "Excel Files|*.xlsx;*.xls";
            openFileDialog1.ShowDialog();
            FilePath = openFileDialog1.FileName;

            dataTable = ExcelToDataTable(FilePath, true);
            dataGridView1.DataSource = dataTable;

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                return;
            }
            DataTable dtTemp = new DataTable();
            dtTemp = dataTable.Copy();//临时表，只用来检查数据涂颜色

            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 1; i < dtTemp.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) != 0)
                    {
                        if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) < Convert.ToDouble(dtTemp.Rows[i - 1]["***"].ToString()))
                        {
                            dtTemp.Rows[i]["***"] = dtTemp.Rows[i - 1]["***"].ToString();
                            //dataGridView1.Rows[i].Cells["***"].Style.BackColor = Color.Red;
                            listReplaceRows.Add(i);
                            isSorted = false;
                            //break;
                        }
                    }
                }
            }

            //将原始表重新装入dataGridView1，检查的行数涂颜色
            dataGridView1.DataSource = dataTable;

            //标示替换过的行
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < listReplaceRows.Count; j++)
                {
                    if (i == listReplaceRows[j])
                    {
                        dataGridView1.Rows[i].Cells["***"].Style.BackColor = Color.Red;
                    }
                }
            }

            listReplaceRows.Clear();//清空异常行数列表

        }

        private void btnProces_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = dataTable.Copy();//获取加载的数据

            //================================调用处理函数========================
            dataTable.Clear();
            dataTable = DataProcess(dt);

            //==============================标红颜色===============================
            //将处理替换后的table重新装入dataGridView1
            dataGridView1.DataSource = dataTable;

            //标示替换过的行
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < listReplaceRows.Count; j++)
                {
                    if (i == listReplaceRows[j])
                    {
                        dataGridView1.Rows[i].Cells["***"].Style.BackColor = Color.Red;
                    }
                }
            }

        }

        private void btnExpert_Click(object sender, EventArgs e)
        {
            string filepath = txtFolerPath.Text.Trim() + "导出的表格.xlsx";
            DataTableToExcel(dataTable, filepath);

        }

        #region
        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isColumnName"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath, bool isColumnName)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                string extension = System.IO.Path.GetExtension(filePath);

                using (fs = System.IO.File.OpenRead(filePath))
                {
                    workbook = new XSSFWorkbook(fs);
                    //2007版本
                    //if (extension.Equals(".xlsx"))                 //xlsx使用XSSFWorkbook
                    //{
                    //    workbook = new XSSFWorkbook(fs);
                    //}
                    // 2003版本
                    //else if (extension.Equals(".xls"))            //xls使用HSSFWorkbook
                    //{
                    //    workbook = new HSSFWorkbook(fs);
                    //}

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)  
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataTable dt, string filePath)
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new XSSFWorkbook();//2007版本可以导出更多行数超过65535行
                    string sheetName = GetSheetName(filePath);
                    sheet = workbook.CreateSheet(sheetName);//创建文件同名的sheet
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    try
                    {
                        //设置每行每列的单元格,  
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = sheet.CreateRow(i + 1);
                            for (int j = 0; j < columnCount; j++)
                            {
                                cell = row.CreateCell(j);//excel第二行开始写入数据  
                                cell.SetCellValue(dt.Rows[i][j].ToString());
                            }
                        }
                        //using (fs = File.OpenWrite("F:\\aaaa.xlsx"))

                        using (fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))//向原文件中写入新的数据
                        {
                            workbook.Write(fs);//向打开的这个xlsx文件中写入数据  
                            result = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return false;
            }

        }

        /// <summary>
        /// 数据表功能处理
        /// </summary>
        /// <param name="dt">传入要处理的数据表</param>
        /// <returns></returns>
        public static DataTable DataProcess(DataTable dt)
        {

            #region 原处理

            DataTable dtTemp = new DataTable();
            dtTemp = dt.Copy();

            //把表中的所有时间列都转换成日期格式,并进行处理，并进行导出
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                dtTemp.Rows[i]["日期时间"] = ToDateTimeValue(dtTemp.Rows[i]["日期时间"].ToString());
            }
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 1; i < dtTemp.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) >= 0 && Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) > 0)
                    {
                        if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) < Convert.ToDouble(dtTemp.Rows[i - 1]["***"].ToString()))
                        {
                            dtTemp.Rows[i]["***"] = dtTemp.Rows[i - 1]["***"].ToString();
                            listReplaceRows.Add(i);
                            isSorted = false;

                            if (i > 6)
                            {
                                dtTemp.Rows[i]["***"] = (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) - Convert.ToDouble(dtTemp.Rows[i - 6]["***"].ToString())) * 10;
                                dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
                            }
                            else
                            {
                                dtTemp.Rows[i]["***"] = 10;    //默认和上一个推进位移相差1 1*10=10
                                dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
                            }
                        }

                        if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) > 99)
                        {
                            if (i > 6)
                            {
                                dtTemp.Rows[i]["***"] = (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) - Convert.ToDouble(dtTemp.Rows[i - 6]["***"].ToString())) * 10;
                                dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
                            }
                            else
                            {
                                //如果行数小于6又有异常，则取上一个值做计算
                                dtTemp.Rows[i]["***"] = 10;
                                dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
                            }
                        }
                    }
                }
            }

            return dtTemp;

            #endregion

     

        }


        /// <summary>
        /// 数据处理 两张表
        /// </summary>
        /// <param name="dtsource"></param>
        /// <param name="dttarget"></param>
        /// <returns></returns>
        //public static DataTable DataProcess(DataTable dtsource,DataTable dttarget)
        //{

            //#region 原处理

            //DataTable dt_source = new DataTable();
            //dt_source = dtsource.Copy();

            //DataTable dt_target = new DataTable();
            //dt_target = dttarget.Copy();

            //////把表中的所有时间列都转换成日期格式,并进行处理，并进行导出
            ////for (int i = 0; i < dtTemp.Rows.Count; i++)
            ////{
            ////    dtTemp.Rows[i]["日期时间"] = ToDateTimeValue(dtTemp.Rows[i]["日期时间"].ToString());
            ////}
            ////bool isSorted = false;
            ////while (!isSorted)
            ////{
            ////    isSorted = true;
            ////    for (int i = 1; i < dtTemp.Rows.Count; i++)
            ////    {
            ////        if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) >= 0 && Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) > 0)
            ////        {
            ////            if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) < Convert.ToDouble(dtTemp.Rows[i - 1]["***"].ToString()))
            ////            {
            ////                dtTemp.Rows[i]["***"] = dtTemp.Rows[i - 1]["***"].ToString();
            ////                listReplaceRows.Add(i);
            ////                isSorted = false;

            ////                if (i > 6)
            ////                {
            ////                    dtTemp.Rows[i]["***"] = (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) - Convert.ToDouble(dtTemp.Rows[i - 6]["***"].ToString())) * 10;
            ////                    dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
            ////                }
            ////                else
            ////                {
            ////                    dtTemp.Rows[i]["***"] = 10;    //默认和上一个推进位移相差1 1*10=10
            ////                    dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
            ////                }
            ////            }

            ////            if (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) > 99)
            ////            {
            ////                if (i > 6)
            ////                {
            ////                    dtTemp.Rows[i]["***"] = (Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) - Convert.ToDouble(dtTemp.Rows[i - 6]["***"].ToString())) * 10;
            ////                    dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
            ////                }
            ////                else
            ////                {
            ////                    //如果行数小于6又有异常，则取上一个值做计算
            ////                    dtTemp.Rows[i]["***"] = 10;
            ////                    dtTemp.Rows[i]["***"] = Convert.ToDouble(dtTemp.Rows[i]["***"].ToString()) / Convert.ToDouble(dtTemp.Rows[i]["***"].ToString());
            ////                }
            ////            }
            ////        }
            ////    }
            ////}

            ////return dtTemp;

            #endregion



        //}

        /// <summary>
        /// 从路径字符串中获取sheet名
        /// </summary>
        /// <returns></returns>
        private static string GetSheetName(string pathName)
        {
            string fileName = Path.GetFileNameWithoutExtension(pathName);
            int index = fileName.LastIndexOf(@"\\");
            string result = fileName.Substring(index + 1);
            return result;
        }

        /// <summary>
        /// 给表格添加序号，从0开始
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable AddAutoIdColumn(DataTable dt)
        {
            if (dt != null && dt.Columns[0].ColumnName != "序号")
            {
                DataColumn column = new DataColumn("序号", typeof(int));
                dt.Columns.Add(column);
                dt.Columns["序号"].SetOrdinal(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][0] = i;//i + 1;
                }
            }
            return dt;
        }

        /// <summary>
        /// Excel表格中数值格式的日期时间转换为时间格式的日期时间
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        private static string ToDateTimeValue(string strNumber)
        {
            if (!string.IsNullOrWhiteSpace(strNumber))
            {
                double dataDate = Convert.ToDouble(strNumber);
                DateTime dateTimeValue = DateTime.FromOADate(dataDate);
                return dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                MessageBox.Show(strNumber + " 不是数字格式");
                return strNumber;
            }
        }

        /// <summary>
        /// 判断字符串是否为数值
        /// </summary>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            const string pattern = "^[0-9]*$";
            Regex rx = new Regex(pattern);
            return rx.IsMatch(str);
        }


        //#endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //导入两个表格
            
        }

        private void btsource_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
            openFileDialog1.Filter = "Excel Files|*.xlsx;*.xls";
            openFileDialog1.ShowDialog();
            FilePath = openFileDialog1.FileName;

            dtsource = ExcelToDataTable(FilePath, true);
            dataGridView1.DataSource = dtsource;
        }

        private void bttarget_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
            openFileDialog1.Filter = "Excel Files|*.xlsx;*.xls";
            openFileDialog1.ShowDialog();
            FilePath = openFileDialog1.FileName;

            dttarget = ExcelToDataTable(FilePath, true);
            dataGridView2.DataSource = dttarget;
        }

        //npoi合并单元格赋值
        private void button2_Click(object sender, EventArgs e)
        {
            var wb = new XSSFWorkbook();
            var sheet = wb.CreateSheet("Sheet1");
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));
            var row = sheet.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.SetCellValue("合并了4个单元格");
            
        }




        DataSet ds_source=new DataSet();
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
            openFileDialog1.Filter = "Excel Files|*.xlsx;*.xls";
            openFileDialog1.ShowDialog();
            FilePath = openFileDialog1.FileName;
            ds_source = NpoiExcelHelper.ExcelToDataSet(FilePath, true);//Excel导入
           
        }
        private DataTable GetData(DataSet ds_source)         
        {
            if (ds_source.Tables.Count != 5)
            { MessageBox.Show("请检查导入的表格"); return new DataTable(); }
            DataTable dt = new DataTable();
            dt.Columns.Add("产品场景");
            dt.Columns.Add("产品描述");
            dt.Columns.Add("部件类别编号");
            dt.Columns.Add("Column1");
            dt.Columns.Add("Column2");
            dt.Columns.Add("Column3");
            dt.Columns.Add("选择方式");
            dt.Columns.Add("选项值序号范围");
            dt.Columns.Add("选项值序号位数");
            dt.Columns.Add("开放阶段");
            dt.Columns.Add("备注");
            dt.Columns.Add("Column4");
            dt.Columns.Add("Column5");
            dt.Columns.Add("Column6");

            DataRow dtrow1= dt.NewRow();
            dtrow1.SetField("部件类别编号", "部件总类(3位字符)");
            dtrow1.SetField("Column1", "部件名称类别\r\n(2位字符)");
            dtrow1.SetField("Column2", "部件部位/属性(2位字符)");
            dtrow1.SetField("Column3", "可否设置拼色(1=是,\r\n0=否）");
            dtrow1.SetField("Column1", "部件名称类别\r\n(2位字符)");
            dt.Rows.Add(dtrow1);


            DataRow dtrow2 = dt.NewRow();
            dtrow2.SetField("部件类别编号", "部件总类(3位字符)");
            dtrow2.SetField("Column1", "部件名称类别\r\n(2位字符)");
            dtrow2.SetField("Column2", "部件部位/属性(2位字符)");
            dtrow2.SetField("Column3", "可否设置拼色(1=是,\r\n0=否）");
            dtrow2.SetField("Column1", "部件名称类别\r\n(2位字符)");
            dt.Rows.Add(dtrow2);



            return dt;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData(ds_source);


            string SaveFilePath =NpoiExcelHelper.SaveFilePathName();
            NpoiExcelHelper.ExportExcel(dt, "测试表格",SaveFilePath);


        }


    }
}
