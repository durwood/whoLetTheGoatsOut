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
        private readonly Cell[,] _cells;
        private readonly int _size;
        private GameState _gameState;
        private int _numBombs;
        private int _numMarked;

        public Board(int size)
        {
            _size = size;
            _cells = new Cell[size, size];
            for (var row = 0; row < size; ++row)
                for (var col = 0; col < size; ++col)
                    GetCells()[row, col] = new Cell();
            _gameState = GameState.InProgress;
        }

        public void AddBomb(int row, int col)
        {
            GetCells()[row, col].AddBomb();
            _numBombs++;
            PopulateAdjacencyCounts();
        }

        private void PopulateAdjacencyCounts()
        {
            for (var row = 0; row < GetSize(); ++row)
                for (var col = 0; col < GetSize(); ++col)
                {
                    if (GetCells()[row, col].HasBomb())
                        continue;
                    var bombsAroundCellCount = CountBombsAroundCell(row, col);
                    if (bombsAroundCellCount == 0)
                        GetCells()[row, col].ClearContents();
                    else
                        GetCells()[row, col].AddBombsAroundCellCount(bombsAroundCellCount);
                }
        }

        public Cell[,] GetCells()
        {
            return _cells;
        }

        private int CountBombsAroundCell(int row0, int col0)
        {
            var count = 0;
            for (var row = row0 - 1; row <= row0 + 1; ++row)
                for (var col = col0 - 1; col <= col0 + 1; ++col)
                {
                    if (NotValidCell(row, col) || IsSameCell(row0, col0, row, col))
                        continue;
                    if (GetCells()[row, col].HasBomb())
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
            return (row < 0) || (row > GetSize() - 1) || (col < 0) || (col > GetSize() - 1);
        }

        public override string ToString()
        {
            var cellStrings = GetCells().Cast<Cell>().Select(cell => cell.ToString());
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
            var cell = GetCells()[row, col];
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
            return !GetCells().Cast<Cell>().Any(cell => !cell.IsRevealed && !cell.HasBomb());
        }

        private void RevealAllBombs()
        {
            foreach (var cell in GetCells())
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

                    var cell = GetCells()[row, col];
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
            return _size;
        }

        public void ToggleMark(int row, int col)
        {
            GetCells()[row, col].ToggleMark();
            var markedCells = from Cell item in GetCells() where item.IsMarked select item;
            _numMarked = markedCells.Count();
        }

        public int GetNumberOfUnmarkedBombs()
        {
            return Math.Max(_numBombs - _numMarked, 0);
        }
    }
}