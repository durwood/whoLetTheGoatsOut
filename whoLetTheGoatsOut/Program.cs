using System;
using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //var rnd = new RandomGenerator();
            //var boardGenerator = new BoardGenerator(rnd);
            //var board = boardGenerator.GenerateBoard(9, 10);
            var board = BoardGenerator.GetStandardBoard();
            var game = new WinformGameController(board);
            game.Run();


        }
    }
}