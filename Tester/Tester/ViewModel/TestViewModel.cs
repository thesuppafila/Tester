using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tester.Model;
using Tester.TestCreator;

namespace Tester.ViewModel
{
    class TestViewModel : BaseViewModel
    {
        CreateTestViewModel createTestViewModel = new CreateTestViewModel();
        CreateGroupViewModel createGroupViewModel = new CreateGroupViewModel();

        public TestViewModel()
        {
            Package = new Package();
            Package.Load();
        }

        private Package package;
        public Package Package
        {
            get
            {
                return package;
            }
            set
            {
                package = value;
            }
        }

        public ObservableCollection<Test> Tests
        {
            get
            {
                return Package.Tests;
            }
            set
            {
                Package.Tests = value;
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
                        createTestViewModel = new CreateTestViewModel();
                        CreateTestView createTestView = new CreateTestView(createTestViewModel);
                        if (createTestView.ShowDialog() == true)
                        {
                            Tests.Add(createTestViewModel.CurrentTest);
                        }
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
                        Tests.Remove((Test)obj);
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
                        createTestViewModel = new CreateTestViewModel { CurrentTest = (Test)obj };
                        CreateTestView createTestView = new CreateTestView(createTestViewModel);
                        if (createTestView.ShowDialog() == true)
                        {
                            SelectedTest = createTestViewModel.CurrentTest;
                        }
                    }));
            }
        }

        public ObservableCollection<Model.Group> Groups
        {
            get
            {
                return Package.Groups;
            }
            set
            {
                Package.Groups = value;
                OnPropertyChanged("Groups");
            }
        }

        private Model.Group selectedGroup;
        public Model.Group SelectedGroup
        {
            get
            {
                return selectedGroup;
            }
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
            }
        }

        private RelayCommand addNewGroupCommand;
        public RelayCommand AddNewGroupCommand
        {
            get
            {
                return addNewGroupCommand ??
                    (addNewGroupCommand = new RelayCommand(obj =>
                    {
                        createGroupViewModel = new CreateGroupViewModel();
                        CreateGroupView createGroupView = new CreateGroupView(createGroupViewModel);
                        if (createGroupView.ShowDialog() == true)
                            Groups.Add(createGroupViewModel.CurrentGroup);
                    }));
            }
        }


        [field: NonSerialized]
        private RelayCommand removeGroupCommand;
        public RelayCommand RemoveGroupCommand
        {
            get
            {
                return removeGroupCommand ??
                    (removeGroupCommand = new RelayCommand(obj =>
                    {
                        Groups.Remove((Group)obj);
                    }));
            }
        }

        [field: NonSerialized]
        private RelayCommand editGroupCommand;
        public RelayCommand EditGroupCommand
        {
            get
            {
                return editGroupCommand ??
                    (editGroupCommand = new RelayCommand(obj =>
                    {
                        createGroupViewModel = new CreateGroupViewModel { CurrentGroup = (Group)obj };
                        CreateGroupView createGroupView = new CreateGroupView(createGroupViewModel);
                        if (createGroupView.ShowDialog() == true)
                            SelectedGroup = createGroupViewModel.CurrentGroup;
                    }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        Package.Tests = Tests;
                        Package.Groups = Groups;
                        Package.Save();
                        MessageBox.Show("Изменения успешно сохранены.");
                    }));
            }
        }

        public async void OnMainViewClosing(object sender, CancelEventArgs e)
        {
            Package.Tests = Tests;
            Package.Groups = Groups;
            Package.Save();
        }
    }
}
