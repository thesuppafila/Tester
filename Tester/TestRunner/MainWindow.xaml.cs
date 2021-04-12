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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tester.Model;
using Tester.TestRunner;

namespace TestRunner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Group g1 = new Group();
            Test t1 = new Test();
            //Ticket ticket = currentTest.GetTicket(int.Parse(countTextBox.Text), int.Parse(startIndexTextBox.Text), int.Parse(endIndexTextBox.Text));
            t1.GetTicket();
            //TestView testView = new TestView(groupTextBox.Text, nameTextBox.Text, ticket);
            //testView.ShowDialog();
        }
    }
}
