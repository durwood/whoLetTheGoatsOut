using System;
using System.Text;
using System.Collections.Generic;

namespace bombsweeper
{
    enum GameState { InProgress, Won, Lost };


    public class Board
    {
        private GameState _gameState;
        readonly int _size;
        readonly Cell[,] _cells;

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
            //PopulateAdjacencyCounts();
        }

        private void PopulateAdjacencyCounts()
        {
            for (int row = 0; row < _size; ++row)
                for (int col = 0; col < _size; ++col)
                {
                    if (_cells[col, row].HasBomb())
                        continue;
                var adjacentBombs = CountAdjacentBombs(col, row);
                    if (adjacentBombs == 0)
                        _cells[col, row].SetContent(Cell.Empty);
                    else
                        _cells[col, row].SetContent(adjacentBombs.ToString()[0]);
                }
        }

        private int CountAdjacentBombs(int x0, int y0)
        {
            int count = 0;
            for (int x = x0 - 1; x <= x0 + 1; ++x)
            {
                for (int y = y0 - 1; y <= y0 + 1; ++y)
                {
                    if (x < 0 || x > _size - 1)
                        continue;
                    if (y < 0 || y > _size - 1)
                        continue;
                    if (x == x0 && y == y0)
                        continue;
                    if (_cells[x, y].HasBomb())
                        count++;
                }
            }
            return count;
        }

        public string Display()
        {
            StringBuilder sb = new StringBuilder();
            for (var row = 0; row < _size; ++row)
                DisplayRow(sb, row);
            DisplayFooter(sb);
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

        public void Reveal(int x, int y)
        {
            var content = _cells[x, y].Reveal();
            if (content == Cell.Bomb)
            {
                _gameState = GameState.Lost;
                RevealBoard();
            }
            else
            {
                Expose(x, y);
            }
        }

        void RevealBoard()
        {
            foreach (var cell in _cells)
                cell.Reveal();
        }

        private void Expose(int x0, int y0)
        {
            for (int x = x0 - 1; x <= x0 + 1; ++x)
            {
                for (int y = y0 - 1; y <= y0 + 1; ++y)
                {
                    if (x < 0 || x > _size - 1)
                        continue;
                    if (y < 0 || y > _size - 1)
                        continue;
                    if (x == x0 && y == y0)
                        continue;
                    if (_cells[x, y].HasBomb())
                        continue;
                    if (!_cells[x, y].IsRevealed)
                    {
                        _cells[x, y].Reveal();
                        Expose(x, y);
                    }
                }
            }
        }

        void DisplayFooter(StringBuilder sb)
        {
            sb.Append($"  ");
            for (var col = 0; col < _size; ++col)
                sb.Append($"{col+1} ");
            sb.AppendLine();
        }

        private void DisplayRow(StringBuilder sb, int row)
        {
            sb.Append($"{row + 1} ");
            for (var col = 0; col < _size; ++col)
                sb.Append($"{_cells[col, row].Display()} ");
            sb.AppendLine();
        }

   }

}
