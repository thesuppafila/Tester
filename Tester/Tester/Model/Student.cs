using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    [Serializable]
    public class Student
    {
        public string Name;

        public int Score;

        public string GroupId;

        public int Id;

        public Student()
        {

        }

        public Student(string name) {
            Name = name;
        }

        public Student(string name, int score, string groupId, int id)
        {
            Name = name;
            Score = score;
            GroupId = groupId;
            Id = id;
        }

        public void setScore(int score)
        {
            Score = score;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
