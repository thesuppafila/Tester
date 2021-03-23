using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tester.Model
{
    class Question: ICloneable
    {
        public string Body;

        public List<Answer> Answers;

        public string Bones;

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
        }

        public string GetTrueAnswer()
        {
            string trueCode = string.Empty;
            foreach (Answer ans in Answers)
                if (ans.IsRight && ans.Code != null)
                    trueCode += ans.Code;
            return trueCode;
        }

        public override string ToString()
        {
            return string.Format("{0}\n{1}\n", Body, string.Join("\n", Answers));
        }

        public object Clone()
        {
            return new Question(Bones);
        }
    }
}
