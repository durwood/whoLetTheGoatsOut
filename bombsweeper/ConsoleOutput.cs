using System;

namespace bombsweeper
{
    public class ConsoleOutput : IDisplay
    {
        private const int LabelAllowance = 4;
        private const int _statusLine = 0;

        public void ShowResult(Board board)
        {
            if (board.GameWon())
                Console.WriteLine("Congratulations, you won!");
            else if (board.GameLost())
                Console.WriteLine("IsLoser.");
            else
                Console.WriteLine("Quitter.");
        }

        public void UpdateStatus(int elapsedSec, int numBombs)
        {
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine($"Bombs: {numBombs}  Elapsed Time: {elapsedSec}");
        }

        public virtual void Init()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }

        public const int BoardLine = 2;

        private void GetConsoleXCoordinate(ref int x)
        {
            x = LabelAllowance + 1 + x*2;
        }

        public void Display(Board board)
        {
            var size = board.GetSize();
            for (var row = 0; row < size; ++row)
                DisplayRow(board, row);
            DisplayFooter(size);
        }

        private void DisplayRow(Board board, int row)
        {
            var cells = board.GetRow(row);

            char rowLabel = (char)(65 + row);
            var lineStart = $"{rowLabel,LabelAllowance-1} ";
            Console.SetCursorPosition(0, BoardLine + row);
            Console.Write(lineStart);

            for (var col=0; col<cells.Length; col++)
                DisplayCell(col, row, cells[col]);

            Console.WriteLine();
        }

        private void DisplayCell(int col, int row, Cell cell)
        {
            Console.SetCursorPosition(LabelAllowance + col * 2, BoardLine + row);
            Console.Write(cell + " ");
        }

        private void DisplayFooter(int size)
        {
            if (size > 9)
                DisplayFooterTens(size);
            DisplayFooterOnes(size);
        }

        private  void DisplayFooterOnes(int size)
        {
            Console.Write($"{"",LabelAllowance}");
            for (var col = 0; col < size; ++col)
                Console.Write($"{(col + 1)%10} ");
            Console.WriteLine();
        }

        private  void DisplayFooterTens(int size)
        {
            Console.Write($"{"",LabelAllowance}");
            for (var col = 0; col < size; ++col)
                Console.Write($"{(col + 1)/10} ");
            Console.WriteLine();
        }

        private Cell GetLosingBombCell(Cell[,] cells, int size, out int x, out int y)
        {
            for (var row = 0; row < size; ++row)
                for (var col = 0; col < size; ++col)
                    if (cells[row, col].IsLoser)
                    {
                        x = col;
                        y = row;
                        GetConsoleXCoordinate(ref x);
                        return cells[row, col];
                    }
            x = 0;
            y = 0;
            return null;
        }

        public virtual void DisplayLose(Board board)
        {
            int x, y;
            var cell = GetLosingBombCell(board.GetCells(), board.GetSize(), out x, out y);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y + BoardLine);
            Console.Write(cell);
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, CalcCursorLine(board));
        }

        public static int CalcCursorLine(Board board)
        {
            return BoardLine + board.GetSize() + 2;
        }
    }
}