using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIN_LIB
{
    public class VIN
    {
        public static bool CheckVIN(string vin)
        {
            if (!(vin.Length == 17)) return false;
            Dictionary<char, int> numericEqlForLitters = new Dictionary<char, int>() {
                ['a'] = 1,
                ['b'] = 2,
                ['c'] = 3,
                ['d'] = 4,
                ['e'] = 5,
                ['f'] = 6,
                ['g'] = 7,
                ['h'] = 8,
                ['j'] = 1,
                ['k'] = 2,
                ['l'] = 3,
                ['m'] = 4,
                ['n'] = 5,
                ['p'] = 7,
                ['r'] = 9,
                ['s'] = 2,
                ['t'] = 3,
                ['u'] = 4,
                ['v'] = 5,
                ['w'] = 6,
                ['x'] = 7,
                ['y'] = 8,
                ['z'] = 9
            }
            ;
            int weigt = 0; //вес символа
            int index = 0;
            int numericEql = 0;
            int checkSum =0;
            int checkNum = 0;
            foreach (char c in vin.ToCharArray())
            {
                if (char.IsDigit(c)) numericEql = int.Parse(c.ToString());
                else numericEql = numericEqlForLitters[c];
                if (index < 8 || index == 9) {
                    weigt = 9 - index;
                    if (index == 9) if (c == 'x') checkNum = 10;
                        else checkNum = numericEql;
                }
                else if (index == 8) weigt = 10;
                else if (index <= 17) weigt = 19 - index;
                checkSum += numericEql * weigt;
            }
            return checkSum % 11 == checkNum;
        }
    }
}
