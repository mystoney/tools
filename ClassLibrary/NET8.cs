using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class NET8
    {

        public byte[] stringtoHEXstring(string s)
        {
            byte[] a=new byte[s.Length];
            a=Convert.FromHexString(s);
            return a;
        }
    }
}
