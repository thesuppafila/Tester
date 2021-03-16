using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class TicketCreator
    {
        private List<Question> Questions;

        public int Variant;

        private string Key;

        public TicketCreator(List<Question> questions)
        {
            Questions = questions;
        }

        public List<Question> CreateTicket(int countOfQuestion, int startIndex, int endIndex)
        {
            Random random = new Random();
            List<Question> ticket = new List<Question>();

            for (int i = 0; i < countOfQuestion; i++)
            {
                string answersString = string.Empty;
                Question question = Questions[random.Next(startIndex, endIndex)];
                ticket.Add(question);

                foreach (Answer ans in question.Answers)
                    if (ans.IsRight)
                        answersString += ans.code.ToString();
                AddKey(i.ToString(), answersString);
            }
            return ticket;
        }

        public void AddKey(string questionNumber, string answersCode)
        {
            Key += String.Format("{0}:{1}", questionNumber, answersCode);
        }

        public string GetKey()
        {
            return Key;
        }
    }
}
