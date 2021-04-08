using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.Model;


namespace Tester.ViewModel
{
    class TestViewModel : BaseViewModel
    {
        private ObservableCollection<Test> tests;
        public ObservableCollection<Test> Tests
        {
            get
            {
                return tests;
            }
            set
            {
                tests = value;
                OnPropertyChanged("Tests");
            }
        }

        private Test selectedTest;
        public Test SelectedTest
        {
            get
            {
                return selectedTest;
            }
            set
            {
                selectedTest = value;
                OnPropertyChanged("SelectedTest");
            }
        }

        private RelayCommand addNewTestCommand;
        public RelayCommand AddNewTestCommand
        {
            get
            {
                return addNewTestCommand ??
                    (addNewTestCommand = new RelayCommand(obj =>
                    {
                        CreateTestView createTestView = new CreateTestView();
                        createTestView.ShowDialog();
                    }));
            }
        }

        private RelayCommand removeTestCommand;
        public RelayCommand RemoveTestCommand
        {
            get
            {
                return removeTestCommand ??
                    (removeTestCommand = new RelayCommand(obj =>
                    {
                        if (SelectedTest != null)
                            Tests.Remove(SelectedTest);
                    }));
            }
        }

        private RelayCommand editTestCommand;
        public RelayCommand EditTestCommand
        {
            get
            {
                return editTestCommand ??
                    (editTestCommand = new RelayCommand(obj =>
                    {
                        if (SelectedTest != null)
                        {
                            CreateTestView createTestView = new CreateTestView { createTestViewModel = new CreateTestViewModel { CurrentTest = SelectedTest } };
                            if(createTestView.ShowDialog() == true)
                            {
                                SelectedTest = createTestView.createTestViewModel.CurrentTest;
                            }
                        }
                    }));
            }
        }

        public TestViewModel()
        {
            Tests = new ObservableCollection<Test>();
        }
    }
}
