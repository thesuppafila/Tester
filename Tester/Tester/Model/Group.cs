using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Tester.Model
{
    [Serializable]
    public class Group : NotifyPropertyChanged
    {
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = new ObservableCollection<Student>(value.OrderBy(i => i));
                OnPropertyChanged("Students");
            }
        }

        public Group()
        {
            Students = new ObservableCollection<Student>();
        }

        public Group(string id) : this()
        {
            Id = id;
        }

        public Group(Group group) : this()
        {
            Id = new string(group.Id.ToCharArray());
            foreach (Student student in group.Students)
            {
                Students.Add(new Student(student.Name));
            }
        }

        public void LoadFromFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                if (fileInfo.Extension.ToLower() == ".txt")
                {
                    var students = File.ReadAllLines(openFileDialog.FileName).Select(x => new Student(x)).ToList();
                    var names = Regex.Matches(File.ReadAllText(openFileDialog.FileName), @"(\d+) ([А-я ]*) (\d)", RegexOptions.Singleline);
                    foreach (var s in students)
                        Students.Add(new Student(s.ToString()));
                }
                if (fileInfo.Extension.ToLower() == ".xml")
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Test));

                    using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
                    {
                        Group group = (Group)formatter.Deserialize(fs);
                        this.Id = group.Id;
                        foreach (Student s in group.Students)
                            Students.Add(s);
                    }
                }
            }
        }

        public bool IsValid()
        {
            if (Id == null)
                return false;
            if (Students.Count < 1)
                return false;
            return true;
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
