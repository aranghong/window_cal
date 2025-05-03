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


        

        // = 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            Add add = new Add();
            double result = add.add(textBox1.Text);
            textBox2.Text += $"결과: {result}";
            
        }

        // 숫자+소수점
        private void CommonBtn(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            //string s = sender.ToString();
            string text = btn.Text;
            
            textBox1.Text += text;
        }

        // + 기호
        private void button_Add_Click(object sender, EventArgs e)
        {
            textBox1.Text += " + ";
        }
    }
}
