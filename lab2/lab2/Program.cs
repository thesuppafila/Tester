using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите начало интервало: ");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите конец интервала: ");
            double b = double.Parse(Console.ReadLine());

            Console.WriteLine("Установите величину погрешности");
            double eps = double.Parse(Console.ReadLine());
            int max_steps = 10000;
            double length = b - a;

            double f_a = F(a);
            double f_b = F(b);
            int k = 0;
            double x = 0;

            while (b - a > eps && k < max_steps)
            {
                x = (a + b) / 2;
                if (F(a) * F(x) <= 0)
                {
                    b = x;
                }
                else
                    a = x;
            }

            Console.WriteLine("Корень уравнения  = " + (a + b) / 2);
            Console.ReadKey();
        }

        static double F(double x)
        {
            return 1/Math.Tan(x) - x / 10;
        }
    }
}
