using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    public class Test
    {
        public List<Question> questions;

        public Test()
        {
            questions = new List<Question>();
        }

        public Test(List<Question> questions)
        {
            this.questions = (questions.Select(q => (Question)q.Clone()).ToList());
        }

        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            if (questions.Contains(question))
                questions.Remove(question);
        }

        public List<Question> GetQuestions()
        {
            return questions;
        }
    }
}
