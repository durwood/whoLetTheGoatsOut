using System;
using System.Collections.Generic;
using System.Linq;

namespace bombsweeper
{
    public class BoardGenerator
    {
        private readonly IRandomGenerator _rng;
        private HashSet<Coordinate> _bombLocations;
        private int _size;
        private Board _board;

        public BoardGenerator(IRandomGenerator randomNumberGenerator)
        {
            _rng = randomNumberGenerator;
        }

        public Board GenerateBoard(int size, int numBombs)
        {
            _size = size;
            _board = new Board(size);
            _bombLocations = new HashSet<Coordinate>();
            for (var idx = 0; idx < numBombs; ++idx)
                AddBomb();
            return _board;
        }

        private void AddBomb()
        {
            var addAdjacent = _rng.NextDouble() < 0.33;
            if (addAdjacent && _bombLocations.Count > 0)
                AddAdjacentBomb();
            else
                AddRandomBomb();
        }

        private void AddRandomBomb()
        {
            Coordinate newBomb;
            do
            {
                newBomb.X = _rng.Next(0, _size);
                newBomb.Y = _rng.Next(0, _size);
            } while (_bombLocations.Contains(newBomb));
            AddBomb(newBomb);
        }

        private void AddBomb(Coordinate pos)
        {
            _bombLocations.Add(pos);
            _board.AddBomb(pos.X, pos.Y);
        }

        private void AddAdjacentBomb()
        {
            var bombIdx = _rng.Next(0, _bombLocations.Count);
            var existing = _bombLocations.ToList()[bombIdx];
            var neighbors = GetNeighbors(existing);
            var newBomb = neighbors[_rng.Next(0, neighbors.Count)];
            AddBomb(newBomb);
        }

        private List<Coordinate> GetNeighbors(Coordinate coord)
        {
            var neighbors = new List<Coordinate>();
            var colMin = Math.Max(0, coord.X - 1);
            var colMax = Math.Min(_size - 1, coord.X + 1);
            var rowMin = Math.Max(0, coord.Y - 1);
            var rowMax = Math.Max(_size - 1, coord.Y + 1);
            for (var col = colMin; col <= colMax; ++col)
                for (var row = rowMin; row <= rowMax; ++row)
                {
                    var pos = new Coordinate() {X=col, Y=row};
                 if (!_bombLocations.Contains(pos))
                        neighbors.Add(pos);
                }
            return neighbors;
        }
    }
}