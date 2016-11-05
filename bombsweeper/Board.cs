using System;
using System.Linq;
using System.Text;

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
        private const int LabelAllowance = 3;
        private readonly Cell[,] _cells;
        private readonly int _size;
        private GameState _gameState;
        private int _numBombs;
        private int _numMarked;

        public Board(int size)
        {
            _size = size;
            _cells = new Cell[_size, _size];
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                    _cells[row, col] = new Cell();
            _gameState = GameState.InProgress;
        }

        public void AddBomb(int x, int y)
        {
            _cells[y, x].AddBomb();
            _numBombs++;
            PopulateAdjacencyCounts();
        }

        private void PopulateAdjacencyCounts()
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                {
                    if (_cells[row, col].HasBomb())
                        continue;
                    var adjacentBombs = CountAdjacentBombs(col, row);
                    if (adjacentBombs == 0)
                        _cells[row, col].ClearContents();
                    else
                        _cells[row, col].AddAdjacencyNumber(adjacentBombs);
                }
        }

        private int CountAdjacentBombs(int x0, int y0)
        {
            var count = 0;
            for (var x = x0 - 1; x <= x0 + 1; ++x)
                for (var y = y0 - 1; y <= y0 + 1; ++y)
                {
                    if (IsValidCell(x, y))
                        continue;
                    if ((x == x0) && (y == y0))
                        continue;
                    if (_cells[y, x].HasBomb())
                        count++;
                }
            return count;
        }

        private bool IsValidCell(int x, int y)
        {
            return (x < 0) || (x > _size - 1) || (y < 0) || (y > _size - 1);
        }

        public Cell GetLosingBombCell(out int x, out int y)
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                    if (_cells[row, col].Loser)
                    {
                        x = col;
                        y = row;
                        GetConsoleXCoordinate(ref x);
                        return _cells[row, col];
                    }
            x = 0;
            y = 0;
            return null;
        }

        private void GetConsoleXCoordinate(ref int x)
        {
            x = LabelAllowance + 1 + x*2;
        }

        public void Display()
        {
            for (var row = 0; row < _size; ++row)
            {
                var rowString = string.Join(" ", GetRow(row).Select(c => c.ToString()));
                var line = string.Join(" ", $"{row + 1,LabelAllowance}", $"{rowString}");
                Console.WriteLine(line);
            }
            DisplayFooter();
        }

        public Cell[] GetRow(int row)
        {
            var offset = row*_size;
            return _cells.Cast<Cell>().Skip(offset).Take(_size).ToArray();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var row = 0; row < _size; ++row)
            {
                for (var col = 0; col < _size; ++col)
                    sb.Append($"{_cells[row, col]} ");
                sb.AppendLine();
            }
            return sb.ToString();
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

        public void Reveal(int x, int y)
        {
            if (_cells[y, x].IsMarked)
                return;

            var content = _cells[y, x].Reveal();
            if (content == Cell.Bomb)
            {
                _cells[y, x].MarkAsLoser();
                _gameState = GameState.Lost;
                RevealAllBombs();
            }
            else
            {
                Expose(x, y);
                foreach (var cell in _cells)
                    if (!cell.IsRevealed && !cell.HasBomb())
                        return;
                _gameState = GameState.Won;
            }
        }

        private void RevealAllBombs()
        {
            foreach (var cell in _cells)
                if (!cell.IsMarked && cell.HasBomb())
                    cell.Reveal();
        }

        private void Expose(int x0, int y0)
        {
            for (var x = x0 - 1; x <= x0 + 1; ++x)
                for (var y = y0 - 1; y <= y0 + 1; ++y)
                {
                    if ((x < 0) || (x > _size - 1))
                        continue;
                    if ((y < 0) || (y > _size - 1))
                        continue;
                    if ((x == x0) && (y == y0))
                        continue;
                    if (_cells[y, x].HasBomb())
                        continue;
                    if (!_cells[y, x].IsRevealed)
                    {
                        var content = _cells[y, x].Reveal();
                        if (content == Cell.Empty)
                            Expose(x, y);
                    }
                }
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

        public int GetSize()
        {
            return _size;
        }

        public void ToggleMark(int x, int y)
        {
            _cells[y, x].ToggleMark();
            var markedCells = from Cell item in _cells where item.IsMarked select item;
            _numMarked = markedCells.Count();
        }

        public int GetNumberOfUnmarkedBombs()
        {
            return Math.Max(_numBombs - _numMarked, 0);
        }
    }
}