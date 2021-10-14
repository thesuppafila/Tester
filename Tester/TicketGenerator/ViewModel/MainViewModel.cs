using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.Model;

namespace TicketGenerator.ViewModel
{
    class MainViewModel : NotifyPropertyChanged
    {
        public ExpressionsViewModel ExpViewModel;

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

        public MainViewModel()
        {
            ExpViewModel = new ExpressionsViewModel();
            CurrentView = ExpViewModel;
        }
    }
}
