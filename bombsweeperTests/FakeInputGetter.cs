using bombsweeper;

namespace bombsweeperTests
{
    public class FakeInputGetter : InputGetter
    {
        private BoardClick _click;

        public override BoardClick GetCell()
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