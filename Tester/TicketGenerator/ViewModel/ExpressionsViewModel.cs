using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tester.Model;
using Tester.ViewModel;

namespace TicketGenerator.ViewModel
{
    public class ExpressionsViewModel : NotifyPropertyChanged
    {        
        private ObservableCollection<Expression> _expressions;
        public ObservableCollection<Expression> Expressions
        {
            get => _expressions;
            set
            {
                _expressions = value;
                OnPropertyChanged("Expressions");
            }
        }

        private string _curPattern;
        public string CurPattern
        {
            get => _curPattern;
            set
            {
                _curPattern = value;
                OnPropertyChanged("CurPattern");
            }
        }

        public RelayCommand AddExp { get; set; }
        public RelayCommand DeleteExp { get; set; }

        public ExpressionsViewModel()
        {
            CurPattern = "a + a + a";
            Expressions = new ObservableCollection<Expression>();

            AddExp = new RelayCommand(obj => {
                try
                {
                    Expressions.Add(new Expression(CurPattern));
                }
                catch
                {
                    MessageBox.Show("Ошибка создания выражения.");
                }
            });

            DeleteExp = new RelayCommand(obj => {
                Expressions.Remove((Expression)obj);
            });
        }
    }
}
