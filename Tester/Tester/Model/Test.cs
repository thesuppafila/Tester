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

namespace Tester.Model
{
    [Serializable]
    public class Test : INotifyPropertyChanged
    {
        private string name;

        private ObservableCollection<Question> questionsList;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Test");
            }
        }

        public ObservableCollection<Question> QuestionsList
        {
            get { return questionsList; }
            set
            {
                questionsList = value;
                OnPropertyChanged("QuestionList");
            }
        }

        public Test()
        {
            QuestionsList = new ObservableCollection<Question>();
        }

        public void AddQuestion(Question question)
        {
            QuestionsList.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            if (QuestionsList.Contains(question))
                QuestionsList.Remove(question);
        }

        public Ticket CreateTicket()
        {
            return new Ticket();
        }

        public void LoadFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                if (fileInfo.Extension.ToLower() == ".txt")
                {

                    var questionBones = Regex.Matches(File.ReadAllText("Data\\questions.txt"), @"(?<=\?)(.)*\n(\#.*\n)*", RegexOptions.Multiline);
                    foreach (var bone in questionBones)
                        QuestionsList.Add(new Question(bone.ToString()));
                }
                if (fileInfo.Extension.ToLower() == ".xml")
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Test));

                    using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
                    {
                        Test test = (Test)formatter.Deserialize(fs);
                        foreach (Question q in test.QuestionsList) //почему то не работает
                            QuestionsList.Add(q);
                    }
                }
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

        public bool IsValid()
        {
            if (Name == null || Name == string.Empty)
                return false;

            if (QuestionsList.Count == 0)
                return false;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
