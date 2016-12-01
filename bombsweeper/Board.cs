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
        public object SavedBoard { get; private set; }
        readonly BoardSerializer _serializer;

        public Board(int size)
        {
            _serializer = new BoardSerializer();
            _size = size;
            _cells = new Cell[size, size];
            for (var row = 0; row < size; ++row)
                for (var col = 0; col < size; ++col)
                    _cells[row, col] = new Cell();
            _gameState = GameState.InProgress;
        }

        public void Finish()
        {
            SavedBoard = _size > 3 ? _serializer.Serialize(this) : "";
        }

        public void AddBomb(int row, int col)
        {
            _cells[row, col].AddBomb();
            _numBombs++;
            PopulateAdjacencyCounts();
        }

        public Cell GetLosingBombCell(out int x, out int y)
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                    if (_cells[row, col].IsLoser)
                    {
                        x = col;
                        y = row;
                        return _cells[row, col];
                    }
            x = 0;
            y = 0;
            return null;
        }

        private void PopulateAdjacencyCounts()
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                {
                    if (_cells[row, col].HasBomb)
                        continue;
                    var bombsAroundCellCount = CountBombsAroundCell(row, col);
                    if (bombsAroundCellCount == 0)
                        _cells[row, col].ClearContents();
                    else
                        _cells[row, col].AddBombsAroundCellCount(bombsAroundCellCount);
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
                    if (_cells[row, col].HasBomb)
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
            return (row < 0) || (row > _size - 1) || (col < 0) || (col > _size - 1);
        }

        public override string ToString()
        {
            var cellStrings = _cells.Cast<Cell>().Select(cell => cell.ToString());
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
            var cell = _cells[row, col];
            cell.Reveal();
            if (cell.IsRevealed)
                if (cell.HasBomb)
                {
                    cell.MarkAsLoser();
                    _gameState = GameState.Lost;
                    RevealAllBombs();
                }
                else
                {
                    if (cell.IsEmpty())
                        RevealNeighbors(row, col);
                    if (NoFurtherCellsToReveal())
                        _gameState = GameState.Won;
                }
        }

        private bool NoFurtherCellsToReveal()
        {
            return !_cells.Cast<Cell>().Any(cell => !cell.IsRevealed && !cell.HasBomb);
        }

        private void RevealAllBombs()
        {
            foreach (var cell in _cells)
                if (!cell.IsMarked && cell.HasBomb)
                    cell.Reveal();
        }

        private void RevealNeighbors(int row0, int col0)
        {
            for (var row = row0 - 1; row <= row0 + 1; ++row)
                for (var col = col0 - 1; col <= col0 + 1; ++col)
                {
                    if (NotValidCell(row, col) || IsSameCell(row0, col0, row, col) || IsDiagonal(row0, col0, row, col))
                        continue;

                    var cell = _cells[row, col];
                    if (!cell.HasBomb && !cell.IsRevealed)
                    {
                        cell.Reveal();
                        if (cell.IsRevealed && cell.IsEmpty())
                            RevealNeighbors(row, col);
                    }
                }
        }

        private bool IsDiagonal(int row0, int col0, int row, int col)
        {
            return false;
            //return row0 != row && col0 != col;
        }

        public int GetSize()
        {
            return _size;
        }

        public void ToggleMark(int row, int col)
        {
            _cells[row, col].ToggleMark();
            var markedCells = from Cell item in _cells where item.IsMarked select item;
            _numMarked = markedCells.Count();
        }

        public int GetNumberOfUnmarkedBombs()
        {
            return Math.Max(_numBombs - _numMarked, 0);
        }
    }
}