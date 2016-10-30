using System;

namespace bombsweeper
{
    public class ElapsedSecondsCalculator
    {
        private readonly DateTime _startTime;

        public ElapsedSecondsCalculator()
        {
            _startTime = DateTime.Now;
        }

        public int ElapsedSec()
        {
            var curTime = DateTime.Now;
            return (int) new TimeSpan(curTime.Ticks - _startTime.Ticks).TotalSeconds;
        }
    }
}