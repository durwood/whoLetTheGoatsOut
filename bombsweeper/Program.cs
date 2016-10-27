using System;

namespace bombsweeper
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            var board = new Board(4);
            board.AddBomb(0, 3);
            board.AddBomb(2, 1);
            
            var game = new Game(new InputGetter(), board);
			game.Run();
		    Console.ReadKey();
		}
	}

	


	

	
}

