using System;

namespace bombsweeper
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            var board = new Board(9);
            board.AddBomb(2,1);
            board.AddBomb(1,2);
            board.AddBomb(7, 2);
            board.AddBomb(6, 3);
            board.AddBomb(8, 3);
            board.AddBomb(6, 4);
            board.AddBomb(7, 4);
            board.AddBomb(3, 5);
            board.AddBomb(0, 7);
            board.AddBomb(0, 8);
            
            var game = new Game(new InputGetter(), board);
			game.Run();
		    Console.ReadKey();
		}
	}

	


	

	
}

