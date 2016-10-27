using System;

namespace bombsweeper
{
	class MainClass
	{
		public static void Main(string[] args)
		{
            var game = new Game(new InputGetter(), 2);
			game.Run();
		    Console.ReadKey();
		}
	}

	


	

	
}

