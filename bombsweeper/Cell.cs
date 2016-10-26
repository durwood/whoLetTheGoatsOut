using System;

namespace bombsweeper
{

	public class Cell
	{
        public const char Block = '\u25A0';
        public char Value;

		public Cell()
		{
		    Value = Block;

        }

	    public string Display()
	    {
	        return Value.ToString();
	    }
	}
}
