using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
            //List<Question> list = CreateQuestionsCollection("Data\\questions.txt");
            //TicketCreator ticketCreator = new TicketCreator(list);
            //Ticket ticket = ticketCreator.CreateTicket(20, 0, 60);
            //ticket.SaveToFile("e:\\textFile.txt");
        }

        public List<Question> CreateQuestionsCollection(string path)
        {
            List<Question> questions = new List<Question>();
            var questionBones = Regex.Matches(File.ReadAllText(path), @"(?<=\?)(.)*\n(\#.*\n)*", RegexOptions.Multiline);
            foreach (var bone in questionBones)
                questions.Add(new Question(bone.ToString()));
            return questions;
        }

        private void создатьНовыйТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            string fileName;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                questionsList = new List<Question>();

                questionsList = CreateQuestionsCollection(fileName);
                foreach (Question q in questionsList)
                    listBox1.Items.Add(q.GetBody());
            }
        }

        private void createTestButton_Click(object sender, EventArgs e)
        {
            if (questionsList == null)
            {
                MessageBox.Show("Необходимо загрузить файл теста.");
                return;
            }
            if (textBox1.Text == null)
            {
                MessageBox.Show("Введите номер группы.");
                return;
            }

            TicketCreator ticketCreator = new TicketCreator(questionsList);
            ticketList = new List<Ticket>();

            ticketList = ticketCreator.CreateTickets(int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text), int.Parse(textBox5.Text));
            
            int index = 0;

            foreach (Ticket t in ticketList)            
                t.SaveToFile(String.Format("{3}\\{0}_{1}.txt", textBox1.Text, ++index, textBox1.Text));
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = Directory.GetCurrentDirectory();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(folderDialog.SelectedPath).GetFiles())
                {
                    var testResult = new TestResult();
                    if (testResult.TryParseFile(fileInfo.FullName))
                    {
                        listBox1.Items.Add(string.Format("{0}: {1} баллов", testResult.Name, testResult.Result));
                    }
                    else
                    {
                        listBox1.Items.Add(string.Format("Не удалось прочитать файл: {0}", fileInfo.FullName));
                    }
                }
            }
        }
    }
}
