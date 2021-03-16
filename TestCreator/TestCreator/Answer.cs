using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    class Answer
    {
        public string Body;
        public bool IsRight;
        public string code;

        public Answer(string body, bool isRight)
        {
            Body = body;
            IsRight = isRight;
            GenerateCode();
        }

        private void GenerateCode()
        {
            Random random = new Random();
            code = random.Next(100).ToString();
        }
    }
}
