using System;
using bombsweeper;
using NUnit.Framework;
using System;
namespace bombsweeperXamarinTests
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
