using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace REG_MARK_LIB
{
    public class RegMark
    {
        public static bool CheckMark(string mark)
        {
            string alfavit = "abekhmytxcop";
            if (!(mark.Length == 8 || mark.Length == 9)) return false;
            if (!(alfavit.Contains(mark[0]) && alfavit.Contains(mark[4]) && alfavit.Contains(mark[5]))) return false;
            if (!(char.IsDigit(mark[1]) && char.IsDigit(mark[2]) && char.IsDigit(mark[3]))) return false;
            if (!((mark.Length == 8 && char.IsDigit(mark[6]) && char.IsDigit(mark[7])) || (mark.Length == 9 && char.IsDigit(mark[6]) && char.IsDigit(mark[7]) && char.IsDigit(mark[8])))) return false;
            //string region = mark.Remove(0, 6);
            //MySqlConnection connection = new MySqlConnection("server = localhost; database = worlds; user = root; password =root;");
            //connection.Open();
            //MySqlCommand selectRegions = new MySqlCommand($"SELECT * FROM region WHERE okato = '{region}';", connection);
            //if (!selectRegions.ExecuteReader().Read()) return false;
            return true;
        }
        private static string GetNum(string mark)
        {
            if (!CheckMark(mark)) return mark;           
            return $"{mark[1]}{mark[2]}{mark[3]}";
        }
        private static string GetSerial(string mark)
        {
            if (!CheckMark(mark)) return mark;
            return $"{mark[0]}{mark[4]}{mark[5]}";
        }
        public static string GetNextMarkAfter(string mark)
        {
            //if (!CheckMark(mark)) return mark;
            string nextMark = "";
            //a, В, Е, К, М, Н, О, Р, С, Т, У и Х
            Dictionary<char, string> nextSerial = new Dictionary<char, string>()
            {
                ['a'] = "b",
                ['b'] = "e",
                ['e'] = "k",
                ['k'] = "m",
                ['m'] = "h",
                ['h'] = "o",
                ['o'] = "p",
                ['p'] = "c",
                ['c'] = "t",
                ['t'] = "y",
                ['y'] = "x",
                ['x'] = "a"
            };
            if (!GetNum(mark).Equals("999"))
            {
                int nextNum = int.Parse(GetNum(mark)) + 1;
                if (nextNum < 10)
                {
                    nextMark = mark.Remove(1, 3);
                    nextMark = nextMark.Insert(1, $"00{nextNum.ToString()}");
                }
                else if (nextNum < 100)
                {
                    nextMark = mark.Remove(1, 3);
                    nextMark = nextMark.Insert(1, $"0{nextNum.ToString()}");
                }
                else if (nextNum < 1000)
                {
                    nextMark = mark.Remove(1, 3);
                    nextMark = nextMark.Insert(1, $"{nextNum.ToString()}");
                }
                return nextMark;
            }
            else if (GetSerial(mark).Equals("xxx")) return mark;
            else
            {
                
                if (mark[5] != 'x')
                {
                    nextMark = mark.Insert(5, nextSerial[mark[5]]);
                    nextMark = nextMark.Remove(6, 1);
                    nextMark = nextMark.Replace("999", "001");
                }
                else if (mark[4] != 'x')
                {
                    nextMark = mark.Insert(4, nextSerial[mark[5]]);
                    nextMark = nextMark.Remove(5, 1);
                    nextMark = nextMark.Replace("999", "001");
                }
                else if (mark[0] != 'x')
                {
                    nextMark = mark.Insert(0, nextSerial[mark[5]]);
                    nextMark = nextMark.Remove(1, 1);
                    nextMark = nextMark.Replace("999", "001");
                }
                return nextMark;
            }
            
        }
        public static string GetNextMarkInRange(string prevmark, string rangestart, string rangeend)
        {
            //if (!(CheckMark(prevmark) && CheckMark(rangestart) && CheckMark(rangeend))) return "";
            string nextMark = GetNextMarkAfter(prevmark);
            if (string.Compare(GetSerial(nextMark), GetSerial(rangeend)) == 0){
                if (int.Parse(GetNum(nextMark)) <= int.Parse(GetNum(nextMark))) return nextMark;
                return "out of stack";
            }
            else if(string.Compare(GetSerial(nextMark), GetSerial(rangeend)) < 0 && string.Compare(GetSerial(nextMark), GetSerial(rangestart)) >= 0)
            {
                return nextMark;
            }
            return "out of stack";
        }
        public static int CountNextMarkInRange(string prevmark, string rangestart, string rangeend)
        {
            //if (!(CheckMark(prevmark) && CheckMark(rangestart) && CheckMark(rangeend))) return 0;
            string nextMark =prevmark;
            int i  = 0;
            do
            {
                i++;
                nextMark = GetNextMarkInRange(nextMark, rangestart,rangeend);
                //if (nextMark == "out of stack") break;                          
            } while (nextMark != "out of stack");
            return i--;
        }
    }
}
