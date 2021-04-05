using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tester.Model
{
    public class QuestionGroup
    {
        private ObservableCollection<Question> qGroup;
        public ObservableCollection<Question> QGroup
        {
            get
            {
                return qGroup;
            }
            set
            {
                if (value != null)
                {
                    qGroup = value;
                    Count = qGroup.Count;
                }
                OnPropertyChanged("QuestionGroup");
            }
        }

        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
