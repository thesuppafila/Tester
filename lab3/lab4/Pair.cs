using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Pair
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Pair(double x, double fx)
        {
            X = Math.Round(x, 2);
            Y = Math.Round(fx, 2);
        }
    }
}
