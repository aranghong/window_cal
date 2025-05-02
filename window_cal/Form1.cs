using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace window_cal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += " + "; 
        }

        public int CalResult(string word)
        {
            int index = word.IndexOf("+"); 

            string a = word.Substring(0, index).Trim();    
            int s = int.Parse(a);      
            
            string b = word.Substring(index + 2).Trim();
            int ss = int.Parse(b);

            return s+ss;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int result = CalResult(textBox1.Text);
            textBox2.Text += $"결과: {result}";
        }
    }
}
