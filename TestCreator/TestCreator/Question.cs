using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestCreator
{
    public class Question
    {
        private string Body;
        public List<Answer> Answers;

        public Question(string bone)
        {
            Body = Regex.Match(bone, @".*?(?=\r\n)").ToString();
            Answers = new List<Answer>();
            var answerBones = Regex.Matches(bone, @"(?<=#).*?(?=\r\n)");
            foreach (var answer in answerBones)
            {
                Answers.Add(new Answer(answer.ToString()));
            }
        }

        public void SetBudy(string body)
        {
            if (body == "")
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
