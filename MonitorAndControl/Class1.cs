using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorAndControl
{
    class Class1
    {
        static List<List<int>> Combine(int[] arr, int n)
        {
            List<List<int>> result = new List<List<int>>();
            List<int> current = new List<int>();
            _Combine(arr, n, 0, current, result);
            return result;
        }

        static void _Combine(int[] arr, int n, int start, List<int> current, List<List<int>> result)
        {
            if (n == 0)
            {
                result.Add(new List<int>(current));
                return;
            }

            for (int i = start; i < arr.Length; i++)
            {
                current.Add(arr[i]);
                _Combine(arr, n - 1, i + 1, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }

        //static void Main(string[] args)
        //{
        //    int[] arr = { 1, 2, 3, 4, 5 };
        //    int m = 3;
        //    List<List<int>> combinations = Combine(arr, m);
        //    foreach (List<int> combination in combinations)
        //    {
        //        foreach (int num in combination)
        //        {
        //            Console.Write(num + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}
    }
}
