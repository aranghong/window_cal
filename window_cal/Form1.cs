using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace window_cal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //폼 로드될 때, 모든 버튼에 태그 속성 부여
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Tag = "theme";
                }
            }

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

            // 전체 입력텍스트에 e가 포함되어있으면, 우선적으로 예외처리
            string eTest = textBox1.Text;
            // e 앞/뒤가 비어있는 잘못된 입력 형식 예외처리
            //5e, e5
            if (eTest == "e" ||
                eTest.EndsWith("e") ||
                eTest.EndsWith("e+") || eTest.EndsWith("e-") ||
                eTest.StartsWith("e"))
            {
                MessageBox.Show("e 앞에는 숫자가, e 뒤에는 지수가 필요합니다.");
                return;
            }


            // 3+5+5 처럼 마지막 문자열이 숫자 or 팩토리얼인지 체크 아닐 경우 messageBox 출력                           
            if (char.IsDigit(ch[ch.Length -1]) || (ch[ch.Length - 1].Equals('!')) || (ch[ch.Length - 1].Equals('π')))
            {
                for (int i = 0; i < ch.Length; i++)
                {
                    // 지수 표현까지 포함되도록 처리 (exp)
                    if (char.IsDigit(ch[i]) || ch[i] == '.' || ch[i] == 'e' ||
                        (ch[i] == '+' && temp.EndsWith("e")) ||
                        (ch[i] == '-' && temp.EndsWith("e"))) // 숫자인지 체크 숫자일 경우 true
                    {

                        temp += ch[i];  // 숫자 or e-지수 표현

                       

                    }

                    else if (ch[i] == '-' && (i == 0 || !char.IsDigit(ch[i - 1]))) // 음수 시작할 경우 처리
                    {
                        temp += ch[i];  // 음수 시작 허용
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(temp))
                        {
                            
                            if (double.TryParse(temp, System.Globalization.NumberStyles.Float,
                                System.Globalization.CultureInfo.InvariantCulture, out double parsedNumber))
                            {
                                //textBox2.Text += $"{temp} = {parsedNumber}\r\n";  //숫자 파싱 성공시 출력
                                doubles.Add(parsedNumber);

                                if (temp.Contains('e'))
                                {
                                    textBox2.Text += $"{temp} = {parsedNumber}\r\n";  //숫자 파싱 성공시 출력
                                }
                            }
                            else
                            {
                                MessageBox.Show($"잘못된 숫자 형식입니다: {temp}");
                                return; // 계산 중단
                            }
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
                    // 1차 우선순위 연산 처리 [^]
                    for (int i = 0; i < chars.Count; i++)
                    {
                        if (chars[i].Equals("^")) // xʸ 거듭제곱
                        {
                            if (i >= doubles.Count - 1)
                            {
                                MessageBox.Show("올바른 수식이 아닙니다! (^앞에 수를 넣어주세요.)");
                                continue; // 오류 메시지 출력 후 계산 생략
                            }

                            textBox2.Text += $"{doubles[i]}^{doubles[i + 1]} = ";
                            doubles[i] = Math.Pow(doubles[i], doubles[i + 1]);
                            textBox2.Text += doubles[i] + "\r\n";
                            chars.RemoveAt(i);
                            doubles.RemoveAt(i + 1);
                            i--;
                            //result += doubles[i];
                            continue;
                        }

                        if (chars[i].Equals("√")) // 제곱근
                        {

                            double sqrtTarget = doubles[i];

                            if (sqrtTarget < 0)
                            {
                                // 음수일 경우 음수 부호 유지한 채 절댓값 처리
                                textBox2.Text += $"√({sqrtTarget})= ";
                                doubles[i] = -Math.Sqrt(Math.Abs(sqrtTarget));
                            }
                            else
                            {
                                textBox2.Text += $"√{sqrtTarget}= ";
                                doubles[i] = Math.Sqrt(sqrtTarget);
                            }

                            chars.RemoveAt(i);
                            textBox2.Text += doubles[i] + "\r\n";
                            i--;
                            continue;
                        }

                        if (chars[i].Equals("π")) // π 처리
                        {
                            if (i < doubles.Count)
                            {
                                textBox2.Text += $"{doubles[i]}π= {doubles[i] * Math.PI} \r\n";
                                doubles[i] = (doubles[i] * Math.PI);
                                chars.RemoveAt(i);
                                i--;
                                continue;
                            }
                            else
                            {
                                textBox2.Text += $"π={Math.PI} + \r\n";
                                doubles.Add(Math.PI);
                                chars.RemoveAt(i);
                                i--;
                                continue;
                            }
                        }

                        if (chars[i].Equals("!")) // 팩토리얼
                        {
                            Factorial fac = new Factorial();

                            if (i < doubles.Count)
                            {
                                textBox2.Text += $"fact({doubles[i]})= ";
                                doubles[i] = fac.factorialCal(doubles[i].ToString());
                                chars.RemoveAt(i);
                                //result += doubles[i];
                                textBox2.Text += doubles[i] + "\r\n";
                                i--;
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("올바른 수식이 아닙니다! (!앞에 수를 넣어주세요.)");
                            }
                        }

                    }
                // 1.5차 우선순위 연산 처리 [*, /]
                for (int i = 0; i < chars.Count; i++)
                {
                    Console.WriteLine(chars[i]);

                    // 3+2*5 일 경우, (2*5)곱셈부터 우선적으로 연산해야 함
                    if (chars[i] == "x" || chars[i] == "%")
                    {

                        if (chars[i].Equals("x"))
                        {
                            double tmp = 1;
                            Mul mulCal = new Mul();
                            tmp = mulCal.mul(doubles[i], doubles[i + 1]);

                            doubles[i] = tmp;

                            chars.RemoveAt(i);
                            doubles.RemoveAt(i + 1);
                            i--;

                            Console.WriteLine(tmp);
                            continue;

                        }

                        if (chars[i].Equals("%"))
                        {
                            double tmp = 0;
                            Div divCal = new Div();
                            tmp = divCal.div(doubles[i], doubles[i + 1]);

                            doubles[i] = tmp;

                            chars.RemoveAt(i);
                            doubles.RemoveAt(i + 1);
                            i--;
                            continue;

                        }

                    }
                }

                //2차 우선순위 낮은 연산 실행
                // 💡 먼저 result를 기본 값으로 설정

                if (chars.Count == 0 && doubles.Count == 1)
                {
                    result = doubles[0];  // 예: 2^3만 입력된 경우 -> 연산자 없이 숫자만 있을 때
                    if (!textBox2.Text.Contains($"{result}")) // 중복 방지
                    {
                        textBox2.Text += $"{textBox1.Text} = {result}";
                    }
                }
                else
                {
                    for (int i = 0; i < chars.Count; i++)
                    {
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

                            Console.WriteLine(result);


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

                        

                    }
                    textBox2.Text += $"\r\n{textBox1.Text} = {result}";


                }

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
            if (text.Equals("Clear"))
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

                    string tmp = cal.abs(value);
                    textBox2.Text = $"|{value}| = {tmp}";
                }
                else
                {
                    MessageBox.Show("절댓값을 적용할 수 있는 숫자만 입력해 주세요.");
                }
            }
            else if (text.Equals("x²"))
            {
                if (double.TryParse(textBox1.Text, out double value))
                {
                    string tmp = (value * value).ToString();
                    textBox2.Text = $"{value}² = {tmp}";
                }
            }
            else if (text.Equals("+/-"))
            {
                if (double.TryParse(textBox1.Text, out double value))
                {
                    textBox2.Text = (value * -1).ToString();
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



        // 배경색 On
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.BackColor = ColorTranslator.FromHtml("#E5D0AC");
                textBox2.BackColor = ColorTranslator.FromHtml("#FEF9E1");

                //태그 속성 가진 버튼에 테마 색상 반영
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button btn && (btn.Tag?.ToString() == "theme"))
                    {
                        btn.BackColor = textBox2.BackColor;
                    }
                }

            }
        }

        // 배경색 Off
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                this.BackColor = ColorTranslator.FromHtml("#D2E0FB");
                textBox2.BackColor = ColorTranslator.FromHtml("#EEF5FF");

                //태그 속성 가진 버튼에 테마 색상 반영

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button btn && (btn.Tag?.ToString() == "theme"))
                    {
                        btn.BackColor = textBox2.BackColor;
                    }
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                this.BackColor = SystemColors.Control;
                textBox2.BackColor = SystemColors.ControlLightLight;

                //태그 속성 가진 버튼에 테마 색상 반영

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button btn && (btn.Tag?.ToString() == "theme"))
                    {
                        btn.BackColor = textBox2.BackColor;
                    }
                }
            }
        }

    }
}
