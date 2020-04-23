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
        double minX;
        double maxX;

        double minY;
        double maxY;

        FuncDelegate func;

        public MainWindow()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

            DrawingGroup group = new DrawingGroup();

            GeometryDrawing drw = new GeometryDrawing();
            GeometryGroup gg = new GeometryGroup();

            drw.Brush = Brushes.Beige;
            drw.Pen = new Pen(Brushes.LightGray, 0.01);

            RectangleGeometry myRectGeometry = new RectangleGeometry();
            myRectGeometry.Rect = new Rect(0, 0, 1, 1);
            gg.Children.Add(myRectGeometry);

            drw.Geometry = gg;
            group.Children.Add(drw);
        }

        private void GetValuesButton_Click(object sender, RoutedEventArgs e)
        {
            minX = double.Parse(aValue.Text);
            maxX = double.Parse(bValue.Text); ;
            int n = int.Parse(nValue.Text);
            double step = (maxX - minX) / n;
            List<Pair> pairs = new List<Pair>();
            double[] x = new double[n];
            double[] fx = new double[n];
            for (int i = 0; i < n; i++)
            {
                var p = new Pair(minX + step * i, func);
                pairs.Add(p);
                x[i] = p.X;
                fx[i] = p.Y;
            }
            pairs.Add(new Pair(maxX, func));
            dataGrid1.ItemsSource = pairs;

            var pixelWidth = stirlingGraph.ActualWidth;
            var pixelHeight = stirlingGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);

            OxGraph.Points = GetRawPoints((double v) => 0); //new PointCollection() { new Point(0, pixelHeight - MapToPixel(0, minY, maxY, pixelHeight)), new Point(pixelWidth, pixelHeight - MapToPixel(0, minY, maxY, pixelHeight)) };
            OyGraph.Points = new PointCollection() { new Point(MapToPixel(0, minX, maxX, pixelWidth), 0), new Point(MapToPixel(0, minX, maxX, pixelWidth), pixelHeight) };
            rawGraph.Points = GetRawPoints(func);
            linearGraph.Points = GetLinear(pairs);// GetPoints(pixelWidth / n);
            stirlingGraph.Points = GetPointsStirling(pairs);
            neutonGraph.Points = GetPointsNeuton(pairs);
        }

        double osx(double x) => 0;

        PointCollection GetRawPoints(FuncDelegate func)
        {
            var pixelWidth = linearGraph.ActualWidth;
            var pixelHeight = linearGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (double pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, minX, maxX);
                var y = func(x1);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
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
                var x1 = MapFromPixel(pixelX, pixelWidth, minX, maxX);
                var y = Linear(pairs.Select(x => x.X).ToArray(), pairs.Select(x => x.Y).ToArray(), x1);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
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
                var x1 = MapFromPixel(pixelX, pixelWidth, minX, maxX);
                var y = Stirling(pairs.Select(x => x.X).ToArray(), pairs.Select(x => x.Y).ToArray(), x1);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
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
                var x1 = MapFromPixel(pixelX, pixelWidth, minX, maxX);
                var y = Newton(pairs.Select(x => x.X).ToArray(), pairs.Select(x => x.Y).ToArray(), x1); //Заменить на Ньютона
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            return points;
        }

        double MapFromPixel(double pixelV, double pixelMax, double minV, double maxV) =>
            minV + (pixelV / pixelMax) * (maxX - minX);

        double MapToPixel(double v, double minV, double maxV, double pixelMax) =>
            (v - minV) / (maxV - minV) * pixelMax;

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
            int n = 5;// x.Length;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            switch (comboBox.SelectedIndex)
            {
                case 0: functionLabel.Content = "Функция: F(x) = sin(0.47*x + 0.2) + x^2"; func = new FuncDelegate(f23); stirlingCheckBox.IsChecked = true; neutonCheckBox.IsChecked = false; minY = -1; maxY = 100; break;
                case 1: functionLabel.Content = "Функция: F(x) = cos(x) + (x / 5)"; func = new FuncDelegate(f25); stirlingCheckBox.IsChecked = false; neutonCheckBox.IsChecked = true; minY = -5; maxY = 5; break;
            }
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
