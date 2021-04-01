using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    [Serializable]
    public class Ticket : INotifyPropertyChanged
    {
        private string body;

        private List<Question> questionsList;

        private int variant;

        public string Body
        {
            get { return body; }
            set
            {
                body = value;
                OnPropertyChanged("Body");
            }
        }

        public List<Question> Questions
        {
            get { return questionsList; }
            set
            {
                questionsList = value;
                OnPropertyChanged("QuestionsList");
            }
        }

        public int Variant
        {
            get { return variant; }
            set
            {
                variant = value;
                OnPropertyChanged("Variant");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public void SaveToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(this.ToString());
            }
        }

        public override string ToString()
        {
            string ticket = string.Empty;
            for (int i = 0; i < questionsList.Count(); i++)
            {
                ticket += string.Format("{0}. {1}\n", (i + 1).ToString(), questionsList[i]);
            }
            return ticket;
        }
    }
}
