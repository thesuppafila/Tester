using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace Tester.Model
{
    [Serializable]
    public class Question : ICloneable, INotifyPropertyChanged, IQuestion
    {
        public Question()
        {
            Answers = new ObservableCollection<Answer>();
            TrueAnswers = new ObservableCollection<Answer>();
        }

        private string body;
        public string Body
        {
            get { return body; }
            set
            {
                body = value;
                OnPropertyChanged("Body");
            }
        }

        private ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
            }
        }

        private string bones;
        public string Bones
        {
            get { return bones; }
            set
            {
                bones = value;
                OnPropertyChanged("Bones");
            }
        }

        private bool isMultiple;
        public bool IsMultiple
        {
            get { return isMultiple; }
            set
            {
                isMultiple = value;
                if (TrueAnswers != null)
                    if (!isMultiple)
                        TrueAnswers.Clear();
                OnPropertyChanged("IsMultiple");
            }
        }

        private ObservableCollection<Answer> trueAnswers;
        public ObservableCollection<Answer> TrueAnswers
        {
            get
            {
                return trueAnswers;
            }
            set
            {
                trueAnswers = value;
                OnPropertyChanged("TrueAnswers");
            }
        }

        public Question(string bone)
        {
            Bones = bone;
            Body = Regex.Match(bone, @".*?(?=\r\n)").ToString();
            Answers = new ObservableCollection<Answer>();
            TrueAnswers = new ObservableCollection<Answer>();
            var answerBones = Regex.Matches(bone, @"(?<=#).*?(?=\r\n)");
            foreach (var answer in answerBones)
            {
                Answer ans = new Answer(answer.ToString());
                if (ans.IsRight)
                    TrueAnswers.Add(ans);
                Answers.Add(ans);
            }
            int countTrueAnswer = Answers.Where(a => a.IsRight == true).Count();
            if (countTrueAnswer > 1)
                IsMultiple = true;
        }

        public override string ToString()
        {
            return Body;
        }

        public object Clone()
        {
            return new Question(Bones);
        }

        public bool IsValid()
        {
            if (Body == null || Body == string.Empty)
                return false;

            if (Answers.Count == 0)
                return false;

            if (TrueAnswers.Count == 1 && !IsMultiple)
                return true;

            if (TrueAnswers.Count > 1 && IsMultiple)
                return true;
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
