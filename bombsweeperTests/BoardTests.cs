using bombsweeper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bombsweeperTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void BoardDisplaysProperly()
        {
            var c = Cell.Block;
            var testObj = new Board(2);
            string expected = $"{c} {c} \r\n{c} {c} \r\n";
            var result = testObj.Display();
            Assert.AreEqual(result, expected);
        }
    }
}