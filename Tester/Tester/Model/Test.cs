using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tester.Views;

namespace Tester.Model
{
    [Serializable]
    public class Test : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions
        {
            get { return questions; }
            set
            {
                questions = value;
                OnPropertyChanged("Questions");
            }
        }

        public Test()
        {
            Questions = new ObservableCollection<Question>();
        }

        public Test(string name, ObservableCollection<Question> questionsList)
        {
            Name = name;
            Questions = questionsList;
        }


        public void LoadFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
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
            if (saveFileDialog.ShowDialog() == true)
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

        public Ticket GetTicket(int countQuestion, int startIndex, int endIndex)
        {
            Ticket ticket = new Ticket();
            for (int i = 0; i < countQuestion; i++)
                ticket.Questions.Add(Questions[Randomizer.Next(startIndex, endIndex)]);
            ticket.Variant = Randomizer.Next(0, 100);
            return ticket;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
