using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;

namespace Helper.Excel
{
    /// <summary>
    /// ExcelHelper
    /// </summary>
    public class ExcelHelper : IDisposable
    {
        #region 声明
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;
        #endregion

        #region 重载加入表格路径

        /// <summary>
        /// ExcelHelper（fileName）
        /// </summary>
        /// <param name="fileName"></param>
        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        #endregion

        #region 得到EXCEL的工作表中的所有Sheet名

        /// <summary>
        /// 得到EXCEL的工作表中的所有Sheet名
        /// </summary>
        /// <returns></returns>
        public List<string> excel_sheet_list()
        {

            try
            {
                List<string> excel_sheet_list = new List<string>();
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);


                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    string SheetName;
                    SheetName = workbook.GetSheetAt(i).SheetName;
                    excel_sheet_list.Add(SheetName);
                }

                return excel_sheet_list;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region 将DataTable数据导入到excel中

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        #endregion

        #region 将excel中的数据导入到DataTable中

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        #endregion

        #region 将excel中的数据导入到DataTable中

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }

        #endregion


        #region 将DataSet数据导入到excel中

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="ds">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataSetToExcel(DataSet ds, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                for (int ii = 0; ii < ds.Tables.Count; ii++)
                {
                    if (workbook != null)
                    {
                        sheet = workbook.CreateSheet(ds.Tables[ii].TableName);
                    }
                    else
                    {
                        return -1;
                    }

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < ds.Tables[ii].Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(ds.Tables[ii].Columns[j].ColumnName);
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    for (i = 0; i < ds.Tables[ii].Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < ds.Tables[ii].Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(ds.Tables[ii].Rows[i][j].ToString());
                        }
                        ++count;
                    }
                }

                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        #endregion

        #region 将excel中的数据导入到DataSet中

        /// <summary>
        /// 将excel中的数据导入到DataSet中
        /// </summary>
        /// <returns></returns>
        public DataSet exceltoDataSet()
        {
            DataSet return_datrset = new DataSet();
            List<string> excel_name_list = new List<string>();
            excel_name_list = excel_sheet_list();

            foreach (string item in excel_name_list)
            {
                DataTable dt = new DataTable();
                dt = ExcelToDataTable(item, true);
                dt.TableName = item;
                return_datrset.Tables.Add(dt);
            }
            return return_datrset;
        }

        #endregion

        /// <summary>
        /// DataSet转Excel方法。
        /// </summary>
        /// <param name="ds">数据集。</param>
        /// <param name="path">文件保存地址。</param>
        /// <param name="addColumn">是否将列作为第一列数据。</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        /// 调用DataSetToExcelXSSF(ds, filePath);
        public static bool DataSetToExcelXSSF(DataSet ds, string path, bool addColumn = true)
        {
            try
            {
                XSSFWorkbook hw = new XSSFWorkbook();

                for (int t = 0; t < ds.Tables.Count; t++)
                {
                    ISheet sheet2 = (ISheet)hw.CreateSheet(ds.Tables[t].TableName);

                    // 将列名插入到第一行数据。
                    if (addColumn)
                    {
                        IRow rowCol2 = (IRow)sheet2.CreateRow(0);
                        for (int j = 0; j < ds.Tables[t].Columns.Count; j++)
                        {

                            ICell cell = (ICell)rowCol2.CreateCell(j);

                            cell.SetCellValue(ds.Tables[t].Columns[j].ColumnName);
                        }
                    }

                    for (int i = 0; i < ds.Tables[t].Rows.Count; i++)
                    {
                        IRow row = null;
                        if (addColumn)
                        {
                            row = (IRow)sheet2.CreateRow(i + 1);
                        }
                        else
                        {
                            row = (IRow)sheet2.CreateRow(i);
                        }

                        for (int j = 0; j < ds.Tables[t].Columns.Count; j++)
                        {
                            ICell cell = row.CreateCell(j);

                            // 获取列类型。
                            var columnType = ds.Tables[t].Columns[j].DataType;

                            // 如果列是整型数据，需要进行转换，这里需要列已经设置过属性，如果没有需要通过列数进行判断，比如第一列：j==0。
                            if (columnType == typeof(int))
                            {
                                int num = -1;
                                bool flag = int.TryParse(ds.Tables[t].Rows[i][j].ToString(), out num);

                                if (flag)
                                {
                                    cell.SetCellValue(num);
                                }
                                else
                                {
                                    cell.SetCellValue(ds.Tables[t].Rows[i][j].ToString());
                                }
                            }
                            else
                            {
                                if (ds.Tables[t].Rows[i][j].ToString().Contains("<br>"))
                                {
                                    ICellStyle cs = hw.CreateCellStyle();

                                    cs.WrapText = true;
                                    cell.CellStyle = cs;
                                }

                                cell.SetCellValue(ds.Tables[t].Rows[i][j].ToString().Replace("<br>", "\r\n"));
                            }
                        }
                    }
                }

                FileStream file = new FileStream(path, FileMode.Create);
                hw.Write(file);
                file.Close();

                if (!File.Exists(path))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


    }
}




