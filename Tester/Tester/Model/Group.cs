using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tester.Model
{
    [Serializable]
    public class Group : INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get
            {
                return students;
            }
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }

        public Group()
        {
            Students = new ObservableCollection<Student>();
        }

        public Group(string id)
        {
            Students = new ObservableCollection<Student>();
            Id = id;
        }

        public override string ToString()
        {
            return this.Id;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void LoadFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
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
                        Model.Group group = (Model.Group)formatter.Deserialize(fs);
                        this.Id = group.Id;
                        foreach (Student s in group.Students)
                            Students.Add(s);
                    }
                }
            }
        }
    }
}
