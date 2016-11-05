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
                    _cells[col, row] = new Cell();
            _gameState = GameState.InProgress;
        }

        public void AddBomb(int x, int y)
        {
            _cells[x, y].AddBomb();
            _numBombs++;
            PopulateAdjacencyCounts();
        }

        private void PopulateAdjacencyCounts()
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                {
                    if (_cells[col, row].HasBomb())
                        continue;
                    var adjacentBombs = CountAdjacentBombs(col, row);
                    if (adjacentBombs == 0)
                        _cells[col, row].ClearContents();
                    else
                        _cells[col, row].AddAdjacencyNumber(adjacentBombs);
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
                    if (_cells[x, y].HasBomb())
                        count++;
                }
            return count;
        }

        private bool IsValidCell(int x, int y)
        {
            return (x < 0) || (x > _size - 1) || (y < 0) || (y > _size - 1);
        }

        public string Display(bool showLabels = false)
        {
            return showLabels ? DisplayWithLabels() : DisplayWithoutLabels();
        }

        public Cell GetLosingBombCell(out int x, out int y)
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                    if (_cells[col, row].Loser)
                    {
                        x = col;
                        y = row;
                        GetConsoleCoordinate(ref x, ref y);
                        return _cells[col, row];
                    }
            x = 0;
            y = 0;
            return null;
        }

        private void GetConsoleCoordinate(ref int x, ref int y)
        {
            x = LabelAllowance + 1 + x*2;
        }

        private string DisplayWithLabels()
        {
            var sb = new StringBuilder();
            for (var row = 0; row < _size; ++row)
                sb.AppendLine($"{row + 1,LabelAllowance} {DisplayRow(row)}");
            sb.AppendLine(DisplayFooter());
            return sb.ToString();
        }

        private string DisplayWithoutLabels()
        {
            var sb = new StringBuilder();
            for (var row = 0; row < _size; ++row)
                sb.AppendLine(DisplayRow(row));
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
            if (_cells[x, y].IsMarked)
                return;

            var content = _cells[x, y].Reveal();
            if (content == Cell.Bomb)
            {
                _cells[x, y].MarkAsLoser();
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
                    if (_cells[x, y].HasBomb())
                        continue;
                    if (!_cells[x, y].IsRevealed)
                    {
                        var content = _cells[x, y].Reveal();
                        if (content == Cell.Empty)
                            Expose(x, y);
                    }
                }
        }

        private string DisplayFooter()
        {
            var sb = new StringBuilder();
            if (_size > 9)
            {
                sb.Append($"{"",LabelAllowance + 1}");
                for (var col = 0; col < _size; ++col)
                    sb.Append($"{(col + 1)/10} ");
                sb.AppendLine();
            }
            sb.Append($"{"",LabelAllowance + 1}");
            for (var col = 0; col < _size; ++col)
                sb.Append($"{(col + 1)%10} ");
            sb.AppendLine();
            return sb.ToString();
        }

        public string DisplayRow(int row)
        {
            var sb = new StringBuilder();
            for (var col = 0; col < _size; ++col)
                sb.Append($"{_cells[col, row]} ");
            return sb.ToString();
        }

        public int GetSize()
        {
            return _size;
        }

        public void ToggleMark(int x, int y)
        {
            _cells[x, y].ToggleMark();
            _numMarked = (from Cell item in _cells where item.IsMarked select item).Count();
        }

        public int GetNumberOfUnmarkedBombs()
        {
            return Math.Max(_numBombs - _numMarked, 0);
        }
    }
}