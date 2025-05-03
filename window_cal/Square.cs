using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace window_cal
{
    internal class Square
    {

        public double squ(string word)
        {
            //√(16)
            int index = word.IndexOf("(");  //1
            int index2 = word.LastIndexOf(")"); //4

            string a = word.Substring(index + 1, index2 - index - 1).Trim();    //index+1부터 index2-index-1개 만큼 자름 -> 숫자만 남음
            double s = double.Parse(a);
            
            return Math.Sqrt(s);
        }
    }
}
