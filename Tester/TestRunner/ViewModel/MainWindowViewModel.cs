using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tester.Model;
using Tester.TestRunner;
using Tester.ViewModel;

namespace TestRunner.ViewModel
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        public Package Package { get; set; }

        public ObservableCollection<Group> Groups
        {
            get => Package.Groups;
            set
            {
                Package.Groups = value;
                OnPropertyChanged("Groups");
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

        private Student currentStudent;
        public Student CurrentStudent
        {
            get => currentStudent;
            set
            {
                currentStudent = value;
                OnPropertyChanged("CurrentStudent");
            }
        }

        public ObservableCollection<Test> Tests
        {
            get => Package.Tests;
            set
            {
                Package.Tests = value;
                OnPropertyChanged("Tests");
            }
        }

        private Test currentTest;
        public Test CurrentTest
        {
            get => currentTest;
            set
            {
                currentTest = value;
                OnPropertyChanged("CurrentTest");
            }
        }

        public MainWindowViewModel()
        {
            Package = new Package();
            Package.Load();
        }

        public RelayCommand StartTest
        {
            get
            {
                return new RelayCommand(obj =>
                    {
                        try
                        {
                            TestView tView = new TestView(CurrentGroup.Id, CurrentStudent.Name, CurrentTest.GetTicket()); //
                            tView.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    });
            }
        }
    }
}
