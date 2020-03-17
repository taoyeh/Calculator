using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Cal : Form
    {
        bool isAppend=false;
        string preOper = null;
        double result;
        public Cal()
        {
            InitializeComponent();
        }

        private void NumClicked(object sender, EventArgs e)
        {
            string num = ((Button)sender).Text;
            NumInput(num);
        }

        private void NumInput(string num)
        {
            //预算完成，把textbox2的值清空
            if (!isAppend && textBox1.Text == textBox2.Text) textBox2.Text = "";
            if (isAppend) textBox1.Text += num;
            else textBox1.Text = num;

            textBox2.Text += num;
            isAppend = true;
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void DoubleClicked(object sender, EventArgs e)
        {
            string curOper = ((Button)sender).Text;
            DoubleOperInput(curOper);
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void DoubleOperInput(string curOper)
        {
            double curNum = double.Parse(textBox1.Text);
            if (preOper == null)
            {
                preOper = curOper;
                result = curNum;
            }
            else
            {
                switch (preOper)
                {
                    case "+": result += curNum; break;
                    case "-": result -= curNum; break;
                    case "*": result *= curNum; break;
                    case "/": result /= curNum; break;
                }
                preOper = curOper;
            }
            if(preOper!="=")
            textBox2.Text += preOper;
            isAppend = false;
        }

        private void SingleClicked(object sender, EventArgs e)
        {
            string curOper = ((Button)sender).Text;
            double curNum = double.Parse(textBox1.Text);
            switch (curOper)
            {
                case "Sqrt": curNum=Math.Sqrt(curNum); break;
                case "1/x": curNum =1/ curNum; break;
                case "x^2": curNum *= curNum; break;
                case "sin": curNum = Math.Sin(Math.PI * (curNum / 180)) ; break;
                case "cos": curNum = Math.Cos(Math.PI * (curNum / 180)); break;
                case "tan": curNum = Math.Tan(Math.PI * (curNum / 180)); break;
            }
            textBox1.Text = curNum.ToString();
            textBox2.Text = textBox1.Text;
            isAppend = false;
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void DotClicked(object sender, EventArgs e)
        {
            if(isAppend)
            {
                if (textBox1.Text.ToString().IndexOf('.')  < 0  && textBox1.Text.Length != 0) textBox1.Text += ".";
                //backspace 之后的输入
                else if( textBox1.Text.Length==0) textBox1.Text = "0.";
            }
            else  textBox1.Text = "0.";
            textBox2.Text += '.';
            isAppend = true;
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void EqualClicked(object sender, EventArgs e)
        {
            DoubleClicked(btnEqual, null);
            preOper = null;
            MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
            sc.Language = "JavaScript";
            textBox1.Text= sc.Eval(textBox2.Text.ToString()).ToString();
            textBox2.Text = textBox1.Text;
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void BSClicked(object sender, EventArgs e)
        {
            if(isAppend)
            {
                string now = textBox1.Text.ToString();
                if(now.Length>0)
                {
                    textBox1.Text = now.Substring(0, now.Length - 1);
                    if(textBox2.Text.ToString().Length>0)
                    textBox2.Text = textBox2.Text.ToString().Substring(0, textBox2.Text.ToString().Length - 1);
                }
                
            }
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            preOper = null;
            result = 0;
            isAppend = false;
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.TextLength;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            //数字键
            if(char.IsDigit(e.KeyChar))
            {
                NumInput(e.KeyChar.ToString());
            }
            //加减乘除
            else if(e.KeyChar=='+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/')
            {
                DoubleOperInput(e.KeyChar.ToString());
            }
            //小数点
            else if(e.KeyChar=='.' || e.KeyChar == '。')
            {
                DotClicked(null, null);
            }
            //回车出结果
            else if(e.KeyChar==13)
            {
                EqualClicked(null, null);
            }
            //backspace
            else if(e.KeyChar==8)
            {
                BSClicked(null, null);
            }
            
            textBox1.SelectionStart = textBox1.TextLength;
        }
    }
}
