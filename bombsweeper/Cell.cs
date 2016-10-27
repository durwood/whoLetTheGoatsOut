using System;

namespace bombsweeper
{

	public class Cell
	{
        public const char Block = '\u25A0';
        public const char Bomb = '*';
        public const char Space = ' ';
        public char Content;
        private bool IsRevealed;

		public Cell()
		{
            IsRevealed = false;
            Content = Space;
        }

        public string Display()
	    {
            return (IsRevealed ? Content : Block).ToString();
	    }

        public char Reveal()
        {
            IsRevealed = true;
            return Content;
        }

        public void AddBomb()
        {
            Content = Bomb;
        }
	}
}
