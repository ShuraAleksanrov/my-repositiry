using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(REG_MARK_LIB.RegMark.GetNextMarkInRange("a001aa99", "a001aa99", "a999ab99"));
            Console.WriteLine(REG_MARK_LIB.RegMark.CountNextMarkInRange("a001aa99", "a001aa99", "a999bb99"));
            Console.ReadKey();
        }
    }
}
