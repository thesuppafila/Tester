using System;

namespace Tester.Model
{
    [Serializable]
    public class Student: NotifyPropertyChanged, IComparable<Student>
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public Student()
        {

        }

        public Student(string name)
        {
            Name = name;
        }

        public int CompareTo(Student other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
