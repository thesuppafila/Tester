﻿using System;
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

namespace Tester.Views
{
    /// <summary>
    /// Логика взаимодействия для ExceptionView.xaml
    /// </summary>
    public partial class ExceptionView : Window
    {
        public ExceptionView()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
