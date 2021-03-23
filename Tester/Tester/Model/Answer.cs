using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    class Answer
    {
        public string Body;

        public bool IsRight;

        public string Code;

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
    }
}
