using System;
using System.Linq;

namespace bombsweeper
{
    public class BoardConsoleView
    {
        private const int LabelAllowance = 3;
        private readonly int _boardLine;
        private readonly Board _boardModel;
        private readonly Cell[,] _cells;
        private readonly int _size;

        public BoardConsoleView(Board boardModel, int boardLine)
        {
            _boardModel = boardModel;
            _size = boardModel.GetSize();
            _boardLine = boardLine;
            _cells = boardModel.GetCells();
        }

        public void DisplayBoard()
        {
            for (var row = 0; row < _size; ++row)
                DisplayRow(row);
            DisplayFooter();
            if (_boardModel.GameLost())
                DisplayLosingBomb();
        }

        private void DisplayRow(int row)
        {
            Console.Write($"{row + 1,LabelAllowance}");
            var cells = GetRow(row);
            foreach (var cell in cells)
            {
                Console.Write(" ");
                var cellConsoleView = new CellConsoleView(cell);
                cellConsoleView.DisplayCell();
            }
            Console.WriteLine("");
        }

        private void DisplayFooter()
        {
            if (_size > 9)
                DisplayFooterTens();
            DisplayFooterOnes();
        }

        private void DisplayFooterOnes()
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < _size; ++col)
                Console.Write($"{(col + 1)%10} ");
            Console.WriteLine();
        }

        private void DisplayFooterTens()
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < _size; ++col)
                Console.Write($"{(col + 1)/10} ");
            Console.WriteLine();
        }

        private static void GetConsoleXCoordinate(ref int x)
        {
            x = LabelAllowance + 1 + x*2;
        }

        private Cell[] GetRow(int row)
        {
            var offset = row*_size;
            return _cells.Cast<Cell>().Skip(offset).Take(_size).ToArray();
        }

        private void DisplayLosingBomb()
        {
            Console.SetCursorPosition(0, _boardLine);
            if (_boardModel.GameLost())
            {
                int x, y;
                var cell = _boardModel.GetLosingBombCell(out x, out y);
                GetConsoleXCoordinate(ref x);
                var savedColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y + _boardLine);

                var cellConsoleView = new CellConsoleView(cell);
                cellConsoleView.DisplayCell();

                Console.BackgroundColor = savedColor;
            }
        }
    }
}