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
    public class Pair
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Pair(double _x)
        {
            X = _x;
            Y = f(X);
        }

        public double f(double x)
        {
            return Math.Round(Math.Sin(0.47 * x + 0.2) + Math.Pow(x, 2), 2);
        }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double minX;
        double maxX;

        double minY;
        double maxY;

        public double f(double x)
        {
            return Math.Round(Math.Sin(0.47 * x + 0.2) + Math.Pow(x, 2), 2);
        }

        public MainWindow()
        {
            InitializeComponent();
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

            GetLinear();
            GetRaw();
            //return;*/
            stirlingGraph.Points.Clear();
            double a = double.Parse(aValue.Text);
            minX = a;
            minY = f(minX);
            double b = double.Parse(bValue.Text);
            maxX = b;
            maxY = f(maxX);
            int n = int.Parse(nValue.Text);
            double step = (b - a) / n;
            List<Pair> pairs = new List<Pair>();
            double[] x = new double[n];
            double[] fx = new double[n];
            for (int i = 0; i < n; i++)
            {
                var p = new Pair(a + step * i);
                pairs.Add(p);
                x[i] = p.X;
                fx[i] = p.Y;
            }
            dataGrid1.ItemsSource = pairs;
            
            var pixelWidth = stirlingGraph.ActualWidth;
            var pixelHeight = stirlingGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (int pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, a, b);
                var y = Stirling(x, fx, x1, n);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            stirlingGraph.Points = points;
        }

        void GetLinear()
        {
            double a = double.Parse(aValue.Text);
            minX = a;
            minY = f(minX);
            double b = double.Parse(bValue.Text);
            maxX = b;
            maxY = f(maxX);
            int n = int.Parse(nValue.Text);
            double step = (b - a) / n;
            List<Pair> pairs = new List<Pair>();
            double[] x = new double[n];
            double[] fx = new double[n];
            for (int i = 0; i < n; i++)
            {
                var p = new Pair(a + step * i);
                pairs.Add(p);
                x[i] = p.X;
                fx[i] = p.Y;
            }
            dataGrid1.ItemsSource = pairs;

            var pixelWidth = linearGraph.ActualWidth;
            var pixelHeight = linearGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (double pixelX = 0; pixelX < pixelWidth; pixelX += pixelWidth / n)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, a, b);
                var y = f(x1);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            linearGraph.Points = points;
        }

        void GetRaw()
        {
            double a = double.Parse(aValue.Text);
            minX = a;
            minY = f(minX);
            double b = double.Parse(bValue.Text);
            maxX = b;
            maxY = f(maxX);
            int n = int.Parse(nValue.Text);
            double step = (b - a) / n;
            List<Pair> pairs = new List<Pair>();
            double[] x = new double[n];
            double[] fx = new double[n];
            for (int i = 0; i < n; i++)
            {
                var p = new Pair(a + step * i);
                pairs.Add(p);
                x[i] = p.X;
                fx[i] = p.Y;
            }
            dataGrid1.ItemsSource = pairs;

            var pixelWidth = rawGraph.ActualWidth;
            var pixelHeight = rawGraph.ActualHeight;
            PointCollection points = new PointCollection((int)pixelWidth + 1);
            for (double pixelX = 0; pixelX < pixelWidth; pixelX++)
            {
                var x1 = MapFromPixel(pixelX, pixelWidth, a, b);
                var y = f(x1);
                var pixelY = pixelHeight - MapToPixel(y, minY, maxY, pixelHeight);
                points.Add(new Point(pixelX, pixelY));
            }
            rawGraph.Points = points;
        }

        double MapFromPixel(double pixelV, double pixelMax, double minV, double maxV) =>
            minV + (pixelV / pixelMax) * (maxX - minX);

        double MapToPixel(double v, double minV, double maxV, double pixelMax) =>
            (v - minV) / (maxV - minV) * pixelMax;

        static double Stirling(double[] x, double[] fx, double x1, int n)
        {
            double h, a, u, y1 = 0, d = 1, temp1 = 1, temp2 = 1, k = 1, l = 1;
            double[,] delta = new double[n, n];
            int i, j, s;
            h = x[1] - x[0];
            s = (int)Math.Floor((double)(n / 2));
            a = x[s]; 
            u = (x1 - a) / h;

            // Готовим прямую разницу
            // стол
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

            // Расчет f (x) с использованием Стирлинга
            // формула
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
    }
}
