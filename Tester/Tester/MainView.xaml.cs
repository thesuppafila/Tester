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
            HelloView helloView = new HelloView();
            if (helloView.ShowDialog() == true)
            {
                InitializeComponent();
                this.MinWidth = System.Windows.SystemParameters.WorkArea.Width;
                this.MinHeight = System.Windows.SystemParameters.WorkArea.Height;
                this.MaxHeight = this.MinWidth;
                this.MaxWidth = this.MinWidth;

                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.NoResize;
            }
            else Close();
        }
    }
}
