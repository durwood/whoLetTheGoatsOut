using System;
using bombsweeper;

namespace bombsweeperXamarinTests
{
    public class FakeInputGetter : InputGetter
    {
        BoardClick _click;

        public override BoardClick GetClick()
        {
            return _click;
        }

        public void SetInput(int x, int y)
        {
            _click.X = x;
            _click.Y = y;
        }
    }
}