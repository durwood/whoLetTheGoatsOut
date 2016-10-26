using System;

namespace bombsweeper
{

	public class Cell
	{
		public char Value;

		public Cell()
		{
			Value = '▉';
		}

		public void Print()
		{
			Console.Write(Value);
		}
	}
}
