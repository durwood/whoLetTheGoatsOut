using bombsweeper;
using NUnit.Framework;
namespace bombsweeperXamarinTests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void BoardDisplaysProperly()
        {
            var c = Cell.Block;
            var testObj = new Board(2);
			string footer = "  1 2 ";
			string row1 = $"1 {c} {c} ";
			string row2 = $"2 {c} {c} ";
			string expected = string.Join("\n", row1, row2, footer) + "\n";
            var result = testObj.Display();
            Assert.AreEqual(result, expected);
        }
    }
}