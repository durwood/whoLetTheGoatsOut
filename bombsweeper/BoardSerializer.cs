using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace bombsweeper
{
    public class BoardSerializer
    {
        BitArraySerializer _bitArraySerializer;

        public BoardSerializer()
        {
            _bitArraySerializer = new BitArraySerializer();
        }

        public string Serialize(Board board)
        {
            BitArray bitArray = BoardToBitArray(board);
            return _bitArraySerializer.Serialize(bitArray);
        }

        public Board DeSerialize(string serializedBoard)
        {
            var bitArray = _bitArraySerializer.Deserialize(serializedBoard);
            var size = (int)(Math.Sqrt(bitArray.Length));
            var board = new Board(size);
            int linearIndex = 0;;
            for (int col = 0; col < size; ++col)
                for (int row = 0; row < size; ++row)
                    if (bitArray[linearIndex++])
                        board.AddBomb(col, row);
            return board;
        }


        private BitArray BoardToBitArray(Board board)
        {
            var size = board.GetSize() * board.GetSize();
            var bitArray = new BitArray(size, false);
            int index = 0;
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
