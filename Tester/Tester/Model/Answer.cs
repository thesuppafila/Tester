using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    public class Answer : ICloneable
    {
        public string Body;

        public bool Right;

        public string Code;

        public string Bone;

        public Answer(string bone)
        {
            Bone = bone;
            if (bone[0] == '$')
            {
                Right = true;
                bone = bone.Substring(1);
            }
            Body = bone;
        }

        public void SetRight()
        {
            this.Right = true;
        }

        public void SetUnright()
        {
            this.Right = false;
        }

        public bool IsRight()
        {
            return Right;
        }

        public object Clone()
        {
            return new Answer(this.Bone);
        }

        public override string ToString()
        {
            return "\t" + Code + ". " + Body;
        }
    }
}
