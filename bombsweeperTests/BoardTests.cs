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
        private Board _testObj;

        private string GetExpectedString(params char[] cells)
        {
            var cellIdx = 0;
            var sb = new StringBuilder();
            var size = cells.Length == 4 ? 2 : 3;
            for (var row = 0; row < size; ++row)
            {
                for (var col = 0; col < size; ++col)
                {
                    var cell = cells[cellIdx++];
                    sb.Append($"{cell} ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        [Test]
        public void BoardDisplaysProperly()
        {
            var expected = GetExpectedString(_hidden, _hidden, _hidden, _hidden);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void BoardIsInitializedWithAdjacencyCounts()
        {
            var testObj = new Board(3);
            testObj.AddBomb(0, 0);
            testObj.AddBomb(1, 0);
            var expected = GetExpectedString(_hidden, _hidden, _hidden, '2', '2', '1', _empty, _empty, _empty);
            testObj.Reveal(0, 2);
            var result = testObj.Display();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ClickingOnBombLosesGameAndRevealsBoard()
        {
            _testObj.AddBomb(0, 1);
            _testObj.Reveal(0, 1);
            var expected = GetExpectedString(_hidden, _hidden, _bomb, _hidden);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_testObj.GameLost());
        }

        [Test]
        public void RevealingLastCellWinsGame()
        {
            _testObj.AddBomb(0, 0);
            _testObj.Reveal(1, 1);
            var expected = GetExpectedString(_hidden, '1', '1', '1');
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_testObj.GameWon());
            
        }

        [Test]
        public void ClickingOnCellRemovesBlock()
        {
            _testObj.Reveal(0, 0);
            var result = _testObj.Display();
            StringAssert.Contains(_empty.ToString(), result);
        }

        [Test]
        public void ClickingOnNonBombRevealsAdjacentCells()
        {
            _testObj.AddBomb(0, 0);
            _testObj.Reveal(1, 1);
            var expected = GetExpectedString(_hidden, '1', '1', '1');
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RowsDisplayProperly()
        {
            var result = _testObj.DisplayRow(0);
            var expected = $"{_hidden} {_hidden} ";
            Assert.AreEqual(expected, result);
        }
    }
}