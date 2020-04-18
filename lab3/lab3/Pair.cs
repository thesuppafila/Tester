using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Pair
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Pair(double _x, FuncDelegate f)
        {
            X = Math.Round(_x, 2);
            Y = Math.Round(f(X), 2);
        }
    }
}
