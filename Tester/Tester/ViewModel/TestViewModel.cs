using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Tester.Model;
using Tester.Views;

namespace Tester.ViewModel
{
    class TestViewModel : NotifyPropertyChanged
    {
        public Package Package { get; set; }

        private Test selectedTest;
        public Test SelectedTest
        {
            get => selectedTest;
            set
            {
                selectedTest = value;
                OnPropertyChanged("SelectedTest");
            }
        }

        private Group selectedGroup;
        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                OnPropertyChanged("SelectedGroup");
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

        public ObservableCollection<Group> Groups
        {
            get => Package.Groups;
            set
            {
                Package.Groups = value;
                OnPropertyChanged("Groups");
            }
        }

        public TestViewModel()
        {
            Package = new Package();
            Package.Load();
        }

        private RelayCommand addNewTestCommand;
        public RelayCommand AddNewTestCommand
        {
            get
            {
                return addNewTestCommand ??
                    (addNewTestCommand = new RelayCommand(obj =>
                    {
                        var createTestViewModel = new CreateTestViewModel();
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
                        if (MessageBox.Show("Удалить тест?", "Удаление...", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                        var createTestViewModel = new CreateTestViewModel { CurrentTest = (Test)obj };
                        CreateTestView createTestView = new CreateTestView(createTestViewModel);
                        if (createTestView.ShowDialog() == true)
                        {
                            SelectedTest = createTestViewModel.CurrentTest;
                        }
                    }));
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
                        var createGroupViewModel = new CreateGroupViewModel();
                        CreateGroupView createGroupView = new CreateGroupView(createGroupViewModel);
                        if (createGroupView.ShowDialog() == true)
                            Groups.Add(createGroupViewModel.CurrentGroup);
                    }));
            }
        }

        private RelayCommand removeGroupCommand;
        public RelayCommand RemoveGroupCommand
        {
            get
            {
                return removeGroupCommand ??
                    (removeGroupCommand = new RelayCommand(obj =>
                    {
                        if (MessageBox.Show("Удалить группу?", "Удаление...", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            Groups.Remove((Group)obj);
                    }));
            }
        }

        private RelayCommand editGroupCommand;
        public RelayCommand EditGroupCommand
        {
            get
            {
                return editGroupCommand ??
                    (editGroupCommand = new RelayCommand(obj =>
                    {
                        var createGroupViewModel = new CreateGroupViewModel { CurrentGroup = (Group)obj };
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
                        var v = new DemoView();
                        v.Show();
                    }));
            }
        }

        public void OnMainViewClosing(object sender, CancelEventArgs e)
        {
            Package.Tests = Tests;
            Package.Groups = Groups;
            Package.Save();
        }
    }
}
