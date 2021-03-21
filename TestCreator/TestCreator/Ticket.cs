using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class Ticket
    {
        private List<Question> Questions;

        private List<string> Key = new List<string>();

        public Ticket(List<Question> questions, int count, int startIndex, int endIndex)
        {
            Random random = new Random();
            Questions = new List<Question>();
            List<int> usedQuestions = new List<int>();
            int indexer = 0;
            while (indexer < count)
            {
                int j = random.Next(startIndex, endIndex);
                if (!usedQuestions.Contains(j))
                {
                    usedQuestions.Add(j);

                    Questions.Add(questions[j]);
                    Key.Add(String.Format("{0}:{1}", (indexer + 1).ToString(), questions[j].GetTrueAnswer()));
                    indexer++;
                }
                else continue;
            }
        }

        public List<string> GetAnswerKey()
        {
            if (Key == null)
                throw new ArgumentNullException();
            return Key;
        }

        public List<Question> GetQuestions()
        {
            return Questions;
        }


    }
}
