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

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для SelectModeView.xaml
    /// </summary>
    public partial class SelectModeView : Window
    {
        public SelectModeView()
        {
            InitializeComponent();
        }

        private void testModeButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void createTestModeButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
