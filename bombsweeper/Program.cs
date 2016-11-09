using System;
using System.Collections.Generic;

namespace bombsweeper
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var options = ParseArgs(args);
            var board = CreateBoard(options);
            var game = new Game(board, new ConsoleUi(board.GetSize()), new ConsoleCommandInterface(2 + board.GetSize() + 2));
            game.Run();
            Console.ReadKey();
        }

        private static Board CreateBoard(Dictionary<string, bool> options)
        {
            Board board;
            if (options["simpleBoard"])
                board = BoardGenerator.Build3();
            else if (options["generateBoard"])
            {
                var rnd = new RandomGenerator();
                var boardGenerator = new BoardGenerator(rnd);
                board = boardGenerator.GenerateBoard(9, 10);
            }
            else
                board = BoardGenerator.Build9();
            return board;
        }

        private static Dictionary<string, bool> ParseArgs(string[] args)
        {
            var dict = new Dictionary<string, bool>
            {
                {"generateBoard", false},
                {"simpleBoard", false}
            };
            foreach (var arg in args)
                if (arg == "--generate")
                    dict["generateBoard"] = true;
                else if (arg == "--simple")
                    dict["simpleBoard"] = true;
            return dict;
        }
    }
}