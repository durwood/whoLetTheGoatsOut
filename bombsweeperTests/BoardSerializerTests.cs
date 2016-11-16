using System.Collections;
using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class BoardSerializerTests
    {
        [SetUp]
        public void SetUp()
        {
            _board = new Board(2);
            _testObj = new BoardSerializer();
        }

        private Board _board;
        private BoardSerializer _testObj;

        [Test]
        public void CanSerializeAndDeserializeDefaultBitArray()
        {
            var bitArray = new BitArray(8, false);
            var serialized = _testObj.SerializeBitArray(bitArray);
            var result = _testObj.DeserializeBitArray(serialized);
            Assert.That(bitArray, Is.EqualTo(result));
        }

        [Test]
        public void CanSerializeAndDeserializeAllTrueBitArray()
        {
            var expected = new BitArray(8, false);
            expected.Set(0, true);
            var serialized = _testObj.SerializeBitArray(expected);
            var result = _testObj.DeserializeBitArray(serialized);
            CollectionAssert.Contains(result, expected);
        }

        [Test]
        public void CanSerializeAndDeserializeAllSteps()
        {
            var bitArray = new BitArray(8, true) {[0] = false};
            //bitArray[9] = false;
            var byteArray = _testObj.BitArrayToBytes(bitArray);
            var serialized = BoardSerializer.ByteArrayToString(byteArray);
            var byteArray2 = BoardSerializer.StringToByteArray(serialized);
            Assert.That(byteArray2, Is.EqualTo(byteArray));
            var bitArray2 = _testObj.ByteArrayToBitArray(byteArray);
            Assert.That(bitArray, Is.EqualTo(bitArray2));
        }

        [Test]
        public void ByteArrayToBitArrayTests()
        {
            byte[] bytes = { (byte)1} ;
            var expected = new BitArray(8, false);
            expected.Set(0, true);
            var bitArray = _testObj.ByteArrayToBitArray(bytes);
            Assert.That(bitArray, Is.EqualTo(expected));

            bytes = new byte[] { (byte)128 };
            expected = new BitArray(8, false);
            expected.Set(7, true);
            bitArray = _testObj.ByteArrayToBitArray(bytes);
            Assert.That(bitArray, Is.EqualTo(expected));

            bytes = new byte[] { (byte)128, (byte) 1 };
            expected = new BitArray(16, false);
            expected.Set(7, true);
            expected.Set(8, true);
            bitArray = _testObj.ByteArrayToBitArray(bytes);
            Assert.That(bitArray, Is.EqualTo(expected));

            bytes = new byte[] { (byte)128, (byte)129 };
            expected = new BitArray(16, false);
            expected.Set(7, true);
            expected.Set(8, true);
            expected.Set(15, true);
            bitArray = _testObj.ByteArrayToBitArray(bytes);
            Assert.That(bitArray, Is.EqualTo(expected));
        }

        [Test]
        public void SetBitTests()
        {
            var result = _testObj.SetBit(0, 0);
            Assert.That(result, Is.EqualTo(1));

            result = _testObj.SetBit(0, 1);
            Assert.That(result, Is.EqualTo(2));

            result = _testObj.SetBit(0, 2);
            Assert.That(result, Is.EqualTo(4));

            result = _testObj.SetBit(0, 3);
            Assert.That(result, Is.EqualTo(8));

            result = _testObj.SetBit(0, 4);
            Assert.That(result, Is.EqualTo(16));

            result = _testObj.SetBit(0, 5);
            Assert.That(result, Is.EqualTo(32));

            result = _testObj.SetBit(0, 6);
            Assert.That(result, Is.EqualTo(64));

            result = _testObj.SetBit(0, 7);
            Assert.That(result, Is.EqualTo(128));

            result = _testObj.SetBit(1, 0);
            Assert.That(result, Is.EqualTo(1));

            result = _testObj.SetBit(128, 0);
            Assert.That(result, Is.EqualTo(129));
        }

        [Test]
        public void BitArrayToByteArrayTests()
        {
            byte[] expected = { (byte)1 };
            var bits = new BitArray(8, false);
            bits.Set(0, true);
            var result = _testObj.BitArrayToBytes(bits);
            Assert.That(result, Is.EqualTo(expected));

            expected = new byte[] { (byte)128 };
            bits = new BitArray(8, false);
            bits.Set(7, true);
            result = _testObj.BitArrayToBytes(bits);
            Assert.That(result, Is.EqualTo(expected));

            expected = new byte[] { (byte)128, (byte)1 };
            bits = new BitArray(16, false);
            bits.Set(7, true);
            bits.Set(8, true);
            result = _testObj.BitArrayToBytes(bits);
            Assert.That(result, Is.EqualTo(expected));

            expected = new byte[] { (byte)128, (byte)129 };
            bits = new BitArray(16, false);
            bits.Set(7, true);
            bits.Set(8, true);
            bits.Set(15, true);
            result = _testObj.BitArrayToBytes(bits);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ByteToBitArray()
        {
            var bitArray = _testObj.ByteArrayToBitArray(new byte[] {1});
            Assert.That(bitArray.Length, Is.EqualTo(8));
            Assert.IsTrue(bitArray[0]);
            Assert.IsFalse(bitArray[7]);

            bitArray = _testObj.ByteArrayToBitArray(new byte[] { 128 });
            Assert.That(bitArray.Length, Is.EqualTo(8));
            Assert.IsFalse(bitArray[0]);
            Assert.IsTrue(bitArray[7]);
        }

        [Test]
        public void BytesToBitArray()
        {
            var twoBytes = new byte[] { 1, 128 };
            var bitArray = _testObj.ByteArrayToBitArray(twoBytes);
            Assert.That(bitArray.Length, Is.EqualTo(16));
            Assert.IsTrue(bitArray[0]);
            Assert.IsTrue(bitArray[15]);

            var byteBack = _testObj.BitArrayToBytes(bitArray);
        }

    }
}