using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class BoardGeneratorTests
    {
        [SetUp]
        public void SetUp()
        {
            var rnd = new FakeRandomGenerator();
            _testObj = new BoardGenerator(rnd);
        }

        private BoardGenerator _testObj;

        [Test]
        public void BoardGeneratorGeneratesBoardOfCorrectSize()
        {
            var board = _testObj.GenerateBoard(2, 0);
            Assert.That(board.GetSize(), Is.EqualTo(2));
        }

        [Test]
        public void BoardGeneratorGeneratesBoardWithCorrectNumberOfBombs()
        {
            var board = _testObj.GenerateBoard(2, 1);
            Assert.That(board.GetNumberOfUnmarkedBombs(), Is.EqualTo(1));
        }
    }

    internal class FakeRandomGenerator : IRandomGenerator
    {
        public double NextDouble()
        {
            return 0.0;
        }

        public int Next(int minValue, int maxValue)
        {
            return 0;
        }
    }
}