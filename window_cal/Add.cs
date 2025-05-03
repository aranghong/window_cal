using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace window_cal
{
    internal class Add
    {
        public double add(string word)
        {
            // 22+3

            int index = word.IndexOf("+");  //2

            string a = word.Substring(0, index).Trim(); //0인덱스부터 index개수만큼 가지고 오겠다
            double s = double.Parse(a); //22

            string b = word.Substring(index + 1).Trim();    //인덱스+1부터 끝까지 가지고 오겠다
            double ss = double.Parse(b);    //3

            return s + ss;
        }
    }
}
