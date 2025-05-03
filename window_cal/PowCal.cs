using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace window_cal
{
    internal class PowCal
    {
        public double powCal(string word)
        {
            //12^2

            int index = word.IndexOf("^");

            string a = word.Substring(0, index).Trim();
            double s = double.Parse(a);

            string b = word.Substring(index + 1).Trim();
            double ss = double.Parse(b);

            return Math.Pow(s, ss);
        }
    }

}
