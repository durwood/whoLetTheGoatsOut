using System;
using System.Collections.Generic;

namespace bombsweeper
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var options = ParseArgs(args);
            var board = Board.Create(options);
            var view = new ConsoleView(board);
            var commandInterface = new CommandInterface(view);
            var game = new Game(board, view, commandInterface);
            game.Run();
            Console.ReadKey();
        }

        private static Dictionary<string, bool> ParseArgs(string[] args)
        {
            var dict = new Dictionary<string, bool>
            {
                {"newBoard", false},
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