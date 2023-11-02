using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExcelEdit
{
    public class NpoiExcelHelper
    {
        # region 读取Execl数据到DataTable(DataSet)中
        /// <summary>
        /// 读取Execl数据到DataTable(DataSet)中
        /// </summary>
        /// <param name="filePath">指定Execl文件路径</param>
        /// <param name="isFirstLineColumnName">设置第一行是否是列名</param>
        /// <returns>返回一个DataTable数据集</returns>
        public static DataSet ExcelToDataSet(string filePath, bool isFirstLineColumnName)
        {
            DataSet dataSet = new DataSet();
            int startRow = 0;
            try
            {
                using (FileStream fs = File.OpenRead(filePath))
                {
                    IWorkbook workbook = null;
                    // 如果是2007+的Excel版本
                    if (filePath.IndexOf(".xlsx") > 0)
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    // 如果是2003-的Excel版本
                    else if (filePath.IndexOf(".xls") > 0)
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                    if (workbook != null)
                    {
                        //循环读取Excel的每个sheet，每个sheet页都转换为一个DataTable，并放在DataSet中
                        for (int p = 0; p < workbook.NumberOfSheets; p++)
                        {
                            ISheet sheet = workbook.GetSheetAt(p);
                            DataTable dataTable = new DataTable();
                            dataTable.TableName = sheet.SheetName;
                            if (sheet != null)
                            {
                                int rowCount = sheet.LastRowNum;//获取总行数
                                if (rowCount > 0)
                                {
                                    IRow firstRow = sheet.GetRow(0);//获取第一行
                                    int cellCount = firstRow.LastCellNum;//获取总列数

                                    //构建datatable的列
                                    if (isFirstLineColumnName)
                                    {
                                        startRow = 1;//如果第一行是列名，则从第二行开始读取
                                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                        {
                                            ICell cell = firstRow.GetCell(i);
                                            if (cell != null)
                                            {
                                                if (cell.StringCellValue != null)
                                                {
                                                    DataColumn column = new DataColumn(cell.StringCellValue);
                                                    dataTable.Columns.Add(column);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                        {
                                            DataColumn column = new DataColumn("column" + (i + 1));
                                            dataTable.Columns.Add(column);
                                        }
                                    }

                                    //填充行
                                    for (int i = startRow; i <= rowCount; ++i)
                                    {
                                        IRow row = sheet.GetRow(i);
                                        if (row == null) continue;

                                        DataRow dataRow = dataTable.NewRow();
                                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                                        {
                                            ICell cell = row.GetCell(j);
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
                                                        if (format == 14 || format == 22 || format == 31 || format == 57 || format == 58)
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
                            dataSet.Tables.Add(dataTable);
                        }

                    }
                }
                return dataSet;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 导出到Excel
        public static void ExportExcel(DataTable dt, string SheetName, string savePath)
        {
            try
            {
                //创建一个工作簿
                IWorkbook workbook = new HSSFWorkbook();

                //创建一个 sheet 表
                ISheet sheet = workbook.CreateSheet(SheetName);

                //创建一行
                IRow rowH = sheet.CreateRow(0);

                //创建一个单元格
                ICell cell = null;

                //创建单元格样式
                ICellStyle cellStyle = workbook.CreateCellStyle();

                //创建格式
                IDataFormat dataFormat = workbook.CreateDataFormat();

                //设置为文本格式，也可以为 text，即 dataFormat.GetFormat("text");
                cellStyle.DataFormat = dataFormat.GetFormat("@");

                //设置列名
                foreach (DataColumn col in dt.Columns)
                {
                    //创建单元格并设置单元格内容
                    rowH.CreateCell(col.Ordinal).SetCellValue(col.Caption);

                    //设置单元格格式
                    rowH.Cells[col.Ordinal].CellStyle = cellStyle;
                }

                //写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //跳过第一行，第一行为列名
                    IRow row = sheet.CreateRow(i + 1);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        cell = row.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                        cell.CellStyle = cellStyle;
                    }
                }



                //设置新建文件路径及名称
                //string savePath = path + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xls";

                //创建文件
                FileStream file = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);

                //创建一个 IO 流
                MemoryStream ms = new MemoryStream();

                //写入到流
                workbook.Write(ms);

                //转换为字节数组
                byte[] bytes = ms.ToArray();

                file.Write(bytes, 0, bytes.Length);
                file.Flush();



                //释放资源
                bytes = null;

                ms.Close();
                ms.Dispose();

                file.Close();
                file.Dispose();

                workbook.Close();
                sheet = null;
                workbook = null;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 导出到Excel

        #region 选择保存文件的名称以及路径  取消返回 空"";
        /// <summary>
        /// 选择保存文件的名称以及路径  取消返回 空"";
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filter"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string SaveFilePathName(string fileName = null, string filter = null, string title = null)
        {
            string path = "";
            System.Windows.Forms.SaveFileDialog fbd = new System.Windows.Forms.SaveFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                fbd.FileName = fileName;
            }
            // if (!string.IsNullOrEmpty(filter))
            //{
            fbd.Filter = "Excel|*.xls;*.xlsx;";// "Excel|*.xls;*.xlsx;";
                                               //}
                                               //if (!string.IsNullOrEmpty(title))
                                               //{
            fbd.Title = title;// "保存为";
            //}
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.FileName;
            }
            return path;
        }

        #endregion

        #region List转为DataTable

        public static DataTable ToDataTable<T>(IEnumerable<T> list)
        {
            PropertyInfo[] modelItemType = typeof(T).GetProperties();
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(modelItemType.Select(Columns => new DataColumn(Columns.Name, Columns.PropertyType)).ToArray());
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in modelItemType)
                    {
                        object obj = pi.GetValue(list.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] dataRow = tempList.ToArray();
                    dataTable.LoadDataRow(dataRow, true);
                }
            }
            return dataTable;
        }

        #endregion List转为DataTable

        #region DataSet导出到Execl
        /// <summary>
        /// 将DataSet导出到Execl文档
        /// </summary>
        /// <param name="dataSet">传入一个DataSet</param>
        /// <param name="Outpath">导出路径（可以不加扩展名，不加默认为.xls）</param>
        /// <returns>返回一个Bool类型的值，表示是否导出成功</returns>
        /// True表示导出成功，Flase表示导出失败
        public static bool DataTableToExcel(DataSet dataSet, string Outpath)
        {
            bool result = false;
            try
            {
                if (dataSet == null || dataSet.Tables == null || dataSet.Tables.Count == 0 || string.IsNullOrEmpty(Outpath))
                    throw new Exception("输入的DataSet或路径异常");
                int sheetIndex = 0;
                //根据输出路径的扩展名判断workbook的实例类型
                IWorkbook workbook = null;
                string pathExtensionName = Outpath.Trim().Substring(Outpath.Length - 5);
                if (pathExtensionName.Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook();
                }
                else if (pathExtensionName.Contains(".xls"))
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    Outpath = Outpath.Trim() + ".xls";
                    workbook = new HSSFWorkbook();
                }
                //将DataSet导出为Excel
                foreach (DataTable dt in dataSet.Tables)
                {
                    sheetIndex++;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ISheet sheet = workbook.CreateSheet(string.IsNullOrEmpty(dt.TableName) ? ("sheet" + sheetIndex) : dt.TableName);//创建一个名称为Sheet0的表
                        int rowCount = dt.Rows.Count;//行数
                        int columnCount = dt.Columns.Count;//列数

                        //设置列头
                        IRow row = sheet.CreateRow(0);//excel第一行设为列头
                        for (int c = 0; c < columnCount; c++)
                        {
                            ICell cell = row.CreateCell(c);
                            cell.SetCellValue(dt.Columns[c].ColumnName);
                        }

                        //设置每行每列的单元格,
                        for (int i = 0; i < rowCount; i++)
                        {
                            row = sheet.CreateRow(i + 1);
                            for (int j = 0; j < columnCount; j++)
                            {
                                ICell cell = row.CreateCell(j);//excel第二行开始写入数据
                                cell.SetCellValue(dt.Rows[i][j].ToString());
                            }
                        }
                    }
                }
                //向outPath输出数据
                using (FileStream fs = File.OpenWrite(Outpath))
                {
                    workbook.Write(fs);//向打开的这个xls文件中写入数据
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 根据Excel文件类型返回IWorkbook
        /// <summary>
        /// 根据Excel文件类型返回IWorkbook
        /// </summary>
        /// <param name="fileName">文件路径/文件名称（含后缀名）</param>
        /// <param name="rowNum">Excel行数</param>
        /// <param name="colNum">Excel列数</param>
        /// <param name="isFirstRowColumn">第一行是否是标题</param>
        /// <returns></returns>
        public static IWorkbook GetWorkbook(string fileName, out int rowNum, out int colNum, bool isFirstRowColumn = true)
        {
            bool isXlsx = Path.GetExtension(fileName).Equals(".xlsx");
            if (isXlsx)
            {
                if (isFirstRowColumn)
                {
                    rowNum = 1048575;
                }
                else
                {
                    rowNum = 1048576;
                }
                colNum = 16384;
            }
            else
            {
                if (isFirstRowColumn)
                {
                    rowNum = 65535;
                }
                else
                {
                    rowNum = 65536;
                }
                colNum = 256;
            }

            if (File.Exists(fileName))
            {

                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (isXlsx)
                    {
                        return new XSSFWorkbook(fs);
                    }
                    else
                    {
                        return new HSSFWorkbook(fs);
                    }
                }
            }
            else
            {
                if (isXlsx)
                {
                    return new XSSFWorkbook();
                }
                else
                {
                    return new HSSFWorkbook();
                }
            }
        }
        #endregion 根据Excel文件类型返回IWorkbook

        #region excel中的数据导入到DataTable
        /// <summary>
        /// 将excel中的数据导入到DataTable中(第一行是标题)
        /// 支持多个sheet数据导入（建议多个sheet的数据格式保持一致，将没有数据的sheet删除）
        /// </summary>
        /// <param name="fileName">文件路径（含文件名称后缀名）</param>
        /// <param name="columnFieldText">字段对应中文 顺序需要跟Excel中数据顺序一致</param>
        /// <param name="sheetName">指定Excel中Sheet名称 如果为null时，读取所有sheet中的数据</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string[,] columnFieldText = null, string sheetName = null)
        {
            DataTable data = new DataTable();
            int rowNum = 0;
            int colNum = 0;
            IWorkbook workbook = GetWorkbook(fileName, out rowNum, out colNum);

            for (int e = 0; e < workbook.NumberOfSheets; e++)
            {
                ISheet sheet = workbook.GetSheetAt(e);
                if (sheet != null)
                {
                    var currentSheetIndex = 0;
                    if (!string.IsNullOrEmpty(sheetName))
                    {
                        if (sheet.SheetName == sheetName)
                        {
                            currentSheetIndex = e;
                        }
                    }

                    IRow firstRow = sheet.GetRow(0);
                    if (firstRow != null)
                    {
                        int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                        var dataColumn = columnFieldText != null ? columnFieldText.GetLength(0) : cellCount;
                        int startRow = sheet.FirstRowNum;
                        if (dataColumn <= colNum)
                        {
                            if (e == currentSheetIndex)
                            {
                                for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                {
                                    ICell cell = firstRow.GetCell(i);
                                    if (cell != null)
                                    {
                                        string cellValue = cell.StringCellValue;
                                        if (cellValue != null)
                                        {
                                            DataColumn column = new DataColumn((columnFieldText != null ? columnFieldText[i, 0] : cellValue));
                                            data.Columns.Add(column);
                                        }
                                    }
                                }
                            }

                            startRow = sheet.FirstRowNum + 1;

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
                        else
                        {
                            //数据列数超过了Excel的列数
                        }
                    }

                    if (!string.IsNullOrEmpty(sheetName))
                    {
                        if (sheet.SheetName == sheetName)
                        {
                            break;
                        }
                    }
                }
            }
            return data;
        }
        #endregion excel中的数据导入到DataTable

    }
}
