using System.Collections.Generic;

namespace bombsweeper
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var options = ParseArgs(args);
            var board = CreateBoard(options);
            var game = new ConsoleGameController(board);
            game.Run();
        }

        private static Board CreateBoard(Dictionary<string, bool> options)
        {
            Board board;
            if (options["newBoard"])
            {
                var rnd = new RandomGenerator();
                var boardGenerator = new BoardGenerator(rnd);
                board = boardGenerator.GenerateBoard(9, 10);
            }
            else
                board = BoardGenerator.GetStandardBoard();
            return board;
        }

        private static Dictionary<string, bool> ParseArgs(string[] args)
        {
            var dict = new Dictionary<string, bool>
            {
                {"newBoard", false},
            };
            foreach (var arg in args)
                if (arg == "--generate")
                    dict["generateBoard"] = true;
            return dict;
        }
    }
}