using System;

namespace bombsweeper
{
    public class ConsoleView : IView
    {
        private Board _board;
        private int _boardLine;
        private int _cursorLine;
        private int _statusLine;

        public void Initialize()
        {
            Console.CursorVisible = false;
        }

        public void SetBoard(Board board)
        {
            _board = board;
            _boardLine = 2;
            _statusLine = 0;
            _cursorLine = _boardLine + board.GetSize() + 2;
        }

        public void DisplayBoard()
        {
            Console.SetCursorPosition(0, _boardLine);
            _board.Display();
            if (_board.GameLost())
            {
                int x, y;
                var cell = _board.GetLosingBombCell(out x, out y);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y + _boardLine);
                Console.Write(cell);
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, _cursorLine);
            }
        }


        public void UpdateStatusDisplay(int elapsedSec)
        {
            var numBombs = _board.GetNumberOfUnmarkedBombs();
            var cursorTop = Console.CursorTop;
            var cursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine($"Bombs: {numBombs}  Elapsed Time: {elapsedSec}");
            //This is weird and we should something? test it? something?
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        public void Lose()
        {
            Console.WriteLine("IsLoser.");
        }

        public void Quit()
        {
            Console.WriteLine("Quitter.");
        }

        public void Win()
        {
            Console.WriteLine("Congratulations, you won!");
        }
    }
}