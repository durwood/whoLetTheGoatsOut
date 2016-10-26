using System;

namespace bombsweeper
{

	public class Game
	{
		Board _board;

		public Game(int boardSize)
		{
			_board = new Board(boardSize);
		}

		public void Run()
		{
			Console.WriteLine("Hello World!");
			_board.Print();
		}
	}
	
}
