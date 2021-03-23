using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    class Ticket
    {
        public string Body;

        public List<Question> Questions;

        public List<string> Key;

        public int Variant;


        public void SaveToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(this.ToString());
            }
        }

        public override string ToString()
        {
            string ticket = string.Empty;
            for (int i = 0; i < Questions.Count(); i++)
            {
                ticket += string.Format("{0}. {1}\n", (i + 1).ToString(), Questions[i]);
            }
            return ticket;
        }
    }
}
