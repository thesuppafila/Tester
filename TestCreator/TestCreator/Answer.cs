using System;
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

        public Answer(string body, bool isRight)
        {
            Body = body;
            IsRight = isRight;
        }
    }
}
