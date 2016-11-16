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
        public void CanSerializeAndDeserializeBoard()
        {
            _board.AddBomb(0, 0);
            _board.AddBomb(1, 1);
            var serialized = _testObj.Serialize(_board);
            var deSerialized = _testObj.DeSerialize(serialized);
            Assert.That(deSerialized.GetSize(), Is.EqualTo(_board.GetSize()));
            Assert.That(deSerialized.GetNumberOfUnmarkedBombs(), Is.EqualTo(_board.GetNumberOfUnmarkedBombs()));
        }

    }

    
}