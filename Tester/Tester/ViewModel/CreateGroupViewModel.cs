using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tester.TestCreator;
using Tester.Model;


namespace Tester.ViewModel
{
    public class CreateGroupViewModel : BaseViewModel
    {

        public CreateGroupViewModel()
        {
            CurrentGroup = new Group();
        }

        public CreateGroupViewModel(Model.Group currentGroup)
        {
            CurrentGroup = currentGroup;
        }

        private bool? dialogResult;
        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }

        public string Id
        {
            get
            {
                return CurrentGroup.Id;
            }
            set
            {
                CurrentGroup.Id = value;
                OnPropertyChanged("Id");
            }
        }

        private Model.Group currentGroup;
        public Model.Group CurrentGroup
        {
            get
            {
                return currentGroup;
            }
            set
            {
                currentGroup = value;
                OnPropertyChanged("CurrentGroup");
            }
        }

        public ObservableCollection<Student> Students
        {
            get
            {
                return CurrentGroup.Students;
            }
            set
            {
                CurrentGroup.Students = value;
                OnPropertyChanged("Students");
            }
        }

        private Student selectedStudent;
        public Student SelectedStudent
        {
            get
            {
                return selectedStudent;
            }
            set
            {
                selectedStudent = value;
                if (SelectedStudent != null)
                    StudentName = selectedStudent.Name;
                else StudentName = string.Empty;
                OnPropertyChanged("SelectedStudent");
            }
        }

        private string studentName;
        public string StudentName
        {
            get
            {
                return studentName;
            }
            set
            {
                studentName = value;
                OnPropertyChanged("StudentName");
            }
        }

        private RelayCommand addNewStudentCommand;
        public RelayCommand AddNewStudentCommand
        {
            get
            {
                return addNewStudentCommand ??
                    (addNewStudentCommand = new RelayCommand(obj =>
                    {
                        if (StudentName != null && StudentName != string.Empty)
                            Students.Add(new Student(StudentName));
                    }));
            }
        }

        private RelayCommand loadStudentsFromFileCommand;
        public RelayCommand LoadStudentsFromFileCommand
        {
            get
            {
                return loadStudentsFromFileCommand ??
                    (loadStudentsFromFileCommand = new RelayCommand(obj =>
                    {
                        CurrentGroup.LoadFromFile();
                        Students = CurrentGroup.Students;
                    }));
            }
        }

        [field: NonSerialized]
        private RelayCommand editStudentCommand;
        public RelayCommand EditStudentCommand
        {
            get
            {
                return editStudentCommand ??
                    (editStudentCommand = new RelayCommand(obj =>
                    {
                        if (SelectedStudent != null)
                        {
                            Students.Add(new Student(StudentName));
                            Students.Remove(SelectedStudent);
                        }
                    }));
            }
        }
        
        [field: NonSerialized]
        private RelayCommand removeStudentCommand;
        public RelayCommand RemoveStudentCommand
        {
            get
            {
                return removeStudentCommand ??
                    (removeStudentCommand = new RelayCommand(obj =>
                    {
                        Students.Remove((Student)obj);
                    }));
            }
        }

        private RelayCommand okCommand;
        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ??
                    (okCommand = new RelayCommand(obj =>
                    {
                        if (CurrentGroup.IsValid())
                            DialogResult = true;
                        else
                        {
                            MessageBox.Show("Группа некорректна.");
                        }
                    }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                    (cancelCommand = new RelayCommand(obj =>
                    {
                        DialogResult = false;
                    }));
            }
        }
    }
}
