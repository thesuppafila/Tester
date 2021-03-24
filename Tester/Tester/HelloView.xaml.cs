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

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для HelloView.xaml
    /// </summary>
    public partial class HelloView : Window
    {
        public HelloView()
        {
            InitializeComponent();
            var _enumval = Enum.GetValues(typeof(TestType)).Cast<TestType>();
            testTypeComboBox.ItemsSource = _enumval.ToList();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            Controller controller = new Controller();
            this.DialogResult = true;
            Close();
        }
    }
}
