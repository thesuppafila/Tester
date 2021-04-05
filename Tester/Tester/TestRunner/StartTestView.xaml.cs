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
namespace Tester.TestRunner
{
    /// <summary>
    /// Логика взаимодействия для StartTestView.xaml
    /// </summary>
    public partial class StartTestView : Window
    {
        public Test currentTest;        

        public StartTestView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = currentTest.GetTicket(int.Parse(countTextBox.Text), int.Parse(startIndexTextBox.Text), int.Parse(endIndexTextBox.Text));
            TestView testView = new TestView(groupTextBox.Text, nameTextBox.Text, ticket);
            testView.ShowDialog();
        }
    }
}
