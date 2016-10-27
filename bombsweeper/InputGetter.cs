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
        virtual public BoardClick GetClick()
        {
            string line = Console.ReadLine();
            var items = line.Split(new char[] { ',' });
            return new BoardClick { X = int.Parse(items[0]), Y = int.Parse(items[1]) };
        }
	}
}