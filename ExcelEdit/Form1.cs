using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;

namespace ExcelEdit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string FilePath = "";//单步处理的文件路径
        public static DataSet ds_source = new DataSet();
        public static DataTable dtsource = new DataTable();
        public static DataTable dttarget = new DataTable();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();//首先根据打开文件对话框，选择要打开的文件
            openFileDialog1.Filter = "Excel Files|*.xlsx;*.xls";
            openFileDialog1.ShowDialog();
            FilePath = openFileDialog1.FileName;
            ds_source = NpoiExcelHelper.ExcelToDataSet(FilePath, true);//Excel导入
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetData(ds_source);


            string SaveFilePath = NpoiExcelHelper.SaveFilePathName();
            NpoiExcelHelper.ExportExcel(ds_source.Tables[0], "测试表格", SaveFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        string targetPath = @"d:\Stoney\Desktop\abc\SJW2223Part(空白).xlsx";
        private void button3_Click(object sender, EventArgs e)
        {
            string pathSource = @"d:\\Stoney\\Desktop\\abc\\SJW2223Part.xlsx";

            IWorkbook templateWorkbook;
            using (FileStream fs = new FileStream(pathSource, FileMode.Open, FileAccess.Read))
            {
                templateWorkbook = new XSSFWorkbook(fs);
            }

            string sheetName = "ImportTemplate";
            ISheet sheet = templateWorkbook.GetSheet(sheetName) ?? templateWorkbook.CreateSheet(sheetName);
            IRow dataRow = sheet.GetRow(4) ?? sheet.CreateRow(4);
            ICell cell = dataRow.GetCell(1) ?? dataRow.CreateCell(1);
            cell.SetCellValue("foo");

            using (FileStream fs = new FileStream(pathSource, FileMode.Create, FileAccess.Write))
            {
                templateWorkbook.Write(fs);
            }
        }
   

    }
}
