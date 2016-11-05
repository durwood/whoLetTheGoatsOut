using System.Text;
using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class BoardTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new Board(2);
        }

        private readonly char _hidden = Cell.Block;
        private readonly char _empty = Cell.Empty;
        private readonly char _bomb = Cell.Bomb;
        private readonly char _marked = Cell.Check;
        private Board _testObj;

        private static string GetExpectedString(params char[] cells)
        {
            return string.Join(" ", cells);
        }

        internal static void ValidateCells(Board board, params char[] cells)
        {
            var expected = GetExpectedString(cells);
            var result = board.ToString();
            Assert.AreEqual(expected, result);
        }

        private void ValidateCells(params char[] cells)
        {
            var expected = GetExpectedString(cells);
            var result = _testObj.ToString();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void BoardIsInitializedWithAdjacencyCounts()
        {
            _testObj = new Board(3);
            _testObj.AddBomb(0, 0);
            _testObj.AddBomb(1, 0);
            _testObj.Reveal(2, 0);
            ValidateCells(_hidden, _hidden, _hidden, '2', '2', '1', _empty, _empty, _empty);
        }

        [Test]
        public void ClickingOnBombLosesGameAndRevealsBoard()
        {
            _testObj.AddBomb(0, 1);
            _testObj.Reveal(1, 0);
            ValidateCells(_hidden, _hidden, _bomb, _hidden);
            Assert.IsTrue(_testObj.GameLost());
        }

        [Test]
        public void ClickingOnMarkedCellDoesNothing()
        {
            _testObj.AddBomb(0, 0);
            _testObj.ToggleMark(0, 0);
            _testObj.ToggleMark(0, 1);
            _testObj.Reveal(0, 0);
            _testObj.Reveal(0, 1);
            ValidateCells(_marked, _marked, _hidden, _hidden);
        }

        [Test]
        public void ClickingOnNonBombRevealsAdjacentCells()
        {
            _testObj.AddBomb(0, 0);
            _testObj.Reveal(1, 1);
            ValidateCells(_hidden, '1', '1', '1');
        }

        [Test]
        public void IniltialBoardDisplaysProperly()
        {
            ValidateCells(_hidden, _hidden, _hidden, _hidden);
        }

        [Test]
        public void MarkedCellsWithBombsAreNotRevealedWhenGameIsLost()
        {
            _testObj.AddBomb(0, 0);
            _testObj.AddBomb(1, 0);
            _testObj.ToggleMark(0, 0);
            _testObj.Reveal(0, 1);
            ValidateCells(_marked, _bomb, _hidden, _hidden);
        }

        [Test]
        public void NumberOfUnmarkedBombsDecrementsWhenCellsMarked()
        {
            _testObj.AddBomb(0, 0);
            _testObj.AddBomb(0, 1);
            _testObj.ToggleMark(0, 0); // mark bomb
            Assert.That(_testObj.GetNumberOfUnmarkedBombs(), Is.EqualTo(1));
            _testObj.ToggleMark(0, 0); // unmark bomb
            Assert.That(_testObj.GetNumberOfUnmarkedBombs(), Is.EqualTo(2));
            _testObj.ToggleMark(0, 1); // mark non-bomb
            Assert.That(_testObj.GetNumberOfUnmarkedBombs(), Is.EqualTo(1));
        }

        [Test]
        public void NumberOfUnmarkedBombsEqualsNumberOfBombsInitially()
        {
            _testObj.AddBomb(0, 0);
            _testObj.AddBomb(0, 1);
            Assert.That(_testObj.GetNumberOfUnmarkedBombs(), Is.EqualTo(2));
        }

        [Test]
        public void NumberOfUnmarkedBombsNeverGoesBelowZero()
        {
            _testObj.ToggleMark(0, 0);
            Assert.That(_testObj.GetNumberOfUnmarkedBombs(), Is.EqualTo(0));
        }

        [Test]
        public void RevealingLastCellWinsGame()
        {
            _testObj.AddBomb(0, 0);
            _testObj.Reveal(1, 1);
            ValidateCells(_hidden, '1', '1', '1');
            Assert.IsTrue(_testObj.GameWon());
        }

        [Test]
        public void UnmarkingBombsIncreasesNumberOfUnmarkedBombs()
        {
            _testObj.AddBomb(0, 0);
            _testObj.ToggleMark(0, 0);
            _testObj.ToggleMark(0, 0);
            Assert.That(_testObj.GetNumberOfUnmarkedBombs(), Is.EqualTo(1));
        }
    }
}