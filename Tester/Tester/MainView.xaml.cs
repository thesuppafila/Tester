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

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {

            SelectModeView selectModeView = new SelectModeView();
            if (selectModeView.ShowDialog() == true)
            {
                HelloView helloView = new HelloView();
                if (helloView.ShowDialog() == true)
                {
                    InitializeComponent();
                    this.MinWidth = SystemParameters.WorkArea.Width;
                    this.MinHeight = SystemParameters.WorkArea.Height;
                    this.MaxHeight = this.MinWidth;
                    this.MaxWidth = this.MinWidth;

                    WindowStyle = WindowStyle.None;
                    ResizeMode = ResizeMode.NoResize;
                }
                else Close();
            }
            else
            {                
                CreateTestView createTestView = new CreateTestView();
                createTestView.ShowDialog();
            }
        }

        private void nextQuestionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void beforeQuestionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void endTestButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
