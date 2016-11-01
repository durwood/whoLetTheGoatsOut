﻿using System;
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
            {
                var rnd = new RandomGenerator();
                var boardGenerator = new BoardGenerator(rnd);
                board = boardGenerator.GenerateBoard(9, 10);
            }
            else
                board = Build9();
            return board;
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