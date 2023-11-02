using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace NPOI_Excel
{
    public class ExcelDAL
    {

            #region 定义单元格常用到样式的枚举
            public enum stylexls
            {
                头,
                列标题,
                url,
                时间,
                数字,
                钱,
                百分比,
                中文大写,
                科学计数法,
                默认
            }
            #endregion
            //定义工作薄
            private static IWorkbook m_workbook;
            //定义sheet表
            private static ISheet m_sheet;
            //表名
            private static List<string> m_sheets = new List<string>();
            private static ICellStyle m_cellStyle;
            private static IDataFormat m_datastyle;
            //字体
            private static IFont m_font20;
            //字体
            private static IFont m_font12;
            //字体
            private static IFont m_font;
            /// <summary>
                    /// 创建Excel表
                    /// </summary>
                    /// <param name="dt">传递datatable数据类型</param>
                    /// <param name="filePath">文件保存路径</param>
                    /// <param name="sheetName">工作表名</param>
                    /// <param name="headerName">表格标题名</param>
                    /// <returns></returns>
            public static bool ExportExcel(System.Data.DataTable dt, string filePath, string sheetName, string headerName = "考勤表")
            {
                ICellStyle cellstytle = null;
                try
                {
                    //如果Excel存在就获取IWorkbook对象，否则就重新创建
                    if (File.Exists(filePath))
                    {
                        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        if (filePath.IndexOf(".xlsx") > 0) // 2007版本
                            m_workbook = new XSSFWorkbook(fs);
                        else if (filePath.IndexOf(".xls") > 0) // 2003版本
                            m_workbook = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        //创建一个工作簿
                        m_workbook = new HSSFWorkbook();
                    }
                    if (m_workbook != null)
                    {
                        //获取所有SheetName
                        int count = m_workbook.NumberOfSheets;
                        //如果该工作簿不存在表就创建新表
                        if (count < 1)
                        {
                            //创建一个 sheet 表
                            m_sheet = m_workbook.CreateSheet(sheetName);
                        }
                        else
                        {
                            m_sheets.Clear();
                            for (int i = 0; i < count; i++)
                            {
                                m_sheet = m_workbook.GetSheetAt(i);
                                m_sheets.Add(m_sheet.SheetName);
                            }
                            if (m_sheets.Contains(sheetName))
                            {
                                m_sheet = m_workbook.CreateSheet(sheetName + System.DateTime.Now.ToString("HH-mm-ss") + "副本");
                            }
                            else
                            {
                                m_sheet = m_workbook.CreateSheet(sheetName);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                    #region 打印设置
                    m_sheet.PrintSetup.Copies = 3;
                    m_sheet.PrintSetup.Landscape = false;
                    m_sheet.PrintSetup.Scale = 100;
                    //纸张设置，A4纸
                    m_sheet.PrintSetup.PaperSize = 9;
                    //打印网格线
                    m_sheet.IsPrintGridlines = true;
                    #endregion

                    #region 设置表头
                    m_sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count - 1)); //合并单元格
                    IRow row0 = m_sheet.CreateRow(0);  //创建一行
                    row0.Height = 50 * 20;
                    ICell icelltop0 = row0.CreateCell(0);  //创建一个单元格
                    IFont font = m_workbook.CreateFont();
                    font.FontHeightInPoints = 30;
                    icelltop0.CellStyle = Getcellstyle(m_workbook, stylexls.头);
                    icelltop0.SetCellValue(headerName);
                    #endregion

                    #region 设置列
                    IRow rowH = m_sheet.CreateRow(1);
                    cellstytle = Getcellstyle(m_workbook, stylexls.列标题);
                    //设置列名
                    foreach (DataColumn col in dt.Columns)
                    {
                        //创建单元格并设置单元格内容
                        rowH.CreateCell(col.Ordinal).SetCellValue(col.Caption);

                        //设置单元格格式
                        rowH.Cells[col.Ordinal].CellStyle = cellstytle;
                    }
                    #endregion

                    //写入数据
                    cellstytle = Getcellstyle(m_workbook, stylexls.默认);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //跳过前两行，第一行为标题，第二行为列名 
                        IRow row = m_sheet.CreateRow(i + 2);
                        ICell cell = row.CreateCell(0);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            cell = row.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                            cell.CellStyle = cellstytle;
                        }
                    }
                    //获取当前列的宽度，然后对比本列的长度，取最大值
                    for (int columnNum = 0; columnNum <= dt.Rows.Count; columnNum++)
                    {
                        int columnWidth = m_sheet.GetColumnWidth(columnNum) / 256;
                        for (int rowNum = 1; rowNum <= m_sheet.LastRowNum; rowNum++)
                        {
                            IRow currentRow;
                            //当前行未被使用过
                            if (m_sheet.GetRow(rowNum) == null)
                            {
                                currentRow = m_sheet.CreateRow(rowNum);
                            }
                            else
                            {
                                currentRow = m_sheet.GetRow(rowNum);
                            }

                            if (currentRow.GetCell(columnNum) != null)
                            {
                                ICell currentCell = currentRow.GetCell(columnNum);
                                int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                                if (columnWidth < length)
                                {
                                    columnWidth = length + 10;
                                }
                            }
                        }
                        m_sheet.SetColumnWidth(columnNum, columnWidth * 256);
                        //m_sheet.SetColumnWidth(0, 30 * 256);
                        //m_sheet.SetColumnWidth(1, 10 * 256);
                        //m_sheet.SetColumnWidth(2, 25 * 256);
                        //m_sheet.SetColumnWidth(3, 25 * 256);
                        //m_sheet.SetColumnWidth(4, 10 * 256);
                        //m_sheet.SetColumnWidth(5, 10 * 256);
                    }

                    //创建文件
                    FileStream file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

                    //创建一个 IO 流
                    MemoryStream ms = new MemoryStream();

                    //写入到流
                    m_workbook.Write(ms);

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

                    m_workbook.Close();
                    m_sheet = null;
                    m_workbook = null;
                    m_cellStyle = null;
                    m_datastyle = null;
                    m_font = null;
                    m_font12 = null;
                    m_font20 = null;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            #region 定义单元格常用到样式
            static ICellStyle Getcellstyle(IWorkbook wb, stylexls str)
            {
                try
                {
                    //CreateFont()不能频繁创建，会导致打开EXCEL表的时候报如下错误：
                    //此文件中的某些文本格式可能已经更改，因为它已经超出最多允许的字体数。
                    if (m_font20 == null)
                    {
                        m_font20 = wb.CreateFont();
                        m_font20.FontHeightInPoints = 20;
                        m_font20.FontName = "微软雅黑";
                        m_font20.Boldweight = (short)FontBoldWeight.Bold;
                    }
                    if (m_font12 == null)
                    {
                        m_font12 = wb.CreateFont();
                        m_font12.FontHeightInPoints = 12;
                        m_font12.FontName = "微软雅黑";
                        m_font12.Boldweight = (short)FontBoldWeight.Bold;
                    }
                    if (m_font == null)
                    {
                        m_font = wb.CreateFont();
                        m_font.FontName = "微软雅黑";
                    }

                    //if (m_cellStyle == null)
                    //{
                    m_cellStyle = wb.CreateCellStyle();
                    //边框  
                    m_cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
                    m_cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
                    m_cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
                    m_cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
                    //边框颜色  
                    m_cellStyle.BottomBorderColor = HSSFColor.OliveGreen.Blue.Index;
                    m_cellStyle.TopBorderColor = HSSFColor.OliveGreen.Blue.Index;

                    //背景图形
                    //cellStyle.FillBackgroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;  
                    //cellStyle.FillForegroundColor = HSSFColor.OLIVE_GREEN.BLUE.index;  
                    m_cellStyle.FillForegroundColor = HSSFColor.White.Index;
                    // cellStyle.FillPattern = FillPatternType.NO_FILL;  
                    m_cellStyle.FillBackgroundColor = HSSFColor.Blue.Index;

                    //水平对齐  
                    m_cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

                    //垂直对齐  
                    m_cellStyle.VerticalAlignment = VerticalAlignment.Center;

                    //自动换行  
                    m_cellStyle.WrapText = false;

                    //缩进
                    //cellStyle.Indention = 0;
                    //}
                    //创建格式
                    if (m_datastyle == null)
                    {
                        m_datastyle = wb.CreateDataFormat();
                    }

                    //上面基本都是设共公的设置  
                    //下面列出了常用的字段类型  
                    switch (str)
                    {
                        case stylexls.头:
                            //cellStyle.FillPattern = FillPatternType.LEAST_DOTS;  
                            //设置为文本格式，也可以为 text，即 dataFormat.GetFormat("text");
                            m_cellStyle.DataFormat = m_datastyle.GetFormat("@");
                            m_cellStyle.SetFont(m_font20);
                            break;
                        case stylexls.列标题:
                            // cellStyle.FillPattern = FillPatternType.LEAST_DOTS;
                            m_cellStyle.DataFormat = m_datastyle.GetFormat("@");
                            m_cellStyle.SetFont(m_font12);
                            break;
                        case stylexls.时间:
                            m_cellStyle.DataFormat = m_datastyle.GetFormat("yyyy/mm/dd");
                            m_cellStyle.SetFont(m_font);
                            break;
                        case stylexls.数字:
                            m_cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                            m_cellStyle.SetFont(m_font);
                            break;
                        case stylexls.钱:
                            m_cellStyle.DataFormat = m_datastyle.GetFormat("￥#,##0");
                            m_cellStyle.SetFont(m_font);
                            break;
                        case stylexls.url:
                            //IFont fontcolorblue = wb.CreateFont();
                            //fontcolorblue.Color = HSSFColor.OliveGreen.Blue.Index;
                            //fontcolorblue.IsItalic = true;//下划线  
                            //fontcolorblue.Underline = 1.;
                            //fontcolorblue.FontName = "微软雅黑";
                            //m_cellStyle.SetFont(fontcolorblue);
                            break;
                        case stylexls.百分比:
                            m_cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                            m_cellStyle.SetFont(m_font);
                            break;
                        case stylexls.中文大写:
                            m_cellStyle.DataFormat = m_datastyle.GetFormat("[DbNum2][$-804]0");
                            m_cellStyle.SetFont(m_font);
                            break;
                        case stylexls.科学计数法:
                            m_cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                            m_cellStyle.SetFont(m_font);
                            break;
                        case stylexls.默认:
                            m_cellStyle.SetFont(m_font);
                            break;
                    }
                    return m_cellStyle;
                }
                catch
                {
                    return m_cellStyle;
                }
            }
            #endregion
        
    }
}
