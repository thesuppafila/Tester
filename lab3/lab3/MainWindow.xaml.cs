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

namespace lab3
{
    public delegate double FuncDelegate(double x);

    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty MinXProperty = DependencyProperty.Register("MinX", typeof(double), typeof(Window));
        public static readonly DependencyProperty MaxXProperty = DependencyProperty.Register("MaxX", typeof(double), typeof(Window));
        public static readonly DependencyProperty MinYProperty = DependencyProperty.Register("MinY", typeof(double), typeof(Window));
        public static readonly DependencyProperty MaxYProperty = DependencyProperty.Register("MaxY", typeof(double), typeof(Window));
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(Window));

        private int count;
        public int Count {
            get { return (int)this.GetValue(CountProperty); }
            set {
                count = (value < 4) ? 4 : (value > 20) ? 20 : value;
                this.SetValue(CountProperty, count);
            }
        }
        public double MinX {
            get { return (double)this.GetValue(MinXProperty); }
            set { this.SetValue(MinXProperty, value); }
        }
        public double MaxX {
            get { return (double)this.GetValue(MaxXProperty); }
            set { this.SetValue(MaxXProperty, value); }
        }

        public double MinY {
            get { return (double)this.GetValue(MinYProperty); }
            set { this.SetValue(MinYProperty, value); }
        }
        public double MaxY {
            get { return (double)this.GetValue(MaxYProperty); }
            set { this.SetValue(MaxYProperty, value); }
        }

        FuncDelegate func;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            comboBox1.SelectedIndex = 0;
            MinX = -10;
            MaxX = 10;
            Count = 5;
        }

        private void GetValuesButton_Click(object sender, RoutedEventArgs e)
        {
            double step = (MaxX - MinX) / (Count + 1);
            List<Pair> pairs = new List<Pair>();
            pairs.Add(new Pair(MinX, func));
            for (int i = 1; i <= Count; i++)
            {
                var p = new Pair(MinX + step * i, func);
                pairs.Add(p);
            }
            pairs.Add(new Pair(MaxX, func));
            dataGrid1.ItemsSource = pairs;

            var pixelWidth = OxGraph.ActualWidth;
            var pixelHeight = OxGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);

            OxGraph.Points = GetRawPoints((double v) => 0);
            OyGraph.Points = new PointCollection() { new Point(MapToPixel(0, MinX, MaxX, pixelWidth), 0), new Point(MapToPixel(0, MinX, MaxX, pixelWidth), pixelHeight) };
            rawGraph.Points = GetRawPoints(func);
            linearGraph.Points = GetLinear(pairs);
            stirlingGraph.Points = GetPointsStirling(pairs);
            neutonGraph.Points = GetPointsNeuton(pairs);
        }

        PointCollection GetRawPoints(FuncDelegate func)
        {
            var pixelWidth = linearGraph.ActualWidth;
            var pixelHeight = linearGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (double pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, MinX, MaxX);
                var y = func(x1);
                var pixelY = pixelHeight - MapToPixel(y, MinY, MaxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            return points;
        }

        PointCollection GetLinear(List<Pair> pairs)
        {
            var pixelWidth = linearGraph.ActualWidth;
            var pixelHeight = linearGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (double pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, MinX, MaxX);
                var y = Linear(pairs.Select(x => x.X).ToArray(), pairs.Select(x => x.Y).ToArray(), x1);
                var pixelY = pixelHeight - MapToPixel(y, MinY, MaxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            return points;
        }

        PointCollection GetPointsStirling(List<Pair> pairs)
        {
            var pixelWidth = linearGraph.ActualWidth;
            var pixelHeight = linearGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (int pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, MinX, MaxX);
                var y = Stirling(pairs.Select(x => x.X).ToArray(), pairs.Select(x => x.Y).ToArray(), x1);
                var pixelY = pixelHeight - MapToPixel(y, MinY, MaxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            return points;
        }

        PointCollection GetPointsNeuton(List<Pair> pairs)
        {
            var pixelWidth = linearGraph.ActualWidth;
            var pixelHeight = linearGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (int pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, MinX, MaxX);
                var y = Newton(pairs.Select(x => x.X).ToArray(), pairs.Select(x => x.Y).ToArray(), x1);
                var pixelY = pixelHeight - MapToPixel(y, MinY, MaxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            return points;
        }

        double MapFromPixel(double pixelV, double pixelMax, double minV, double maxV) => minV + (pixelV / pixelMax) * (MaxX - MinX);

        double MapToPixel(double v, double minV, double maxV, double pixelMax) => (v - minV) / (maxV - minV) * pixelMax;

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            switch (comboBox.SelectedIndex)
            {
                case 0: functionLabel.Content = "Функция: F(x) = sin(0.47*x + 0.2) + x^2"; func = new FuncDelegate(f23); stirlingCheckBox.IsChecked = true; neutonCheckBox.IsChecked = false; MinY = -1; MaxY = 100; break;
                case 1: functionLabel.Content = "Функция: F(x) = cos(x) + (x / 5)"; func = new FuncDelegate(f25); stirlingCheckBox.IsChecked = false; neutonCheckBox.IsChecked = true; MinY = -5; MaxY = 5; break;
            }
        }

        static double Linear(double[] x, double[] fx, double _x)
        {
            int x0 = 0;
            int x1 = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > _x)
                {
                    x1 = i;
                    break;
                }
                x0 = i;
            }
            return (_x - x[x0]) * (fx[x1] - fx[x0]) / (x[x1] - x[x0]) + fx[x0];
        }

        static double Stirling(double[] x, double[] fx, double x1)
        {
            double h, a, u, y1 = 0, d = 1, temp1 = 1, temp2 = 1, k = 1, l = 1;
            int n = x.Length;
            double[,] delta = new double[n, n];
            int i, j, s;
            h = x[1] - x[0];
            s = (int)Math.Floor((double)(n / 2));
            a = x[s];
            u = (x1 - a) / h;

            for (i = 0; i < n - 1; ++i)
            {
                delta[i, 0] = fx[i + 1] - fx[i];
            }

            for (i = 1; i < n - 1; ++i)
            {
                for (j = 0; j < n - i - 1; ++j)
                {
                    delta[j, i] = delta[j + 1, i - 1] - delta[j, i - 1];
                }
            }

            y1 = fx[s];
            for (i = 1; i <= n - 1; ++i)
            {
                if (i % 2 != 0)
                {
                    if (k != 2)
                    {
                        temp1 *= (Math.Pow(u, k) - Math.Pow((k - 1), 2));
                    }
                    else
                    {
                        temp1 *= (Math.Pow(u, 2) - Math.Pow((k - 1), 2));
                    }

                    ++k;
                    d *= i;
                    s = (int)Math.Floor((double)((n - i) / 2));
                    y1 += (temp1 / (2 * d)) * (delta[s, i - 1] + delta[s - 1, i - 1]);
                }
                else
                {
                    temp2 *= (Math.Pow(u, 2) - Math.Pow((l - 1), 2));
                    ++l;
                    d *= i;
                    s = (int)Math.Floor((double)((n - i) / 2));
                    y1 += (temp2 / (d)) * (delta[s, i - 1]);
                }
            }
            return y1;
        }

        public double f23(double x)
        {
            return Math.Sin(0.47 * x + 0.2) + Math.Pow(x, 2);
        }

        public double f25(double x)
        {
            return Math.Cos(x) + (x / 5);
        }

        public double dy(List<double> Y, List<double> X)
        {
            if (Y.Count > 2)
            {
                List<double> Yleft = new List<double>(Y);
                List<double> Xleft = new List<double>(X);
                Xleft.RemoveAt(0);
                Yleft.RemoveAt(0);
                List<double> Yright = new List<double>(Y);
                List<double> Xright = new List<double>(X);
                Xright.RemoveAt(Y.Count - 1);
                Yright.RemoveAt(Y.Count - 1);
                return (dy(Yleft, Xleft) - dy(Yright, Xright)) / (X[X.Count - 1] - X[0]);
            }
            else if (Y.Count == 2)
            {
                return (Y[1] - Y[0]) / (X[1] - X[0]);
            }
            else
            {
                throw new Exception("Not available parameter");
            }
        }

        public double Newton(double[] X, double[] Y, double x)
        {
            double res = Y[0];
            double buf;
            List<double> Xlist;
            List<double> Ylist;
            for (int i = 1; i < Y.Length; i++)
            {
                Xlist = new List<double>();
                Ylist = new List<double>();
                buf = 1;
                for (int j = 0; j <= i; j++)
                {
                    Xlist.Add(X[j]);
                    Ylist.Add(Y[j]);
                    if (j < i)
                        buf *= x - X[j];
                }
                res += dy(Ylist, Xlist) * buf;
            }
            return res;
        }

        public double Factorial(int arg)
        {
            double res = 1;
            for (int i = 2; i <= arg; i++)
            {
                res *= i;
            }
            return res;
        }

        public double dy_h(List<double> Y, List<double> X, int number, int index)
        {
            if (number > 1)
            {
                return (dy_h(Y, X, number - 1, index + 1) - dy_h(Y, X, number - 1, index));
            }
            else if (number == 1)
            {
                return (Y[index + 1] - Y[index]);
            }
            else
            {
                throw new Exception("Not available parameter");
            }
        }

        public double GetValue(double[] X, double[] Y, double x, double h)
        {
            double res = Y[0];
            double buf;
            List<double> Xlist = new List<double>(X);
            List<double> Ylist = new List<double>(Y);
            double q = (x - X[0]) / h;
            buf = 1;
            for (int i = 1; i < Y.Length; i++)
            {
                buf *= (q - i + 1) / i;
                res += dy_h(Ylist, Xlist, i, 0) * buf;
            }
            return res;
        }
    }
}
