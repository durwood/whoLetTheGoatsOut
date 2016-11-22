using System;
using System.Collections.Generic;
using System.Linq;

namespace bombsweeper
{
    public class BoardGenerator
    {
        private readonly IRandomGenerator _rng;
        private Board _board;

        public BoardGenerator(IRandomGenerator randomNumberGenerator)
        {
            _rng = randomNumberGenerator;
        }

        public Board GenerateBoard(int size, int numBombs)
        {
            _board = new Board(size);
            for (var idx = 0; idx < numBombs; ++idx)
                AddBomb();
            return _board;
        }

        private void AddBomb()
        {
            var diceRoll = _rng.NextDouble();
            var numAdjacent = (diceRoll < 0.15) ? 2 : (diceRoll < 0.30) ? 1 : 0;
            if (numAdjacent == 2 && AddAdjacentBomb(2))
                return;
            if (numAdjacent == 1 && AddAdjacentBomb(1))
                return;
            AddAdjacentBomb(0);
        }

        private bool AddAdjacentBomb(int numAdjacent)
        {
            var matching = (from Cell item in _board.GetCells() where item.NeighboringBombCount == numAdjacent select item).ToArray();
            if (matching.Length > 0)
            {
                var bombIdx = _rng.Next(0, matching.Length);
                var rowColTuple = GetCellPosition(matching[bombIdx]);
                _board.AddBomb(rowColTuple.Item1, rowColTuple.Item2);
                return true;
            }
            return false;
        }

        private Tuple<int, int> GetCellPosition(Cell cell)
        {
            for (var row = 0; row < _board.GetSize(); ++row)
                for (var col = 0; col < _board.GetSize(); ++col)
                    if (_board.GetCells()[row, col] == cell)
                    {
                        var result = new Tuple<int, int>(row, col);
                        return result;
                    }
            return null;
        }


        public static Board GetStandardBoard()
        {
            var board = Build9();
            return board;
        }

        private static Board Build9()
        {
            var board = new Board(9);
            board.AddBomb(1, 2);
            board.AddBomb(2, 1);
            board.AddBomb(2, 7);
            board.AddBomb(6, 6);
            board.AddBomb(3, 8);
            board.AddBomb(4, 6);
            board.AddBomb(4, 7);
            board.AddBomb(5, 3);
            board.AddBomb(7, 0);
            board.AddBomb(8, 0);
            return board;
        }
    }

}