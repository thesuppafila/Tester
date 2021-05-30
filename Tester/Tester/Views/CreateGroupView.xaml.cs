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
using Tester.ViewModel;

namespace Tester.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateGroupView.xaml
    /// </summary>
    public partial class CreateGroupView : Window
    {
        public CreateGroupView(CreateGroupViewModel createGroupViewModel)
        {
            InitializeComponent();
            DataContext = createGroupViewModel;
        }
    }
}
