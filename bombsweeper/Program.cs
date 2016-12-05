using System;
using System.Collections.Generic;

namespace bombsweeper
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var options = ParseArgs(args, Game.DefaultArguments);
            var game = Game.Create(options, new ConsoleView());
            game.Run();
            Console.ReadKey();
        }

        private static Dictionary<string, bool> ParseArgs(string[] args, Dictionary<string, bool> defaultArguments)
        {
            var dict = defaultArguments;
            foreach (var arg in args)
                if (arg == "--generate")
                    dict["generateBoard"] = true;
                else if (arg == "--simple")
                    dict["simpleBoard"] = true;
            return dict;
        }
    }
}