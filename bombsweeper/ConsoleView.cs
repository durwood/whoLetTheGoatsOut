using System;
using System.Linq;

namespace bombsweeper
{
    public class ConsoleView : IView
    {
        public const int LabelAllowance = 3;
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
            for (var row = 0; row < _board.Size; ++row)
                DisplayRow(row, _board);
            DisplayFooter(_board);

            if (_board.GameLost())
            {
                int x, y;
                var cell = GetLosingBombCell(out x, out y, _board);
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

        public void Clear()
        {
            Console.Clear();
        }

        public void Win()
        {
            Console.WriteLine("Congratulations, you won!");
        }

        private Cell[] GetRow(int row, Board board)
        {
            var offset = row*board.Size;
            return board.GetCells().Cast<Cell>().Skip(offset).Take(board.Size).ToArray();
        }

        private void DisplayRow(int row, Board board)
        {
            var rowLabel = (char) (65 + row);
            var rowString = string.Join(" ", GetRow(row, board).Select(c => c.ToString()));
            var line = string.Join(" ", $"{rowLabel,LabelAllowance}", $"{rowString}");
            Console.WriteLine(line);
        }

        private void DisplayFooterOnes(Board board)
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < board.Size; ++col)
                Console.Write($"{(col + 1)%10} ");
            Console.WriteLine();
        }

        private void DisplayFooterTens(Board board)
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < board.Size; ++col)
                Console.Write($"{(col + 1)/10} ");
            Console.WriteLine();
        }

        private void DisplayFooter(Board board)
        {
            if (board.Size > 9)
                DisplayFooterTens(board);
            DisplayFooterOnes(board);
        }

        private Cell GetLosingBombCell(out int x, out int y, Board board)
        {
            for (var row = 0; row < board.Size; ++row)
                for (var col = 0; col < board.Size; ++col)
                    if (board.GetCells()[row, col].IsLoser)
                    {
                        x = col;
                        y = row;
                        GetConsoleXCoordinate(ref x);
                        return board.GetCells()[row, col];
                    }
            x = 0;
            y = 0;
            return null;
        }

        private void GetConsoleXCoordinate(ref int x)
        {
            x = LabelAllowance + 1 + x*2;
        }
    }
}