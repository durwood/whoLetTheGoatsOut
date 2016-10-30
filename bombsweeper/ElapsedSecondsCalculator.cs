using System;

namespace bombsweeper
{
    public class ElapsedSecondsCalculator
    {
        private readonly DateTime _startTime;
        private int _elapsedSec;

        public ElapsedSecondsCalculator()
        {
            _startTime = DateTime.Now;
            _elapsedSec = 0;
        }

        public int? NewElapsedSec()
        {
            var curTime = DateTime.Now;
            var newElapsedSec = (int) new TimeSpan(curTime.Ticks - _startTime.Ticks).TotalSeconds;
            if (newElapsedSec != _elapsedSec)
            {
                _elapsedSec = newElapsedSec;
                return newElapsedSec;
            }
            return null;
        }
    }
}