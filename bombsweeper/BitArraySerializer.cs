using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace bombsweeper
{
    public class BitArraySerializer
    {
        public BitArray Deserialize(string input)
        {
            var byteArray = StringToByteArray(input);
            var sqrtLength = (int) Math.Sqrt(byteArray.Length*8);
            var size = sqrtLength*sqrtLength;
            return ByteArrayToBitArray(byteArray, size);
        }

        public string Serialize(BitArray bitArray)
        {
            if (bitArray.Length < 4 * 4)
                throw new ArgumentException("Cannot Serialize boards smaller than 4x4");
            byte[] byteArray = BitArrayToBytes(bitArray);
            var result = ByteArrayToString(byteArray);
            return result;
        }
        public BitArray ByteArrayToBitArray(byte[] myBytes, int size = 0)
        {
            var result = new BitArray(myBytes);
            if (size != 0 && size != result.Length)
            {
                result = Truncate(result, size);
            }
            return result;
        }

        private BitArray Truncate(BitArray input, int size)
        {
            var result = new BitArray(size);
            for (int index=0; index < size; ++index)
                result[index] = input[index];
            return result;
        }

        public byte[] BitArrayToBytes(BitArray myBA3)
        {
            var byteList = new List<byte>();
            byte aByte = 0;
            int bitIndex = 0;
            foreach (var bit in myBA3)
            {
                if ((bool)bit)
                    aByte = SetBit(aByte, bitIndex);
                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteList.Add(aByte);
                    aByte = 0;
                }
            }
            if (bitIndex != 0)
            {
                aByte <<= 7 - bitIndex;
                byteList.Add(aByte);
            }

            return byteList.ToArray();
        }

        public byte SetBit(byte aByte, int bitIndex)
        {
            byte mask = 1;
            mask <<= bitIndex;
            aByte |= mask;
            return aByte;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            var result = hex.Replace("-", "");
            return result;
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

    }
}
