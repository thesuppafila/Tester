using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class Question
    {
        private string Body;
        private List<Answer> Answers;

        public void SetBudy(string body)
        {
            if (body != "")
                throw new ArgumentNullException();
            Body = body;
        }

        public void SetAnswers(List<Answer> answers)
        {
            if (answers == null)
                throw new ArgumentNullException();
            Answers = answers;
        }
    }
}
