using System.Collections.ObjectModel;
using System.Windows;
using Tester.Model;

namespace Tester.ViewModel
{
    public class CreateGroupViewModel : NotifyPropertyChanged
    {
        private bool? dialogResult;
        public bool? DialogResult
        {
            get => dialogResult;
            set
            {
                dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }

        private Group currentGroup;
        public Group CurrentGroup
        {
            get => currentGroup;
            set
            {
                currentGroup = value;
                OnPropertyChanged("CurrentGroup");
            }
        }

        private Student selectedStudent;
        public Student SelectedStudent
        {
            get => selectedStudent;
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
            get => studentName;
            set
            {
                studentName = value;
                OnPropertyChanged("StudentName");
            }
        }

        public string Id
        {
            get => CurrentGroup.Id;
            set
            {
                CurrentGroup.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public ObservableCollection<Student> Students
        {
            get => CurrentGroup.Students;
            set
            {
                CurrentGroup.Students = value;
                OnPropertyChanged("Students");
            }
        }

        public CreateGroupViewModel(Group currentGroup = null)
        {
            CurrentGroup = currentGroup??new Group();
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
