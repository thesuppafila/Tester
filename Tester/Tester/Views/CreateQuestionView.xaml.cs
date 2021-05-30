using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tester.Model;
using Tester.ViewModel;

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для AddQuestionView.xaml
    /// </summary>
    public partial class CreateQuestionView : Window
    {
        public CreateQuestionViewModel createQuestionViewModel;

        public CreateQuestionView(CreateQuestionViewModel createQuestionViewModel)
        {
            this.createQuestionViewModel = createQuestionViewModel;
            this.DataContext = createQuestionViewModel;
            InitializeComponent();
        }
    }
}
