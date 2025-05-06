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
            double result = 0; // 최종 출력할 연산 결과

            string symbol = textBox1.Text;

            char[] ch = symbol.ToCharArray(); //22+33-7*8

            List<string> chars = new List<string>(); //+,-,*
            List<double> doubles = new List<double>(); // 22,33,7,8
            string temp = ""; // 22+33-7*8 중 숫자 걸러내는 용

            // 3+5+5 처럼 마지막 문자열이 숫자 or 팩토리얼인지 체크 아닐 경우 messageBox 출력
            if (char.IsDigit(ch[ch.Length -1]) || (ch[ch.Length - 1].Equals('!')) || (ch[ch.Length - 1].Equals('π')))
            {
                for (int i = 0; i < ch.Length; i++)
                {
                    if (char.IsDigit(ch[i]) || ch[i] == '.') // 숫자인지 체크 숫자일 경우 true
                    {
                        temp += ch[i];
                    }
                    else if (ch[i] == '-' && (i == 0 || !char.IsDigit(ch[i - 1]))) // 음수 시작할 경우 처리
                    {
                        temp += ch[i];
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(temp))
                        {
                            doubles.Add(double.Parse(temp)); // 지금까지 만든 숫자를 추가
                            temp = "";
                        }
                        chars.Add(ch[i].ToString()); // 연산자 저장
                    }

                }

                // 마지막 숫자 처리
                if (!string.IsNullOrEmpty(temp)) // temp가 null인지 체크
                {
                    doubles.Add(double.Parse(temp));
                }


                // 기본연산
                for (int i = 0; i < chars.Count; i++)
                {

                    if (chars[i].Equals("x"))
                    {
                        Mul mulCal = new Mul();

                        if (i == 0)
                        {
                            result += mulCal.mul(doubles[i], doubles[i + 1]);
                        }
                        else
                        {
                            result *= mulCal.mul(doubles[i + 1]);
                        }

                    }

                    if (chars[i].Equals("%"))
                    {
                        Div divCal = new Div();

                        if (i == 0)
                        {
                            result += divCal.div(doubles[i], doubles[i + 1]);
                        }
                        else
                        {
                            result /= divCal.div(doubles[i + 1]);
                        }

                    }

                    if (chars[i].Equals("+"))
                    {
                        Add addCal = new Add();

                        if (i == 0)
                        {
                            result += addCal.add(doubles[i], doubles[i + 1]);
                        }
                        else
                        {
                            result += addCal.add(doubles[i + 1]);
                        }

                    }

                    if (chars[i].Equals("-"))
                    {
                        Sub subCal = new Sub();

                        if (i == 0)
                        {
                            result += subCal.sub(doubles[i], doubles[i + 1]);
                        }
                        else
                        {
                            result -= subCal.sub(doubles[i + 1]);
                        }

                    }

                    if (chars[i].Equals("√")) // 제곱근
                    {
                        textBox2.Text += $"√{doubles[i]}= ";
                        doubles[i] = Math.Sqrt(doubles[i]);
                        chars.RemoveAt(i);
                        result += doubles[i];
                        textBox2.Text += doubles[i] + "\r\n";
                        continue;
                    }

                    if (chars[i].Equals("π")) // π 처리
                    {
                        if (i < doubles.Count) {
                            textBox2.Text += $"{doubles[i]}π= {doubles[i] * Math.PI} \r\n";
                            doubles[i] = (doubles[i] * Math.PI);
                            chars.RemoveAt(i);
                            result += doubles[i];
                            continue;
                        }else
                        {
                            textBox2.Text += $"π={Math.PI} + \r\n";
                            doubles.Add(Math.PI);
                            chars.RemoveAt(i);
                            result += doubles[i];
                            continue;
                        }
                    }

                    if (chars[i].Equals("!")) // 팩토리얼
                    {
                        Factorial fac = new Factorial();

                        if (i < doubles.Count) {
                            textBox2.Text += $"fact({doubles[i]})= ";
                            doubles[i] = fac.factorialCal(doubles[i].ToString());
                            chars.RemoveAt(i);
                            result += doubles[i];
                            textBox2.Text += doubles[i] + "\r\n";
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("올바른 수식이 아닙니다! (!앞에 수를 넣어주세요.)");
                        }
                    }

                    if (chars[i].Equals("^")) // xʸ 거듭제곱
                    {
                        if (i >= doubles.Count - 1)
                        {
                            MessageBox.Show("올바른 수식이 아닙니다! (^앞에 수를 넣어주세요.)");
                            continue; // 오류 메시지 출력 후 계산 생럍
                        }

                        textBox2.Text += $"{doubles[i]}^{doubles[i + 1]} = ";
                        doubles[i] = Math.Pow(doubles[i], doubles[i + 1]);
                        chars.RemoveAt(i); 
                        doubles.RemoveAt(i + 1); 
                        result += doubles[i];
                        textBox2.Text += doubles[i] + "\r\n";
                        continue;
                    }

                    if (chars[i].Equals("e")) // exp
                    {
                        textBox2.Text += $"EXP({doubles[i]})= {Math.Exp(doubles[i])} \r\n";
                        result += Math.Exp(doubles[i]);
                        doubles.Add(Math.Exp(doubles[i]));
                        chars.RemoveAt(i);
                        continue;
                    }

                }

                textBox2.Text += $"{textBox1.Text} = {result}";
            }
            else 
            {
                MessageBox.Show("올바른 계산식이 아닙니다.");
            }
        }

        // 텍스트 입력 박스
        private void CommonBtn(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string text = btn.Text;

            // 클리어 버튼
            if (text.Equals("C"))
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else if (text.Equals("BackSpace"))
            {
                if (textBox1.Text.Length > 0)
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                }
            }
            else if (text.Equals("|x|"))
            {
                if (double.TryParse(textBox1.Text, out double value))
                {
                    AbsCal cal = new AbsCal();
                    textBox1.Text = cal.abs(value);
                    textBox2.Text = $"|{value}| = {textBox1.Text}";
                }
                else
                {
                    MessageBox.Show("절댓값을 적용할 수 있는 숫자만 입력해 주세요.");
                }
            }else if (text.Equals("xʸ"))
            {
                if (double.TryParse(textBox1.Text, out double value))
                {

                }

            }
            else if (text.Equals("x²"))
            {
                if (double.TryParse(textBox1.Text, out double value))
                {
                    textBox1.Text = (value * value).ToString();
                    textBox2.Text = $"{value}² = {textBox1.Text}";
                }
            }
            else if (text.Equals("+/-"))
            {
                if (double.TryParse(textBox1.Text, out double value))
                {
                    textBox1.Text = (value * -1).ToString();
                }else
                {
                    MessageBox.Show("올바른 식이 아닙니다.");
                }
            }
            else
            {
                textBox1.Text += text;
            }
        }

    }
}
