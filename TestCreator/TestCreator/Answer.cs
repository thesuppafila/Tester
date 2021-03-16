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
        public string Body;
        public bool IsRight;
        public string Code;
        static Random random = new Random();


        public Answer(string bone)
        {
            random = new Random();
            if (bone[0] == '$')
            {
                IsRight = true;
                bone = bone.Substring(1);
            }
            Body = bone;
            GenerateCode();
        }


        private void GenerateCode()
        {
            Code = random.Next(100).ToString();
        }

        public override string ToString()
        {
            return Body;
        }
    }
}
