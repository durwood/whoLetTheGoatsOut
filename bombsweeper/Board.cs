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
            }
            else
            {
                Expose(x, y);
            }
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
