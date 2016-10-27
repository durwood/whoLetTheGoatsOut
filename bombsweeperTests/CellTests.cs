using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void NewCellDisplaysProperly()
        {
            var testObj = new Cell();
            Assert.AreEqual(testObj.Display(), $"{Cell.Block}");
        }
    }
}