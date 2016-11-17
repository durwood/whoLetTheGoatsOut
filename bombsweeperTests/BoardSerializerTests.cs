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
            _board = new Board(4);
            _testObj = new BoardSerializer();
        }

        [TearDown]
        public void tearDown()
        {
            _board = null;
            _testObj = null;
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