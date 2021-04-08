using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.TestCreator;
using Tester.Model;

namespace Tester.ViewModel
{
    public class CreateGroupViewModel : BaseViewModel
    {
        CreateStudentViewModel createStudentViewModel = new CreateStudentViewModel();

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

        private ObservableCollection<Student> students;
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

        private Student selectedStudents;
        public Student SelectedStudents
        {
            get
            {
                return selectedStudents;
            }
            set
            {
                selectedStudents = value;
                OnPropertyChanged("Students");
            }
        }


        private RelayCommand addNewStudentCommand;
        public RelayCommand AddNewStudentCommmand
        {
            get
            {
                return addNewStudentCommand ??
                    (addNewStudentCommand = new RelayCommand(obj =>
                    {
                        createStudentViewModel = new CreateStudentViewModel();
                        CreateStudentView createStudentView = new CreateStudentView(createStudentViewModel);
                        if (createStudentView.ShowDialog() == true)
                        {
                            Students.Add(createStudentViewModel.CurrentStudent);
                        }

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
                        DialogResult = true;
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
