using System;
using System.Linq;

namespace bombsweeper
{
    internal class ConsoleUi : IUi
    {
        private readonly int _boardLine;
        private readonly int _cursorLine;
        private readonly int _statusLine;
        private const int LabelAllowance = 3;

        internal ConsoleUi(int boardSize)
        {
            Console.CursorVisible = false;
            Console.Clear();
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + boardSize + 2;
        }

        public void HighlightLosingCell(int x, int y, Cell cell)
        {
            var savedColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y + _boardLine);
            Console.Write(cell);
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, _cursorLine);
        }

        public void Display(Board board)
        {
            Console.SetCursorPosition(0, _boardLine);
            board.Display(this);
        }

        public void UpdateStatus(int bombs, int sec)
        {
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine($"Bombs: {bombs}  Elapsed Time: {sec}");
        }

        public void UpdateRow(int row, Cell[] rowOfCells)
        {
            var rowString = string.Join(" ", rowOfCells.Select(c => c.ToString()));
            var line = string.Join(" ", $"{row + 1,LabelAllowance}", $"{rowString}");
            Console.WriteLine(line);
        }

        public void DisplayFooter(int _size)
        {
            if (_size > 9)
                DisplayFooterTens(_size);
            DisplayFooterOnes(_size);
        }

        private void DisplayFooterOnes(int _size)
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < _size; ++col)
                Console.Write($"{(col + 1) % 10} ");
            Console.WriteLine();
        }

        private void DisplayFooterTens(int _size)
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < _size; ++col)
                Console.Write($"{(col + 1) / 10} ");
            Console.WriteLine();
        }
    }
}