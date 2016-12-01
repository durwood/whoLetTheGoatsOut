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

        private readonly string _hidden = CellConsoleView.Block;
        private readonly string _empty = CellConsoleView.Empty;
        private readonly string _bomb = CellConsoleView.Bomb;
        private readonly string _marked = CellConsoleView.Check;
        private Board _testObj;

        private static string GetExpectedString(params string[] cells)
        {
            return string.Join(" ", cells);
        }

        internal static void ValidateCells(Board board, params string[] cells)
        {
            var expected = GetExpectedString(cells);
            var result = board.ToString();
            Assert.AreEqual(expected, result);
        }

        private void ValidateCells(params string[] cells)
        {
            var cellIdx = 0;
            foreach (var cell in _testObj.GetCells())
            {
                var expected = cells[cellIdx++];
                var result = _testObj.ToString();
                Assert.AreEqual(expected, result);
            }

        }

        [Test]
        [Ignore("How to Validate?")]
        public void BoardIsInitializedWithAdjacencyCounts()
        {
            _testObj = new Board(3);
            _testObj.AddBomb(0, 0);
            _testObj.AddBomb(1, 0);
            _testObj.Reveal(2, 0);
            ValidateCells(_hidden, _hidden, _hidden, "2", "2", "1", _empty, _empty, _empty);
        }

        [Test]
        public void ClickingOnBombLosesGameAndRevealsBoard()
        {
            _testObj.AddBomb(1, 0);
            _testObj.Reveal(1, 0);
            //ValidateCells(_hidden, _hidden, _bomb, _hidden);
            Assert.IsTrue(_testObj.GameLost());
        }

        [Test]
        [Ignore("How to Validate?")]
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
        public void ClickingOnNonBombDoesNotRevealDiagonalNonBomb()
        {
            _testObj.AddBomb(0, 0);
            _testObj.AddBomb(1, 1);
            _testObj.Reveal(0, 1);
            Assert.False(_testObj.GetCells()[1, 0].IsRevealed);
        }

        [Test]
        [Ignore("How to Validate?")]
        public void ClickingOnNonBombRevealsAdjacentCells()
        {
            _testObj.AddBomb(0, 0);
            _testObj.Reveal(1, 1);
            ValidateCells(_hidden, "1", "1", "1");
        }

        [Test]
        [Ignore("How to Validate?")]
        public void IniltialBoardDisplaysProperly()
        {
            ValidateCells(_hidden, _hidden, _hidden, _hidden);
        }

        [Test]
        [Ignore("How to validate?")]
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
            _testObj.Reveal(1, 0);
            _testObj.Reveal(0, 1);
            _testObj.Reveal(1, 1);
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