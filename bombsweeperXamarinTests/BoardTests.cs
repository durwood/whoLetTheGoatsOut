using bombsweeper;
using NUnit.Framework;
namespace bombsweeperXamarinTests
{
    [TestFixture]
    public class BoardTests
    {
        char _c = Cell.Block;
        char _s = Cell.Space;
        char _b = Cell.Bomb;
        Board _testObj;


        [SetUp]
        public void SetUp()
        {
            _testObj = new Board(2);
        }

        [Test]
        public void BoardDisplaysProperly()
        {
            string expected = GetExpectedString(_c, _c, _c, _c);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ClickingOnCellRemovesBlock()
        {
            _testObj.Reveal(0, 0);
            string expected = GetExpectedString(_s, _c, _c, _c);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
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
        public void ClickingOnBombLosesGame()
        {
            _testObj.AddBomb(0, 1);
            _testObj.Reveal(0, 1);
            string expected = GetExpectedString(_c, _b, _c, _c);
            var result = _testObj.Display();
            Assert.AreEqual(expected, result);
            Assert.IsTrue(_testObj.GameLost());
        }
    }
}