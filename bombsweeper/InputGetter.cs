using System;

namespace bombsweeper
{
    public struct BoardClick
    {
        public int X;
        public int Y;
    }

    public class InputGetter
    {
        public virtual BoardClick GetClick()
        {
            var line = Console.ReadLine();
            var items = line.Split(',');
            return new BoardClick {X = int.Parse(items[0]) - 1, Y = int.Parse(items[1]) - 1};
        }
    }
}