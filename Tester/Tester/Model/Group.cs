using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    public class Group
    {
        public string Id;

        public List<Student> Students;

        public Group()
        {
            Students = new List<Student>();
        }

        public Group(string id)
        {
            Students = new List<Student>();
            Id = id;
        }

        public void SetId(string id)
        {
            Id = id;
        }

        public void AddStudent(Student student)
        {
            if (!Students.Contains(student))
                Students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            if (Students.Contains(student))
                Students.Remove(Students.Where(s => s.Equals(student)).Single());
        }

        public List<Student> GetStudents()
        {
            return Students;
        }

        public override string ToString()
        {
            return this.Id;
        }
    }
}
