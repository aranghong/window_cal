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
        string status = "";

        public Form1()
        {
            InitializeComponent();
        }

        // = 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = ""; // 22 - 2
            double result = 0;

            string symbol = textBox1.Text;

            if (symbol.Contains("+"))
            {
                Add add = new Add();
                result = add.add(textBox1.Text);
            }

            if (status.Equals("add"))
            {
                Add add = new Add();
                result = add.add(textBox1.Text);
            }
            //if (status.Equals("sub"))
            //{
            //    Sub add = new Add();
            //    result = add.add(textBox1.Text);
            //}


            textBox2.Text += $"결과: {result}";
            
        }

        // 숫자+소수점
        private void CommonBtn(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            //string s = sender.ToString();
            string text = btn.Text; // 22+33
            Console.WriteLine(text);
            textBox1.Text += text;

            //if ((int.TryParse(text, out int number))) 
            //{
            //    textBox1.Text += text;
            //}
            //else
            //{
            //    //if ()
            //    //{

            //    //}
            //    //textBox1.Text += $"{d}";
            //    textBox1.Text += text;
            //}
                
            
        }

        //+ 기호
        //private void button_Add_Click(object sender, EventArgs e)
        //{
        //    textBox1.Text += "+";
        //    status = "add";
        //}

        private void button_Sub_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
            status = "sub";
        }
    }
}
