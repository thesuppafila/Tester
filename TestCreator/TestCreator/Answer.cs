using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    public class Answer
    {
        public string Body;
        public bool IsRight;

        public Answer(string bone)
        {
            if (bone[0] == '$')
            {
                IsRight = true;
                bone = bone.Substring(1);
            }
            Body = bone;
        }

    }
}
