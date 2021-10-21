using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.Model;
using Tester.ViewModel;

using Package = TicketGenerator.Model.Package;

namespace TicketGenerator.ViewModel
{
    class MainViewModel : NotifyPropertyChanged
    {
        public ExpressionsViewModel ExpViewModel { get; set; }
        public TicketBuilderViewModel TicketViewModel { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        public Package Package { get; set; }

        public RelayCommand ExpViewOpen { get; set; }
        public RelayCommand TicketViewOpen { get; set; }

        public MainViewModel()
        {
            Package = new Package();
            Package.Load();            

            ExpViewModel = new ExpressionsViewModel();
            ExpViewModel.Expressions = Package.Expressions;
            TicketViewModel = new TicketBuilderViewModel();
            TicketViewModel.Expressions = Package.Expressions;
            CurrentView = ExpViewModel;

            ExpViewOpen = new RelayCommand(obj =>
            {
                CurrentView = ExpViewModel;
            });

            TicketViewOpen = new RelayCommand(obj =>
            {
                Package.Expressions = ExpViewModel.Expressions;
                Package.Save();
                TicketViewModel.Expressions = Package.Expressions;
                CurrentView = TicketViewModel;
            });
        }

        public void OnMainViewClosing(object sender, CancelEventArgs e)
        {
            Package.Expressions = ExpViewModel.Expressions;
            Package.Save();
        }
    }
}
