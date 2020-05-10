using System;
using System.Collections.Generic;
using System.Data;
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

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public delegate double FuncDelegate(double x);
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register("MinX", typeof(double), typeof(Window));
        public static readonly DependencyProperty MaxXProperty = DependencyProperty.Register("MaxX", typeof(double), typeof(Window));
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(Window));
        public static readonly DependencyProperty CurrentXProperty = DependencyProperty.Register("CurrentX", typeof(double), typeof(Window));
        public static readonly DependencyProperty CurrentStepProperty = DependencyProperty.Register("CurrentStep", typeof(double), typeof(Window));



        public int Count {
            get { return (int)this.GetValue(CountProperty); }
            set { this.SetValue(CountProperty, value); }
        }
        public double MinX {
            get { return (double)this.GetValue(MinXProperty); }
            set { this.SetValue(MinXProperty, value); }
        }
        public double MaxX {
            get { return (double)this.GetValue(MaxXProperty); }
            set { this.SetValue(MaxXProperty, value); }
        }

        public double CurrentX {
            get { return (double)this.GetValue(CurrentXProperty); }
            set { this.SetValue(CurrentXProperty, value); }
        }

        public double CurrentStep {
            get { return (double)this.GetValue(CurrentStepProperty); }
            set { this.SetValue(CurrentStepProperty, value); }
        }

        private DataTable dataTable;

        public DataTable Datatable {
            get {
                if (this.dataTable == null)
                    this.dataTable = this.CreateDataTable();
                return this.dataTable;
            }
        }


        FuncDelegate func;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            MinX = -5;
            MaxX = 5;
            Count = 10;
            switch (comboBox.SelectedIndex)
            {
                case 0: functionLabel.Content = "Функция: F(x) = x^2 / sqrt(x^2 + 2)"; func = new FuncDelegate(f23); break;
                case 1: functionLabel.Content = "Функция: F(x) = sqrt(x^2 + 1) / (2*x + 2.5)"; func = new FuncDelegate(f25); break;
                case 2: functionLabel.Content = "Функция: F(x) = 1 / (1 + x)"; func = new FuncDelegate(ftest); break;
            }
            CurrentStep = 0.001;
            CurrentX = 1;
        }

        private void GetValuesButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeftRectCheckBox.IsChecked == true)
                LeftRectListBoxItem.Content = "Левые прямоугольники, результат: " + LeftRectangle(func, Count, MinX, MaxX).ToString();
            else LeftRectListBoxItem.Content = "Левые прямоугольники, результат: ";

            if (RightRectCheckBox.IsChecked == true)
                RightRectListBoxItem.Content = "Правые прямоугольники, результат: " + RightRectangle(func, Count, MinX, MaxX).ToString();
            else RightRectListBoxItem.Content = "Правые прямоугольники, результат: ";

            if (CentralRectCheckBox.IsChecked == true)
                CentralRectListBoxItem.Content = "Центральные прямоугольники, результат: " + CentralRectangle(func, Count, MinX, MaxX).ToString();
            else CentralRectListBoxItem.Content = "Центральные прямоугольники, результат: ";

            if (TrapeziumCheckBox.IsChecked == true)
                TrapeziumListBoxItem.Content = "Трапеции, результат: " + Trapezium(func, Count, MinX, MaxX).ToString();
            else TrapeziumListBoxItem.Content = "Трапеции, результат: ";
        }

        private void GetDiffValuesButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = CreateDataTable();
            diffDataGrid.DataContext = dataTable.DefaultView;

            firstDiffResult.Content = "Первая производная в заданной точке: " + Dy(CurrentX, CurrentStep).ToString();
            secondDiffResult.Content = "Вторая производная в заданной точке: " + Ddy(CurrentX, CurrentStep).ToString();
        }

        public DataTable CreateDataTable()
        {
            double a = MinX;
            double step = (MaxX - MinX) / Count;
            List<Pair> pairs = new List<Pair>();

            for (int i = 0; i <= Count; i++, a += step)            
                pairs.Add(new Pair(a, func(a)));
            

            int k = 1;
            var dataTable = new DataTable();
            DataRow xRow;
            DataRow yRow;
            yRow = dataTable.NewRow();
            xRow = dataTable.NewRow();

            dataTable.Columns.Add();
            xRow[0] = "x";
            yRow[0] = "f(x)";

            foreach (var pair in pairs)
            {
                dataTable.Columns.Add();
                xRow[k] = pair.X;
                yRow[k] = pair.Y;
                k++;
            }
            dataTable.Rows.Add(xRow);
            dataTable.Rows.Add(yRow);            
            return dataTable;
        }

        public double Dy(double x, double step) => (func(x + step) - func(x)) / step; //первая производная


        public double Ddy(double x, double step) => (func(x + step) - 2 * func(x) + func(x - step)) / Math.Pow(step, 2); //вторая производная        


        public double f23(double x) => Math.Pow(x, 2) / Math.Sqrt(Math.Pow(x, 2) + 2);


        public double f25(double x) => Math.Sqrt(Math.Pow(x, 2) + 1) / (2 * x + 2.5);


        public double ftest(double x) => 1 / (1 + x);


        static double LeftRectangle(FuncDelegate func, double n, double a, double b)
        {
            double x, step, s;
            step = (b - a) / n;
            s = 0;
            for (x = a; x < b; x += step)
                s += func(x) * step;
            return s;
        }

        static double RightRectangle(FuncDelegate func, double n, double a, double b)
        {
            double x, step, s;
            step = (b - a) / n;
            s = 0;
            for (x = a; x < b; x += step)
                s += func(x + step) * step;
            return s;
        }

        static double CentralRectangle(FuncDelegate func, double n, double a, double b)
        {
            double x, step, s;
            step = (b - a) / n;
            s = 0;
            for (x = a + step / 2; x < b; x += step)
                s += func(x);
            return s * step;
        }

        static double Trapezium(FuncDelegate func, double n, double a, double b)
        {
            double x, step, s;
            step = (b - a) / n;
            s = (func(a) + func(b)) / 2;
            for (x = a + step; x < b; x += step)
                s += func(x);
            return s * step;
        }
    }
}
