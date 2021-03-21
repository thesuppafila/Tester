using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    public class Answer
    {
        private string Body;
        public bool IsRight;
        private string Code;

        public Answer(string bone)
        {
            if (bone[0] == '$')
            {
                IsRight = true;
                bone = bone.Substring(1);
            }
            Body = bone;
        }

        public override string ToString()
        {
            return "\t" + Code + ". " + Body;
        }

        public string GetCode()
        {
            return Code;
        }

        public string GetBody()
        {
            return Body;
        }

        public void SetCode(string code)
        {
            Code = code;
        }
    }
}
