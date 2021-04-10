using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    public class QuestionGroup : IQuestion
    {
        public QuestionGroup()
        {
            qGroup = new ObservableCollection<Question>();
        }

        private string body;
        public string Body
        {
            get
            {
                body = this.ToString();
                return body;
            }
            set
            {
                body = value;
                OnPropertyChanged("Body");
            }
        }

        private ObservableCollection<Question> qGroup;
        public ObservableCollection<Question> QGroup
        {
            get
            {
                return qGroup;
            }
            set
            {
                if (value != null)
                {
                    qGroup = value;
                    Count = qGroup.Count;
                }
                OnPropertyChanged("QuestionGroup");
            }
        }

        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            string output = "[Группа] ";
            foreach (Question q in QGroup)
                output += q.ToString() + "\n";
            return output;
        }

        public object Clone()
        {
            QuestionGroup clone = new QuestionGroup();
            foreach (Question q in QGroup)
                clone.QGroup.Add((Question)q.Clone());
            return clone;
        }

        public bool IsValid()
        {
            return false;
        }
    }
}
