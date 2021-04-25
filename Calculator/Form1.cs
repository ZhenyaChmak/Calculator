using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Configuration;
using System.Xml;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 9;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "+";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ",";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "-";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "*";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "/";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "(";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ")";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + "^";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int size = textBox1.Text.Length - 1;
            string text = textBox1.Text;
            textBox1.Text = "";
            for(int i = 0; i < size; i++)
            {
                textBox1.Text = textBox1.Text + text[i];
            }
        }


       public string CreateStek (string input_string)
        {
             string[] outputMas = new string[0];
             string[] outputStack = new string[0];
            
            for(int i=0; i<input_string.Length; i++)
            {
                string temp = "";

                if (input_string[i] == '-' && ((i > 0 && !char.IsDigit(input_string[i - 1]) && input_string[i-1] !=')') || i == 0))
                {
                    i++;
                    temp += "-";
                }

                if (char.IsDigit(input_string[i]))
                {
                   
                    while (i < input_string.Length && (char.IsDigit(input_string[i]) || input_string[i] == ','))
                    {
                        temp += input_string[i].ToString();
                        i++;
                    }
                    i--;
                    Array.Resize(ref outputMas, outputMas.Length + 1);
                    outputMas[outputMas.Length - 1] = temp;
                }

                if (input_string[i] == '*' || input_string[i] =='/')
                {
                    if (outputStack.Length != 0)
                    {
                        if (outputStack[outputStack.Length - 1] != "*" & outputStack[outputStack.Length - 1] != "/" 
                            & outputStack[outputStack.Length - 1] != "^")
                        {
                            Array.Resize(ref outputStack, outputStack.Length + 1);
                            outputStack[outputStack.Length - 1] = input_string[i].ToString();
                        }
                        else
                        {
                            Array.Resize(ref outputMas, outputMas.Length + 1);
                            outputMas[outputMas.Length - 1] = outputStack[outputStack.Length - 1];
                            outputStack[outputStack.Length - 1] = input_string[i].ToString();
                        }
                    }
                    else
                    {
                        Array.Resize(ref outputStack, outputStack.Length + 1);
                        outputStack[outputStack.Length - 1] = input_string[i].ToString();
                    }
                }

                if (input_string[i] == '+' || input_string[i] == '-')
                {
                    m:
                    if(outputStack.Length != 0)
                    {
                        if (outputStack[outputStack.Length - 1] == "(")
                        {
                            Array.Resize(ref outputStack, outputStack.Length + 1);
                            outputStack[outputStack.Length - 1] = input_string[i].ToString();
                        }
                        else
                        {
                            Array.Resize(ref outputMas, outputMas.Length + 1);
                            outputMas[outputMas.Length - 1] = outputStack[outputStack.Length - 1];
                            Array.Resize(ref outputStack, outputStack.Length - 1);
                            goto m;
                        }
                    //    Array.Resize(ref outputStack, outputStack.Length + 1);
                        outputStack[outputStack.Length - 1] = input_string[i].ToString();
                    }
                    else
                    {
                        Array.Resize(ref outputStack, outputStack.Length + 1);
                        outputStack[outputStack.Length - 1] = input_string[i].ToString();
                    }
                }

                if(input_string[i] == '^')
                {
                    Array.Resize(ref outputStack, outputStack.Length + 1);
                    outputStack[outputStack.Length - 1] = input_string[i].ToString();
                }

                if(input_string[i] == '(')
                {
                    Array.Resize(ref outputStack, outputStack.Length + 1);
                    outputStack[outputStack.Length - 1] = input_string[i].ToString();

                }

                if(input_string[i] == ')')
                {
                    int index = outputStack.Length - 1;
                    while(outputStack[index] != "(")
                    {
                        index--;
                    }
                    int c=1;
                    string[] temper = new string[0];
                    for (int a = index+1; a < outputStack.Length; a++)
                    {
                        Array.Resize(ref temper, temper.Length + 1);
                        temper[temper.Length - 1] = outputStack[a];
                        c++;
                    }
                    Array.Reverse(temper);
                    Array.Resize(ref outputStack, outputStack.Length - c);

                    for(int b = 0; b < temper.Length; b++)
                    {
                        Array.Resize(ref outputMas, outputMas.Length + 1);
                        outputMas[outputMas.Length - 1] = temper[b];
                    }
                }
            }
            
            Array.Resize(ref outputMas, outputMas.Length + outputStack.Length);
            Array.Reverse(outputStack);
            int j = 0;
            for (int i=outputMas.Length-outputStack.Length; i<outputMas.Length; i++)
            {
                outputMas[i] = outputStack[j];
                j++;
            }

            string outTemp = "";
            for(int i = 0; i < outputMas.Length; i++)
            {
                outTemp += outputMas[i]+' ';
            }

            return outTemp;
        }

        
        public double Solution (string stack)
        {
            string[] mas = stack.Split(' ');
            string st;
            for (int i = 0; i < mas.Length; i++)
                switch (mas[i])
                {
                    case "*":
                        st = (double.Parse(mas[i - 2]) * double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = st;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "/":
                        st = (double.Parse(mas[i - 2]) / double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = st;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "+":
                        st = (double.Parse(mas[i - 2]) + double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = st;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "-":
                        st = (double.Parse(mas[i - 2]) - double.Parse(mas[i - 1])).ToString();
                        mas[i - 2] = st;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                    case "^":
                        st = (Math.Pow(double.Parse(mas[i - 2]), double.Parse(mas[i - 1]))).ToString();
                        mas[i - 2] = st;
                        for (int j = i - 1; j < mas.Length - 2; j++)
                            mas[j] = mas[j + 2];
                        Array.Resize(ref mas, mas.Length - 2);
                        i -= 2;
                        break;
                }

            return double.Parse(mas[0]);
        }

        public void Create(string inputString, double answer)
        {
            string WayJournal = ConfigurationManager.AppSettings["WayJournal"];
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(WayJournal);
            XmlElement xRoot = xDoc.DocumentElement;

            XmlElement userElement = xDoc.CreateElement("Users");

            // создаем элементы
            XmlElement RecordingDate = xDoc.CreateElement("RecordingDate");
            XmlElement Expression = xDoc.CreateElement("Expression");
            XmlElement Answer = xDoc.CreateElement("Answer");
            // создаем значения
            XmlText _recordingDate = xDoc.CreateTextNode(DateTime.Now.ToString());
            XmlText _inputString = xDoc.CreateTextNode(inputString);
            XmlText _outTemp = xDoc.CreateTextNode(answer.ToString());

            //добавляем узлы
            RecordingDate.AppendChild(_recordingDate);
            Expression.AppendChild(_inputString);
            Answer.AppendChild(_outTemp);

            userElement.AppendChild(RecordingDate);
            userElement.AppendChild(Expression);
            userElement.AppendChild(Answer);
            
            xRoot.AppendChild(userElement);
            xDoc.Save(WayJournal);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            double result = Solution(CreateStek(textBox1.Text));
            Create(textBox1.Text, result);
            textBox1.Text = result.ToString();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Корпорация Home (Zhenya).");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Версия 1.0"+
                "\n@Корпорация Home(Zhenya),2020. Все права защищены.",
                "О программе \"Calculator\"",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
