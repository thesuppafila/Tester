using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class Ticket
    {
        private List<Question> Questions;

        private List<string> Key;

        public void SetQuestions(List<Question> questions) 
        {
            if (questions == null)
                throw new ArgumentNullException();
            Questions = questions;
        }

        public void SetKey(List<string> key)
        {
            Key = key;
        }

        public List<Question> GetQuestions()
        {
            return Questions;
        }

        public List<string> GetAnswerKey()
        {
            if (Key == null)
                throw new ArgumentNullException();
            return Key;
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
            for (int i = 0; i < Questions.Count(); i++)
            {
                ticket += string.Format("{0}. {1}\n", (i + 1).ToString(), Questions[i]);
            }
            return ticket;
        }
    }
}
