using System;

namespace bombsweeper
{

	public class Cell
	{
		public char Value;

		public Cell()
		{
		    Value = '\u25A0';

        }

		public void Print()
		{
			Console.Write($"{Value} ");
		}
	}
}
