using System;

namespace bombsweeper
{
    public class ConsoleView : IView
    {
        private const int LabelAllowance = 3;
        private readonly int _boardLine;
        private readonly int _statusLine;
        private readonly int _cursorLine;

        public ConsoleView(Board board)
        {
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
            Console.CursorVisible = false;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void DisplayBoard(Board board)
        {
            Console.SetCursorPosition(0, _boardLine);
            board.Display(this);
            if (board.GameLost())
            {
                int x, y;
                var cell = board.GetLosingBombCell(out x, out y);
                var savedColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y + _boardLine);
                Console.Write(cell);
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, _cursorLine);
            }
        }

        public void Quit()
        {
            Console.WriteLine("Quitter.");
        }

        public void Lose()
        {
            Console.WriteLine("IsLoser.");
        }

        public void Win()
        {
            Console.WriteLine("Congratulations, you won!");
        }

        public void StatusDisplay(int numBombs, int elapsedSec)
        {
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine($"Bombs: {numBombs}  Elapsed Time: {elapsedSec}");
        }

        public void DisplayFooter(Board board)
        {
            var size = board.GetSize();
            if (size > 9)
                DisplayFooterTens(size);
            DisplayFooterOnes(size);
        }

        public void DisplayRow(string line)
        {
            Console.WriteLine(line);
        }

        public int GetCursorPosition()
        {
            return _cursorLine;
        }

        private static void DisplayFooterOnes(int size)
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < size; ++col)
                Console.Write($"{(col + 1)%10} ");
            Console.WriteLine();
        }

        private static void DisplayFooterTens(int size)
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < size; ++col)
                Console.Write($"{(col + 1)/10} ");
            Console.WriteLine();
        }
    }
}