using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateTable();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CreateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[,] mass = new double[(int)numericUpDown1.Value, (int)numericUpDown1.Value];
            double[] d = new double[(int)numericUpDown1.Value];

            for (int i = 0; i < mass.GetLength(0); i++)
            {
                d[i] = Convert.ToDouble(dataGridView1[(int)numericUpDown1.Value, i].Value);
                for (int j = 0; j < mass.GetLength(1); j++)
                    mass[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
            }
            Solve solve = new Solve(mass, d);
            int k = 1;
            foreach (double x in solve.Answer)
            {
                listBox1.Items.Add("x" + k.ToString() + " = " + x.ToString());
                k++;
            }
        }

        private void CreateTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            for (int i = 0; i < numericUpDown1.Value + 1; i++)
            {
                dataGridView1.Columns.Add("", "");
                if (i == numericUpDown1.Value)
                {
                    dataGridView1.Columns[i].HeaderText = "d";
                    dataGridView1.Columns[i].Width = 55;
                }
                else
                {
                    dataGridView1.Columns[i].HeaderText = "x" + (i + 1).ToString();
                    dataGridView1.Columns[i].Width = 35;
                }
            }
            for (int i = 0; i < numericUpDown1.Value; i++)
            {

                dataGridView1.Rows.Add();
            }
            dataGridView1.RowHeadersVisible = false;

        }
    }
}
