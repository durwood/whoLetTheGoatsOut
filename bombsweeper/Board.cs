using System;
using System.Text;

namespace bombsweeper
{

	public class Board
	{
		readonly int _size;
	    readonly Cell[,] _cells;

		public Board(int size)
		{
			_size = size;
			_cells = new Cell[_size, _size];
			for (var row = 0; row < _size; ++row)
				for (var col = 0; col < _size; ++col)
					_cells[row, col] = new Cell();
		}

	    public string Display()
	    {
	        StringBuilder sb = new StringBuilder();
            for (var row = 0; row < _size; ++row)
            {
                for (var col = 0; col < _size; ++col)
                    sb.Append($"{_cells[row, col].Display()} ");
                sb.AppendLine();
            }
	        return sb.ToString();
	    }
	}
	
}
