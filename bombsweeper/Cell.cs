using System;

namespace bombsweeper
{

	public class Cell
	{
        public const char Block = '\u25A0';
        public const char Space = ' ';
        private char Value;

		public Cell()
		{
		    Value = Block;
        }

        public string Display()
	    {
	        return Value.ToString();
	    }

        public char Reveal()
        {
            var contents = Value;
            Value = Space;
            return contents;
        }
	}
}
