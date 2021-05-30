using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        Package package;

        public MainWindow()
        {
            InitializeComponent();
            package = new Package();
            if (File.Exists("config.dat"))
            {
                package.Load();
            }
            else
            {
                //var dialog = new OpenFileDialog() { InitialDirectory = Directory.GetCurrentDirectory() };
                //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    package.Load();
            }

            try
            {
                foreach (var g in package.Groups)
                    groupComboBox.Items.Add(g.Id);
                foreach (var t in package.Tests)
                    testTypeComboBox.Items.Add(t.Name);
            }
            catch
            {
                System.Windows.MessageBox.Show("Не удалось загрузить конфигурационный файл.");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Test test = package.Tests.Where(t => t.Name == testTypeComboBox.SelectedValue.ToString()).Single();
                TestView tView = new TestView(groupComboBox.SelectedValue.ToString(), nameComboBox.SelectedValue.ToString(), test.GetTicket()); //
                tView.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var students = package.Groups.Where(g => g.Id == groupComboBox.SelectedValue.ToString()).Single().Students;
            nameComboBox.Items.Clear();
            foreach (var s in students)
                nameComboBox.Items.Add(s.Name);
        }

        private void LastResultClicked(object sender, RoutedEventArgs e)
        {
            if (File.Exists("results.txt"))
            {
                System.Windows.Forms.MessageBox.Show(File.ReadAllText("results.txt", Encoding.Default));
            }
        }
    }
}
