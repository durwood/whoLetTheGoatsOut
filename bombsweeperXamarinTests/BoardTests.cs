using bombsweeper;
using NUnit.Framework;
using System.Text;
namespace bombsweeperXamarinTests
{
    [TestFixture]
    public class BoardTests
    {
        char _hidden = Cell.Block;
        char _empty = Cell.Empty;
        char _bomb = Cell.Bomb;
        Board _testObj;


        [SetUp]
        public void SetUp()
        {
            _testObj = new Board(2);
        }

        [Test]
        public void BoardDisplaysProperly()
        {
            string expected = GetExpectedString(_hidden, _hidden, _hidden, _hidden);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ClickingOnCellRemovesBlock()
        {
            _testObj.Reveal(0, 0);
            var result = _testObj.Display();
            StringAssert.Contains(_empty.ToString(), result);
        }

        private string GetExpectedString(params char[] cells)
        {
            int cellIdx = 0;
            var sb = new StringBuilder();
            int size = cells.Length == 4 ? 2 : 3;
            for (int row = 0; row < size; ++row)
            {
                sb.Append($"{row+1} ");
                for (int col = 0; col < size; ++col)
                    sb.Append($"{cells[cellIdx++]} ");
                sb.AppendLine();
            }
            sb.Append("  ");
            for (int col = 0; col < size; ++col)
                sb.Append($"{col+1} ");
            sb.AppendLine();
            return sb.ToString();
                    

            //string row2 = $"2 {cells[2]} {cells[3]} ";
            //string footer = "  1 2 ";
            //string expected = string.Join("\n", row1, row2, footer) + "\n";
            //return expected;
        }

        [Test]
        public void ClickingOnBombLosesGameAndRevealsBoard()
        {
            _testObj.AddBomb(0, 1);
            _testObj.Reveal(0, 1);
            string expected = GetExpectedString(_empty, _empty, _bomb, _empty);
            var result = _testObj.Display();
            Assert.AreEqual(result, expected);
            Assert.IsTrue(_testObj.GameLost());
        }

        [Test]
        public void ClickingOnNonBombRevealsAdjacentCells()
        {
            _testObj.AddBomb(0, 0);
            _testObj.Reveal(1, 1);
            string expected = GetExpectedString(_hidden, _empty, _empty, _empty);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
        }

        [Test, Ignore("WIP")]
        public void BoardIsInitializedWithAdjacencyCounts()
        {
            var testObj = new Board(3);
            testObj.AddBomb(0, 0);
            testObj.AddBomb(1, 0);
            string expected = GetExpectedString(_hidden, _hidden, '1', '2', '1', '1', _empty, _empty, _empty);
            var result = testObj.Display();
            Assert.AreEqual(expected, result);
        }
    }
}