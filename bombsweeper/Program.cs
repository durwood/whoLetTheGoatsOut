using System;

namespace bombsweeper
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var board = Build9();

            var game = new Game(new CommandParser(), board);
            game.Run();
            Console.ReadKey();
        }

        private static Board Build2()
        {
            var board = new Board(2);
            board.AddBomb(0, 0);
            return board;
        }

        private static Board Build3()
        {
            var board = new Board(3);
            board.AddBomb(0, 0);
            board.AddBomb(1, 0);
            return board;
        }

        private static Board Build4()
        {
            var board = new Board(3);
            board.AddBomb(0, 0);
            return board;
        }

        private static Board Build9()
        {
            var board = new Board(9);
            board.AddBomb(2, 1);
            board.AddBomb(1, 2);
            board.AddBomb(7, 2);
            board.AddBomb(6, 3);
            board.AddBomb(8, 3);
            board.AddBomb(6, 4);
            board.AddBomb(7, 4);
            board.AddBomb(3, 5);
            board.AddBomb(0, 7);
            board.AddBomb(0, 8);
            return board;
        }
    }
}