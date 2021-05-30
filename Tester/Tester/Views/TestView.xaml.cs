using System.Windows;
using Tester.ViewModel;

namespace Tester.Views
{
    /// <summary>
    /// Логика взаимодействия для TestView.xaml
    /// </summary>
    /// 

    public partial class TestView : Window
    {
        public TestView()
        {
            InitializeComponent();
            var vm = (TestViewModel)this.DataContext;
            Closing += vm.OnMainViewClosing;
        }
    }
}