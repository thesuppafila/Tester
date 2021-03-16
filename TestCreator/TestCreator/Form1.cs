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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateQuestionsCollection("Data\\questions.txt");
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
    }
}
