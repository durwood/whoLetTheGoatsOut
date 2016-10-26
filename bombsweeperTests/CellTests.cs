using System;
using bombsweeper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bombsweeperTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void NewCellDisplaysProperly()
        {
            var testObj = new Cell();
            Assert.AreEqual(testObj.Display(), $"{Cell.Block}");
        }
    }
}
