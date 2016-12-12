using System;
using System.Linq;

namespace bombsweeper
{
    internal enum GameState
    {
        InProgress,
        Won,
        Lost,
        Quitted
    }

    public class Board
    {
        private GameState _gameState;
        private int _numBombs;
        private int _numMarked;

        public Board(int size)
        {
            Size = size;
            Cells = new Cell[size, size];
            for (var row = 0; row < size; ++row)
                for (var col = 0; col < size; ++col)
                    Cells[row, col] = new Cell();
            _gameState = GameState.InProgress;
        }

        public int Size { get; }

        public Cell[,] Cells { get; }

        public void AddBomb(int row, int col)
        {
            Cells[row, col].AddBomb();
            _numBombs++;
            PopulateAdjacencyCounts();
        }

        private void PopulateAdjacencyCounts()
        {
            for (var row = 0; row < Size; ++row)
                for (var col = 0; col < Size; ++col)
                {
                    if (Cells[row, col].HasBomb())
                        continue;
                    var bombsAroundCellCount = CountBombsAroundCell(row, col);
                    if (bombsAroundCellCount == 0)
                        Cells[row, col].ClearContents();
                    else
                        Cells[row, col].AddBombsAroundCellCount(bombsAroundCellCount);
                }
        }

        public Cell[,] GetCells()
        {
            return Cells;
        }

        private int CountBombsAroundCell(int row0, int col0)
        {
            var count = 0;
            for (var row = row0 - 1; row <= row0 + 1; ++row)
                for (var col = col0 - 1; col <= col0 + 1; ++col)
                {
                    if (NotValidCell(row, col) || IsSameCell(row0, col0, row, col))
                        continue;
                    if (Cells[row, col].HasBomb())
                        count++;
                }
            return count;
        }

        private static bool IsSameCell(int row0, int col0, int row1, int col1)
        {
            return (col1 == col0) && (row1 == row0);
        }

        private bool NotValidCell(int row, int col)
        {
            return (row < 0) || (row > Size - 1) || (col < 0) || (col > Size - 1);
        }

        public Cell GetLosingBombCell(out int x, out int y)
        {
            for (var row = 0; row < Size; ++row)
                for (var col = 0; col < Size; ++col)
                    if (Cells[row, col].IsLoser)
                    {
                        x = col;
                        y = row;
                        GetConsoleXCoordinate(ref x);
                        return Cells[row, col];
                    }
            x = 0;
            y = 0;
            return null;
        }

        private static void GetConsoleXCoordinate(ref int x)
        {
            x = ConsoleView.LabelAllowance + 1 + x*2;
        }

        public override string ToString()
        {
            var cellStrings = Cells.Cast<Cell>().Select(cell => cell.ToString());
            return string.Join(" ", cellStrings);
        }

        public bool GameWon()
        {
            return _gameState == GameState.Won;
        }

        public bool GameLost()
        {
            return _gameState == GameState.Lost;
        }

        public bool GameInProgress()
        {
            return _gameState == GameState.InProgress;
        }

        public void QuitGame()
        {
            _gameState = GameState.Quitted;
        }

        public void Reveal(int row, int col)
        {
            var cell = Cells[row, col];
            var content = cell.Reveal();
            if (cell.IsRevealed)
                if (content == Cell.Bomb)
                {
                    cell.MarkAsLoser();
                    _gameState = GameState.Lost;
                    RevealAllBombs();
                }
                else
                {
                    RevealNeighbors(row, col);
                    if (NoFurtherCellsToReveal())
                        _gameState = GameState.Won;
                }
        }

        private bool NoFurtherCellsToReveal()
        {
            return !Cells.Cast<Cell>().Any(cell => !cell.IsRevealed && !cell.HasBomb());
        }

        private void RevealAllBombs()
        {
            foreach (var cell in Cells)
                if (!cell.IsMarked && cell.HasBomb())
                    cell.Reveal();
        }

        private void RevealNeighbors(int row0, int col0)
        {
            for (var row = row0 - 1; row <= row0 + 1; ++row)
                for (var col = col0 - 1; col <= col0 + 1; ++col)
                {
                    if (NotValidCell(row, col) || IsSameCell(row0, col0, row, col))
                        continue;

                    var cell = Cells[row, col];
                    if (!cell.HasBomb() && !cell.IsRevealed)
                    {
                        var content = cell.Reveal();
                        if (cell.IsRevealed && (content == Cell.Empty))
                            RevealNeighbors(row, col);
                    }
                }
        }

        public int GetSize()
        {
            return Size;
        }

        public void ToggleMark(int row, int col)
        {
            Cells[row, col].ToggleMark();
            var markedCells = from Cell item in Cells where item.IsMarked select item;
            _numMarked = markedCells.Count();
        }

        public int GetNumberOfUnmarkedBombs()
        {
            return Math.Max(_numBombs - _numMarked, 0);
        }
    }
}