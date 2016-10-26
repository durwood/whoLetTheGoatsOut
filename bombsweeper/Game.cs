using System;

namespace bombsweeper
{

	public class Game
	{
	    readonly Board _board;

		public Game(int boardSize)
		{
			_board = new Board(boardSize);
		}

		public void Run()
		{
			Console.WriteLine("Hello World!");
			Console.Write(_board.Display());
		}
	}
	
}
