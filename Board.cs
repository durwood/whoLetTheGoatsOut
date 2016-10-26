using System;

namespace bombsweeper
{

	public class Board
	{
		readonly int _size;
		Cell[,] _cells;

		public Board(int size)
		{
			_size = size;
			_cells = new Cell[_size, _size];
			for (var row = 0; row < _size; ++row)
				for (var col = 0; col < _size; ++col)
					_cells[row, col] = new Cell();
		}

		public void Print()
		{
			for (var row = 0; row < _size; ++row)
			{
				for (var col = 0; col < _size; ++col)
					_cells[row, col].Print();
				Console.WriteLine();
			}
		}
	}
	
}
