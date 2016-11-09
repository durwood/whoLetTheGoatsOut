using System;

namespace bombsweeper
{
    internal class ConsoleUi : IUi
    {
        private readonly int _boardLine;
        private readonly int _cursorLine;
        private readonly int _statusLine;

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
            board.Display();
        }

        public void UpdateStatus(int bombs, int sec)
        {
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine($"Bombs: {bombs}  Elapsed Time: {sec}");
        }

    }
}