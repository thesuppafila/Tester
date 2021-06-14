using System.Windows;
using TestRunner.ViewModel;

namespace Tester.TestRunner
{
    /// <summary>
    /// Логика взаимодействия для TestView.xaml
    /// </summary>
    public partial class TestView : Window
    {
        public TestView(TestViewViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
