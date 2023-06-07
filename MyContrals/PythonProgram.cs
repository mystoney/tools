using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorAndControl
{
   public class PythonProgram
    {
        static void Main(string[] args)
        {
            string filename = @"C:/Users/Stoney/AppData/Local/Programs/Python/Python36-32/python.exe";  // python解释器
            // pyhon模块 arg1 arg2
            string strArgument = @"E:\测试\工具\PythonApplication1\buxiu.py E:\测试\工具\PythonApplication1\weplist.txt";
            ProcessStartInfo startPythonInfo = new ProcessStartInfo(filename, strArgument);
            startPythonInfo.UseShellExecute = false;  // 是否使用操作系统的shell启动进程
            startPythonInfo.RedirectStandardOutput = true;  // 是否将应用程序的输出写入到Process.StandardOutput流中。
            startPythonInfo.RedirectStandardError = true;  // 是否将应用程序的错误输出写入到Process.StandardError流中。

            Process process = new Process();
            process.StartInfo = startPythonInfo;
            process.OutputDataReceived += CaptureOutpt;
            //process.OutputDataReceived += CaptureRrror;

            process.Start();
            process.BeginOutputReadLine();
            //process.BeginErrorReadLine();
            process.WaitForExit();

            Console.ReadKey();
        }

        static void CaptureOutpt(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)  // e.Data就是python模块的返回值
            {
                
                
                Console.WriteLine("计算结果：{0}", e.Data);
            }
        }

    }
}
