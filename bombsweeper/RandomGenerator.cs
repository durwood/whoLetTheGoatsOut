using System;

namespace bombsweeper
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public double NextDouble()
        {
            return _random.NextDouble();
        }

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}