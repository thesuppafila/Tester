using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

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
                    func = new FuncDelegate(f23);
                    EquationLabel.Content = "y' = sin(y - 2x) + 2y";
                    break;
                case 1:
                    func = new FuncDelegate(f25);
                    A = -10;
                    B = 1;
                    Step = 0.001;
                    EquationLabel.Content = "y' = 2xy + 5x - y + y^2";
                    break;
                case 2:
                    func = new FuncDelegate(test);
                    EquationLabel.Content = "y' = x^2 * y^(1/3)";
                    break;
                case 3:
                    func = new FuncDelegate(test1);
                    EquationLabel.Content = "y' = y + x * Math.Pow(Math.E, x)";
                    break;
            }
        }

        private void GetResultButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            Eiler(A, Y);
            sWatch.Stop();
            eilerTimeTextBox.Text = sWatch.ElapsedTicks.ToString();
            sWatch.Start();
            Adams(A, Y);
            sWatch.Stop();
            adamsTimeTextBox.Text = sWatch.ElapsedTicks.ToString();
            sWatch.Start();
            RK(A, Y);
            sWatch.Stop();
            RKTimeTextBox.Text = sWatch.ElapsedTicks.ToString();
            FillDataGrid(mainDataGrid);
        }

        public double f23(double x, double y) => Math.Sin(y - 2 * x) + 2 * y;

        public double f25(double x, double y) => 2 * x * y + 5 * x - y + Math.Pow(y, 2);

        public double test(double x, double y) => x * x * Math.Pow(y, 1 / 3);

        public double test1(double x, double y) => y + x * Math.Pow(Math.E, x);


        public void Eiler(double x, double y) //эйлер
        {
            int n = (int)Math.Abs((B - A) / Step) + 1;
            xList = new List<double>() { x };
            yListE = new List<double>() { y };
            for (int i = 1; i < n; i++)
            {
                y += Step * func(x, y);
                x += Step;
                yListE.Add(y);
                xList.Add(x);
            }
        }

        public void Adams(double x, double y) //адамс
        {
            int n = (int)Math.Abs((B - A) / Step) + 1;
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
        }

        public double DeltaY(int n)
        {
            double Qn1 = Q[n] - Q[n - 1];
            double Qn2 = Q[n] - 2 * Q[n - 1] + Q[n - 2];
            double Qn3 = Q[n] - 3 * Q[n - 1] + 3 * Q[n - 2] - Q[n - 3];
            return Q[n] + Qn1 / 2 - Qn2 / 12 - Qn3 / 24;
        }

        public void RK(double x, double y) //рунге-кутт
        {
            int n = (int)Math.Abs((B - A) / Step) + 1;
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
        }

        public void FillDataGrid(DataGrid dg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("x");
            dt.Columns.Add("Эйлер");
            dt.Columns.Add("Адамс");
            dt.Columns.Add("Рунге-Кутта");

            for (int i = 0; i < xList.Count; i++)
            {
                dt.Rows.Add(Math.Round(xList[i], 6), Math.Round(yListE[i], 6), Math.Round(yListA[i], 6), Math.Round(yListRK[i], 6));
            }
            dg.ItemsSource = dt.DefaultView;
            if (EilerCheckBox.IsChecked == false)
                mainDataGrid.Columns[1].Visibility = Visibility.Hidden;
            if (AdamsCheckBox.IsChecked == false)
                mainDataGrid.Columns[2].Visibility = Visibility.Hidden;
            if (RKCheckBox.IsChecked == false)
                mainDataGrid.Columns[3].Visibility = Visibility.Hidden;
        }
    }
}
