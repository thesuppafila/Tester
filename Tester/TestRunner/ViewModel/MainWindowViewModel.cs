using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tester.Model;

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

        public ObservableCollection<Test> Tests
        {
            get => Package.Tests;
            set
            {
                Package.Tests = value;
                OnPropertyChanged("Tests");
            }
        }

        public MainWindowViewModel()
        {
            Package = new Package();
            Package.Load();
        }
    }
}
