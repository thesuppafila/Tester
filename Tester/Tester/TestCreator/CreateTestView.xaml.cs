using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Tester.Model;
using Tester.ViewModel;

namespace Tester
{
    /// <summary>
    /// Логика взаимодействия для CreateTestView.xaml
    /// </summary>
    public partial class CreateTestView : Window
    {
        public CreateTestView()
        {
            InitializeComponent();
            this.DataContext = new CreateTestViewModel();
        }
    }
}
