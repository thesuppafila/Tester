using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        public delegate double FuncDelegate(double x);

        public Form1()
        {
            InitializeComponent();
        }

        public double GetBiseсtionSolve(FuncDelegate F, double min, double max, double accuracy)
        {
            var length = max - min;
            var err = length;
            double x = 0;
            while (err > accuracy && F(x) != 0)
            {
                x = (min + max) / 2;
                if (F(min) * F(x) < 0)
                {
                    max = x;
                }
                else if (F(x) * F(max) < 0)
                {
                    min = x;
                }

                err = (max - min) / length;
            }

            return x;
        }

        public double HordeMethod(FuncDelegate F, double min, double max, double accuracy)
        {
            double tmp = default(double);
            int n = default(int);

            n = 0;
            while (Math.Abs(max - min) > accuracy)
            {
                tmp = max;
                max = max - (max - min) * F(max) / (F(max) - F(min));
                min = tmp;
                n++;
            }

            return max;
        }

        double f23(double x)
        {
            return Math.Tan(0.44 * x + 0.3) - x * x;
        }

        double f25(double x)
        {
            return (1 / Math.Tan(x)) - 0.1 * x;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                label1.Text = "Функция: tg(0.44x + 0.3) - x^2 = 0";
                label2.Text = "Метод секущих(метод хорд)";
            }
            if (listBox1.SelectedIndex == 1)
            {
                label1.Text = "Функция: ctg(x) - 0.1x = 0";
                label2.Text = "Метод половинного деления";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double min, max, accuracy;
            min = (double)numericUpDown1.Value;
            max = (double)numericUpDown2.Value;
            if (!double.TryParse(textBox1.Text, out accuracy))
                return;

            if (listBox1.SelectedIndex == 0)
            {
                FuncDelegate funcDelegate = new FuncDelegate(f23);
                double x = HordeMethod(funcDelegate, min, max, accuracy);
                richTextBox1.Text = string.Format("Вариант 23, метод хорд, решение: {0}\n", x);
            }
            if (listBox1.SelectedIndex == 1)
            {
                FuncDelegate funcDelegate = new FuncDelegate(f25);
                double x = GetBiseсtionSolve(funcDelegate, min, max, accuracy);
                richTextBox1.Text = string.Format("Вариант 25, метод половинного деления, решение: {0}\n", x);
            }
        }
    }
}