using System;

namespace Tester.Model
{
    public static class Randomizer
    {
        private static Random random = new Random();

        public static int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
