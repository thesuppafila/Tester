using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    public class Answer
    {
        public string body;
        public bool isRight;

        public Answer(string bone)
        {
            if (bone[0] == '$')
            {
                isRight = true;
                bone = bone.Substring(1);
            }
            body = bone;
        }
    }
}
