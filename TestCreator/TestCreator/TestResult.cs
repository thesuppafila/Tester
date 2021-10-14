using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCreator
{
    public class TestResult
    {
        public string Group { get; set; }
        public string Name { get; set; }
        public int Result { get; set; }
        public string Variant { get; set; }
        Dictionary<string, string> Key;
        Dictionary<string, string> Answers;

        public TestResult()
        {
            Key = new Dictionary<string, string>();
            Answers = new Dictionary<string, string>();
        }

        public bool TryParse(string bones)
        {
            return false;
        }

        public bool TryParseFile(string path)
        {
            using (StreamReader reader = new StreamReader(path, Encoding.ASCII))
            {
                Group = reader.ReadLine();
                Name = reader.ReadLine();
                Variant = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var answer = reader.ReadLine();
                    Answers.Add(answer.Split(':')[0], answer.Split(':')[1]);
                }
                GetKey();
                GetResult();
            }
            return true;
        }

        public void GetResult()
        {
            foreach (var v in Answers)
            {
                string value;
                if (Key.TryGetValue(v.Key, out value))
                {
                    if (value == v.Value)
                        Result++;
                }
            }
        }

        public void GetKey()
        {
            using (StreamReader reader = new StreamReader(Group + "\\Keys\\" + Variant + ".txt", Encoding.Default))
            {
                while (!reader.EndOfStream)
                {
                    var answer = reader.ReadLine();
                    Key.Add(answer.Split(':')[0], answer.Split(':')[1]);
                }
            }
        }
    }
}
