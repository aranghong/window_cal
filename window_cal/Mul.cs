using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace window_cal
{
    internal class Mul
    {
        public double mul(string word)
        {
            int index = word.IndexOf("X");
            string a = word.Substring(0, index).Trim();
            double s = double.Parse(a);
            string b = word.Substring(index + 2).Trim();
            double ss = double.Parse(b);
            return s * ss;
        }
    }
}