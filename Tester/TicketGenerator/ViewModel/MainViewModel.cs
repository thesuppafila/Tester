using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.Model;
using Tester.ViewModel;

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

        public RelayCommand ExpViewOpen { get; set; }
        public RelayCommand TicketViewOpen { get; set; }

        public MainViewModel()
        {
            ExpViewModel = new ExpressionsViewModel();
            TicketViewModel = new TicketBuilderViewModel();
            CurrentView = ExpViewModel;

            ExpViewOpen = new RelayCommand(obj => {
                CurrentView = ExpViewModel;
            });

            TicketViewOpen = new RelayCommand(obj => {
                CurrentView = TicketViewModel;
            });
        }
    }
}
