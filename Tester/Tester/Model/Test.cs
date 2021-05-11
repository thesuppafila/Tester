using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Tester.Views;
using System.Windows.Forms;

namespace Tester.Model
{
    [Serializable]
    public class Test : NotifyPropertyChanged
    {
        private int startIndex;
        public int StartIndex
        {
            get => startIndex;
            set {
                startIndex = value;
                OnPropertyChanged("StartIndex");
            }
        }

        private int endIndex;
        public int EndIndex
        {
            get => endIndex;
            set {
                endIndex = value;
                OnPropertyChanged("EndIndex");
            }
        }

        private int _questionCount;
        public int QuestionCount
        {
            get => _questionCount;
            set {
                _questionCount = value;
                OnPropertyChanged("QuestionCount");
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private ObservableCollection<IQuestion> questions;
        public ObservableCollection<IQuestion> Questions
        {
            get => questions;
            set
            {
                questions = value;
                OnPropertyChanged("Questions");
            }
        }

        public Test()
        {
            Questions = new ObservableCollection<IQuestion>();
            QuestionCount = 20;
        }

        public Test(Test test) : this()
        {
            Name = new string(test.Name.ToCharArray());
            foreach (IQuestion q in test.Questions)
                Questions.Add((IQuestion)q.Clone());
        }

        public Test(string name, ObservableCollection<IQuestion> questionsList) : this()
        {
            Name = name;
            Questions = questionsList;
        }

        public void LoadFromFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                if (fileInfo.Extension.ToLower() == ".txt")
                {

                    var questionBones = Regex.Matches(File.ReadAllText(openFileDialog.FileName), @"(?<=\?)(.)*\n(\#.*\n)*", RegexOptions.Multiline);
                    foreach (var bone in questionBones)
                        Questions.Add(new Question(bone.ToString()));
                }
                if (fileInfo.Extension.ToLower() == ".xml")
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Test));

                    using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
                    {
                        Test test = (Test)formatter.Deserialize(fs);
                        this.Name = test.Name;
                        foreach (Question q in test.Questions)
                            Questions.Add(q);
                    }
                }
            }
        }

        public void LoadFile()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Test));

            using (FileStream fs = new FileStream("Data\\localTest.xml", FileMode.OpenOrCreate))
            {
                Test test = (Test)formatter.Deserialize(fs);
                this.Name = test.Name;
                foreach (Question q in test.Questions) //почему то не работает
                    Questions.Add(q);

            }
        }

        public void SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Test));

                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
            }
        }

        public void Save()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Test));
            string[] usedNames = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
            if (!usedNames.Contains(AppDomain.CurrentDomain.BaseDirectory + Name + ".xml"))
                using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + Name + ".xml", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, this);
                }
            else
            {
                ExceptionView exceptionView = new ExceptionView();
                exceptionView.exceptionBodyTextBlock.Text = "Имя занято";
                exceptionView.Show();
            }
        }

        public bool IsValid()
        {
            if (Name == null || Name == string.Empty)
                return false;

            if (Questions.Count == 0)
                return false;
            return true;
        }

        public Ticket GetTicket()
        {
            Ticket ticket = new Ticket();
            for (int i = 0; i < _questionCount; i++)
                ticket.Questions.Add(Questions[Randomizer.Next(startIndex, Questions.Count)]);
            ticket.Variant = Randomizer.Next(0, 100);
            return ticket;
        }

        public Ticket GetTicket(int countQuestion, int startIndex, int endIndex)
        {
            Ticket ticket = new Ticket();
            for (int i = 0; i < countQuestion; i++)
                ticket.Questions.Add(Questions[Randomizer.Next(startIndex, endIndex)]);
            ticket.Variant = Randomizer.Next(0, 100);
            return ticket;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
