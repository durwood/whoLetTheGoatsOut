using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class InputGetterTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new CommandParser();
        }

        private CommandParser _testObj;

        private void ValidateCell(int x, int y)
        {
            var boardcell = _testObj.GetCell();
            Assert.AreEqual(x, boardcell.X);
            Assert.AreEqual(y, boardcell.Y);
        }

        [Test]
        public void CanProcessClickCommand()
        {
            var command = _testObj.GetCommand("c 1,2");
            Assert.AreEqual(BoardCommand.RevealCell, command);
            ValidateCell(0, 1);
        }

        [Test]
        public void CanProcessMarkCommand()
        {
            var command = _testObj.GetCommand("m 1,2");
            Assert.AreEqual(BoardCommand.MarkCell, command);
            ValidateCell(0, 1);
        }

        [Test]
        public void CanProcessQuitCommand()
        {
            var command = _testObj.GetCommand("q");
            Assert.AreEqual(BoardCommand.QuitGame, command);
        }
    }
}