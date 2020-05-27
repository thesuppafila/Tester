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

namespace NM_Sem_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public delegate double FuncDelegate(double x, double y);


    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(Window));
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register("Step", typeof(double), typeof(Window));
        public static readonly DependencyProperty AProperty = DependencyProperty.Register("A", typeof(double), typeof(Window));
        public static readonly DependencyProperty BProperty = DependencyProperty.Register("B", typeof(double), typeof(Window));

        public double Y {
            get { return (double)this.GetValue(YProperty); }
            set { this.SetValue(YProperty, value); }
        }
        public double Step {
            get { return (double)this.GetValue(StepProperty); }
            set { this.SetValue(StepProperty, value); }
        }
        public double A {
            get { return (double)this.GetValue(AProperty); }
            set { this.SetValue(AProperty, value); }
        }
        public double B {
            get { return (double)this.GetValue(BProperty); }
            set { this.SetValue(BProperty, value); }
        }

        FuncDelegate func;
        List<double> xList;
        List<double> yListE;
        List<double> yListA;
        List<double> yListRK;
        List<double> Q;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            comboBox1.SelectedIndex = 0;
            Step = 0.1;
            B = 1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            switch (comboBox.SelectedIndex)
            {
                case 0:
                    func = new FuncDelegate(f23); break;
                case 1:
                    func = new FuncDelegate(f25); break;
            }
        }

        private void GetResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (EilerCheckBox.IsChecked == true)
                Eiler(A, Y);
            if (AdamsCheckBox.IsChecked == true)
                Adams(A, Y);
            if (RKCheckBox.IsChecked == true)
                RK(A, Y);
            FillDataGrid(EilerDataGrid);
        }

        public double f25(double x, double y) => 2 * x * y + 5 * x - y + Math.Pow(y, 2);

        public double f23(double x, double y) => Math.Sin(y - 2 * x) + 2 * y;

        public void Eiler(double x, double y) //эйлер
        {
            int n = (int)Math.Abs((B - A) / Step);
            xList = new List<double>() { x };
            yListE = new List<double>() { y };
            for (int i = 1; i < n; i++)
            {
                y += Step * func(x, y);
                x += Step;
                yListE.Add(y);
                xList.Add(x);
            }
            //FillDataGrid(EilerDataGrid);
        }

        public void Adams(double x, double y) //адамс
        {
            int n = (int)Math.Abs((B - A) / Step);
            xList = new List<double>() { x };
            yListA = new List<double>() { y };
            Q = new List<double>() { Step * func(x, y) };
            for (int i = 1; i < 4; i++)
            {
                double k1 = Step * func(x, y);
                double k2 = Step * func(x + Step / 2, y + k1 / 2);
                double k3 = Step * func(x + Step / 2, y + k2 / 2);
                double k4 = Step * func(x + Step, y + k3);
                double dy = (k1 + 2 * k2 + 2 * k3 + k4) / 6;

                y += dy;
                x += Step;
                yListA.Add(y);
                xList.Add(x);
                Q.Add(Step * func(x, y));
            }
            for (int i = 3; i < n; i++)
            {
                y += DeltaY(i);
                x += Step;
                xList.Add(x);
                yListA.Add(y);
                Q.Add(Step * func(x, y));
            }

            //FillDataGrid(AdamsDataGrid);
        }

        public double DeltaY(int n)
        {
            double Qn1 = Q[n] - Q[n - 1];
            double Qn2 = Q[n] - 2 * Q[n - 1] + Q[n - 2];
            double Qn3 = Q[n] - 3 * Q[n - 1] + 3 * Q[n - 2] - Q[n - 3];
            return Q[3] + Qn1 / 2 + 5 * Qn2 / 12 + 3 * Qn3 / 8;
        }

        public void RK(double x, double y) //рунге-кутт
        {
            int n = (int)Math.Abs((B - A) / Step);
            xList = new List<double>() { x };
            yListRK = new List<double>() { y };

            for (int i = 1; i < n; i++)
            {
                double k1 = Step * func(x, y);
                double k2 = Step * func(x + Step / 2, y + k1 / 2);
                double k3 = Step * func(x + Step / 2, y + k2 / 2);
                double k4 = Step * func(x + Step, y + k3);
                double dy = (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                y += dy;
                x += Step;
                yListRK.Add(y);
                xList.Add(x);
            }
            //FillDataGrid(RKDataGrid);
        }

        public void FillDataGrid(DataGrid dg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("x");
            dt.Columns.Add("Эйлер");
            dt.Columns.Add("Адамс");
            dt.Columns.Add("Рунге-Кутта");
            for (int i = 0; i < xList.Count; i++)
                dt.Rows.Add(xList[i], yListE[i], yListA[i], yListRK[i]);
            dg.ItemsSource = dt.DefaultView;
        }
    }
}
