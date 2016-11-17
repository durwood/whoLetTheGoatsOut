using System;
using System.Collections;

namespace bombsweeper
{
    public class BoardSerializer
    {
        private readonly BitArraySerializer _bitArraySerializer;

        public BoardSerializer()
        {
            _bitArraySerializer = new BitArraySerializer();
        }

        public string Serialize(Board board)
        {
            var bitArray = BoardToBitArray(board);
            return _bitArraySerializer.Serialize(bitArray);
        }

        public Board DeSerialize(string serializedBoard)
        {
            var bitArray = _bitArraySerializer.Deserialize(serializedBoard);
            var size = (int) Math.Sqrt(bitArray.Length);
            var board = new Board(size);
            var linearIndex = 0;
            ;
            for (var col = 0; col < size; ++col)
                for (var row = 0; row < size; ++row)
                    if (bitArray[linearIndex++])
                        board.AddBomb(col, row);
            return board;
        }


        private BitArray BoardToBitArray(Board board)
        {
            var size = board.GetSize()*board.GetSize();
            var bitArray = new BitArray(size, false);
            var index = 0;
            foreach (var cell in board.GetCells())
            {
                if (cell.HasBomb)
                    bitArray.Set(index, true);
                ++index;
            }
            return bitArray;
        }
    }
}