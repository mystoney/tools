using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConn.DBUtility
{
    static class Hexdecoder
    {
        static int HexChar2Int(char c)
        {
            if (c >= '0' && c <= '9') return c - '0';
            if (c >= 'A' && c <= 'F') return c - 'A' + 10;
            if (c >= 'a' && c <= 'f') return c - 'a' + 10;
            throw new Exception("Invalid hex char: [" + c + "]");
        }

        static byte GetByte(string s, int i)
        {
            return (byte)(HexChar2Int(s[2 * i]) * 16 + HexChar2Int(s[2 * i + 1]));
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2) throw new Exception("Usage: hexdecoder.exe input-file output-file");
                var sb = new StringBuilder();
                foreach (var s in File.ReadLines(args[0])) sb.Append(s.Trim());
                using (var sw = new BinaryWriter(File.OpenWrite(args[1])))
                {
                    var s = sb.ToString();
                    if (s.Length % 2 != 0) throw new Exception("Total length must be even number");
                    var bs = new byte[s.Length / 2];
                    for (var i = 0; i < bs.Length; i++) bs[i] = GetByte(s, i);
                    sw.Write(bs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((ex.Message.StartsWith("Usage: ") ? "" : "Error: ") + ex.Message);
            }
        }
    }
}
