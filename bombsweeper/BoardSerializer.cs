using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace bombsweeper
{
    public class BoardSerializer
    {
        int _size;

        public BoardSerializer()
        {
        }

        public string Serialize(Board board)
        {
            BitArray bitArray = BoardToBitArray(board);
            return SerializeBitArray(bitArray);
        }

        public Board DeSerialize(string serializedBoard)
        {
            var bitArray = DeserializeBitArray(serializedBoard);
            var size = (int)(Math.Sqrt(bitArray.Length));
            // TODO: AddBombs
            return new Board(size);
        }

        public BitArray DeserializeBitArray(string input)
        {
            var byteArray = StringToByteArray(input);
            return ByteArrayToBitArray(byteArray);
        }

        public string SerializeBitArray(BitArray bitArray)
        {
            byte[] byteArray = BitArrayToBytes(bitArray);
            var result = ByteArrayToString(byteArray);
            return result;
        }

        private BitArray BoardToBitArray(Board board)
        {
            _size = board.GetSize() * board.GetSize();
            var bitArray = new BitArray(_size, false);
            int index = 0;
            foreach (var cell in board.GetCells())
            {
                if (cell.HasBomb)
                    bitArray.Set(index, true);
                ++index;
            }
            return bitArray;
        }

        private BitArray ByteArrayToBitArray(byte[] myBytes)
        {
            return new BitArray(myBytes);
        }

        private byte[] BitArrayToBytes(BitArray myBA3)
        {
            var byteList = new List<byte>();
            byte aByte = 0;
            int numShifts = 0;
            foreach (var bit in myBA3)
            {
                if ((bool)bit)
                    aByte ^= 1;
                aByte <<= 1;
                numShifts++;
                if (numShifts == 8)
                {
                    numShifts = 0;
                    byteList.Add(aByte);
                    aByte = 0;
                }
            }
            if (numShifts != 0)
            {
                aByte <<= 7 - numShifts;
                byteList.Add(aByte);
            }

            return byteList.ToArray();
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            var result = hex.Replace("-", "");
            return result;
        }

        private static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

    }
}
