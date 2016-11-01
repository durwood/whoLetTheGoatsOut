using System;
using System.Collections.Generic;

namespace bombsweeper
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var options = ParseArgs(args);

            if (options["windows"])
                throw new NotImplementedException("Only Console Implemented");
            PlayConsoleVersion(options);
        }

        private static void PlayConsoleVersion(Dictionary<string, bool> options)
        {
            var board = CreateBoard(options);
            var game = new Game(board);
            game.Run();
            Console.ReadKey();
        }

        private static Board CreateBoard(Dictionary<string, bool> options)
        {
            Board board;
            if (options["simpleBoard"])
                board = Build3();
            else if (options["generateBoard"])
                board = GenerateBoard();
            else
                board = Build9();
            return board;
        }

        private static Board GenerateBoard()
        {
            var board = new Board(9);
            int[,] bombLocations = new int[9, 9];
            var rng = new Random();
            for (var bomb = 0; bomb < 9; ++bomb)
                AddBomb(bombLocations, rng);
            return board;
        }

        private static void AddBomb(int[,] bombLocations, Random rng)
        {
            //bool addAdjacent = rng.NextDouble() < 0.33;
        }

        private static Dictionary<string, bool> ParseArgs(string[] args)
        {
            var dict = new Dictionary<string, bool>
            {
                {"windows", false},
                {"generateBoard", false},
                {"simpleBoard", false}
            };
            foreach (var arg in args)
                if (arg == "--windows")
                    dict["windows"] = true;
                else if (arg == "--generate")
                    dict["generateBoard"] = true;
                else if (arg == "--simple")
                    dict["simpleBoard"] = true;
            return dict;
        }

        private static Board Build3()
        {
            var board = new Board(3);
            board.AddBomb(0, 0);
            board.AddBomb(1, 0);
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