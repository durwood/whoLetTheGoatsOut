using System.Collections;
using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{

    [TestFixture]
    public class BitArraySerializerTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new BitArraySerializer();
        }

        private BitArraySerializer _testObj;

        [Test]
        public void CanSerializeAndDeserializeDefaultBitArray()
        {
            var bitArray = new BitArray(8, false);
            var serialized = _testObj.Serialize(bitArray);
            var result = _testObj.Deserialize(serialized);
            Assert.That(bitArray, Is.EqualTo(result));
        }

        [Test]
        public void CanSerializeAndDeserializeAllTrueBitArray()
        {
            var expected = new BitArray(8, false);
            expected.Set(0, true);
            var serialized = _testObj.Serialize(expected);
            var result = _testObj.Deserialize(serialized);
            CollectionAssert.AreEqual(result, expected);
        }

        [Test]
        public void CanSerializeAndDeserializeAllSteps()
        {
            var bitArray = new BitArray(8, true) {[0] = false};
            //bitArray[9] = false;
            var byteArray = _testObj.BitArrayToBytes(bitArray);
            var serialized = BitArraySerializer.ByteArrayToString(byteArray);
            var byteArray2 = BitArraySerializer.StringToByteArray(serialized);
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

        [TestCase(0, 0, 1)]
        [TestCase(0, 1, 2)]
        [TestCase(0, 2, 4)]
        [TestCase(0, 3, 8)]
        [TestCase(0, 4, 16)]
        [TestCase(0, 5, 32)]
        [TestCase(0, 6, 64)]
        [TestCase(0, 7, 128)]
        [TestCase(1, 0, 1)]
        [TestCase(128, 0, 129)]
        [Test]
        public void SetBitTests(byte initialByte, int index, byte expected)
        {
            var result = _testObj.SetBit(initialByte, index);
            Assert.That(result, Is.EqualTo(expected));
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
            CollectionAssert.AreEqual(twoBytes, byteBack);
        }

    }
}