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
        public MainWindow()
        {
            InitializeComponent();
            List<Question> list = CreateQuestionsCollection("Data\\questions.txt");
            TicketCreator ticketCreator = new TicketCreator(list);
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
    }
}
