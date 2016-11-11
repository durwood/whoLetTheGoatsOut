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
            var bitArray = new BitArray(1, true);
            var serialized = _testObj.SerializeBitArray(bitArray);
            var result = _testObj.DeserializeBitArray(serialized);
            Assert.That(bitArray, Is.EqualTo(result));
        }

    }
}