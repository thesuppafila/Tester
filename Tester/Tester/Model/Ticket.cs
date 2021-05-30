using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tester.Model
{
    [Serializable]
    public class Ticket : NotifyPropertyChanged
    {
        private List<IQuestion> questions;
        public List<IQuestion> Questions
        {
            get => questions;
            set
            {
                questions = value;
                OnPropertyChanged("QuestionsList");
            }
        }

        private int variant;
        public int Variant
        {
            get => variant; 
            set
            {
                variant = value;
                OnPropertyChanged("Variant");
            }
        }

        public Ticket()
        {
            questions = new List<IQuestion>();
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
            for (int i = 0; i < questions.Count(); i++)
            {
                ticket += string.Format("{0}. {1}\n", (i + 1).ToString(), questions[i]);
            }
            return ticket;
        }
    }
}
