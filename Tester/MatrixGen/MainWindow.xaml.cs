using MatrixGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatrixGen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<int, TaskType> taskTypes = new Dictionary<int, TaskType>();

        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            //int i = 0;
            //foreach(TaskType type in Enum.GetValues(typeof(TaskType)))
            //{
            //    taskTypes.Add(i++, type);
            //}
            Generator generator = new Generator();
            generator.Generate(0);
            taskTextBlock.Text = generator.task.ToString();
        }
    }
}
