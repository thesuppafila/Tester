﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestCreator
{
    public class Question: ICloneable
    {
        private string Body;
        private List<Answer> Answers;
        private string bones;

        public Question(string bone)
        {
            bones = bone;
            Body = Regex.Match(bone, @".*?(?=\r\n)").ToString();
            Answers = new List<Answer>();
            var answerBones = Regex.Matches(bone, @"(?<=#).*?(?=\r\n)");
            foreach (var answer in answerBones)
            {
                Answers.Add(new Answer(answer.ToString()));
            }
        }

        public string GetBody()
        {
            return Body;
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

        public List<Answer> GetAnswer()
        {
            return Answers;
        }

        public override string ToString()
        {
            return string.Format("{0}\n{1}\n", Body, string.Join("\n", Answers));
        }

        public string GetTrueAnswer()
        {
            string trueCode = string.Empty;
            foreach (Answer ans in Answers)
                if (ans.IsRight && ans.GetCode() != null)
                    trueCode += ans.GetCode();
            return trueCode;
        }

        public object Clone()
        {
            return new Question(bones);
        }
    }
}
