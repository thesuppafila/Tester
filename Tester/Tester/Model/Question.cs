using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Tester.Model
{
    [Serializable]
    public class Question : NotifyPropertyChanged, ICloneable, IQuestion
    {
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

        public Question()
        {
            Answers = new ObservableCollection<Answer>();
            TrueAnswers = new ObservableCollection<Answer>();
        }

        public Question(Question question) : this()
        {
            Body = new string(question.Body.ToCharArray());
            foreach (Answer answer in question.Answers)
            {
                Answers.Add(new Answer(answer));
            }

            foreach (Answer answer in Answers)
            {
                if (answer.IsRight)
                {
                    TrueAnswers.Add(answer);
                }
            }
        }

        public Question(string bones) : this()
        {
            Bones = bones;
            Body = Regex.Match(bones, @".*?(?=\r\n)").ToString();
            var answerBones = Regex.Matches(bones, @"(?<=#).*?(?=\r\n)");
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

        public object Clone()
        {
            return new Question(this);
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

        public override string ToString()
        {
            return Body;
        }
    }
}
