using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class Solve
    {
        double[,] A;
        double[] d;
        public double[] Answer;
        public Solve(double[,] a, double[] column)
        {
            A = a;
            d = column;
            Answer = Get_Answer(A, d);
        }

        public double[] GetAnswer(double[,] A, double[] d)
        {
            int n = A.GetLength(0);
            //получение диагоналей
            double[] a = new double[n];
            double[] b = new double[n];
            double[] c = new double[n];
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
                b[i] = A[i, i];
            for (int i = 1; i < n; i++)
                a[i] = A[i, i - 1];
            for (int i = 0; i < n - 1; i++)
                c[i] = A[i, i + 1];

            double[] beta = new double[n];
            double[] alpha = new double[n];

            //коэффициенты
            beta[0] = b[0];
            alpha[0] = d[0] / beta[0];

            for (int i = 1; i < n; i++)
            {
                beta[i] = b[i] - (a[i] * c[i - 1] / beta[i - 1]);
                alpha[i] = (d[i] - a[i] * alpha[i - 1]) / beta[i];
            }
            x[n - 1] = alpha[n - 1];

            //получение результата
            for (int i = n - 2; i >= 0; i--)
                x[i] = alpha[i] - (c[i] * x[i + 1] / beta[i]);

            return x;
        }

        public double[] Get_Answer(double[,] A, double[] d)
        {
            int n = A.GetLength(0);
            //получение диагоналей
            double[] a = new double[n];
            double[] b = new double[n];
            double[] c = new double[n];
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
                b[i] = A[i, i];
            for (int i = 1; i < n; i++)
                a[i] = A[i, i - 1];
            for (int i = 0; i < n - 1; i++)
                c[i] = A[i, i + 1];

            double[] beta = new double[n];
            double[] alpha = new double[n];
            double[] gamma = new double[n];

            //коэффициенты

            gamma[0] = b[0];
            alpha[0] = -(c[0] / gamma[0]);
            beta[0] = d[0] / gamma[0];

            for (int i = 1; i < n; i++)
            {
                gamma[i] = b[i] + a[i] * alpha[i - 1];
                alpha[i] = -(c[i] / gamma[i]);
                beta[i] = (d[i] - a[i] * beta[i - 1]) / gamma[i];
            }

            x[n - 1] = beta[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = alpha[i] * x[i + 1] + beta[i];
            }
            return x;
        }

        public override string ToString()
        {
            for (int i = 0; i < A.GetLength(0); i++, Console.WriteLine())
                for (int j = 0; j < A.GetLength(1); j++)
                    Console.Write("{0,5}", A[i, j]);
            Console.WriteLine();
            for (int i = 0; i < d.Length; i++)
            {
                Console.WriteLine("x{0} = {1}", i + 1, Answer[i]);
            }
            return "";
        }
    }
}
