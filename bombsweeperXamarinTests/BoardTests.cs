using bombsweeper;
using NUnit.Framework;
namespace bombsweeperXamarinTests
{
    [TestFixture]
    public class BoardTests
    {
        char _hidden = Cell.Block;
        char _space = Cell.Empty;
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
            StringAssert.Contains(_space.ToString(), result);
        }

        private string GetExpectedString(params char[] cells)
        {
            string row1 = $"1 {cells[0]} {cells[1]} ";
            string row2 = $"2 {cells[2]} {cells[3]} ";
            string footer = "  1 2 ";
            string expected = string.Join("\n", row1, row2, footer) + "\n";
            return expected;
        }

        [Test]
        public void ClickingOnBombLosesGameAndRevealsBoard()
        {
            _testObj.AddBomb(0, 1);
            _testObj.Reveal(0, 1);
            string expected = GetExpectedString(_space, _space, _bomb, _space);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_testObj.GameLost());
        }

        [Test]
        public void ClickingOnNonBombRevealsAdjacentCells()
        {
            _testObj.AddBomb(0, 0);
            _testObj.Reveal(1, 1);
            string expected = GetExpectedString(_hidden, _space, _space, _space);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
        }
    }
}