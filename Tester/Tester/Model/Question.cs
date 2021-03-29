using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tester.Model
{
    [Serializable]
    public class Question: ICloneable
    {
        public string Body;

        public List<Answer> Answers;

        public string Bones;

        public bool IsMultiple;

        public Question()
        {
            Answers = new List<Answer>();
        }

        public Question(string bone)
        {
            Bones = bone;
            Body = Regex.Match(bone, @".*?(?=\r\n)").ToString();
            Answers = new List<Answer>();
            var answerBones = Regex.Matches(bone, @"(?<=#).*?(?=\r\n)");
            foreach (var answer in answerBones)
            {
                Answers.Add(new Answer(answer.ToString()));
            }
            int countTrueAnswer = Answers.Where(a => a.Right == true).Count();
            if (countTrueAnswer > 1)
                IsMultiple = true;
        }

        public void AddAnswer(Answer answer)
        {
            if (!Answers.Contains(answer))
                Answers.Add(answer);
        }

        public List<Answer> GetAnswers()
        {
            return Answers;
        }

        //public string GetTrueAnswer()
        //{
        //    string trueCode = string.Empty;
        //    foreach (Answer ans in Answers)
        //        if (ans.Right && ans.Code != null)
        //            trueCode += ans.Code;
        //    return trueCode;
        //}

        public override string ToString()
        {
            return Body;
        }

        internal void SetBody(string body)
        {
            if (body == null)
                throw new ArgumentNullException();
            this.Body = body;
        }

        public object Clone()
        {
            return new Question(Bones);
        }
    }
}
