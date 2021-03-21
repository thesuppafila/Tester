using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCreator
{
    public partial class MainWindow : Form
    {

        List<Question> questionsList;
        List<Ticket> ticketList;

        public MainWindow()
        {
            InitializeComponent();
        }

        public List<Question> CreateQuestionsCollection(string path)
        {
            List<Question> questions = new List<Question>();
            var questionBones = Regex.Matches(File.ReadAllText(path), @"(?<=\?)(.|\n)*?(?=\n\?)", RegexOptions.Multiline);
            foreach (var bone in questionBones)
            {
                questions.Add(new Question(bone.ToString()));
            }
            return questions;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void создатьНовыйТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            string fileName = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                questionsList = new List<Question>();

                questionsList = CreateQuestionsCollection(fileName);
                listBox1.Items.AddRange(questionsList.ToArray());

                numericUpDown2.Maximum = listBox1.Items.Count;
                numericUpDown4.Maximum = listBox1.Items.Count;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Maximum = numericUpDown4.Value;
        }

        private void createTestButton_Click(object sender, EventArgs e)
        {
            if (questionsList == null)
            {
                MessageBox.Show("Необходимо загрузить файл теста.");
                return;
            }
            TicketCreator ticketCreator = new TicketCreator(questionsList);
            ticketList = new List<Ticket>();
            for(int i = 0; i < numericUpDown2.Value; i++)
            {
                ticketList.Add(ticketCreator.CreateTicket((int)numericUpDown2.Value, (int)numericUpDown3.Value, (int)numericUpDown4.Value));
            }
        }
    }
}
