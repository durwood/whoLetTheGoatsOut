using bombsweeper;
using NUnit.Framework;
using whoLetTheGoatsOut;

namespace bombsweeperTests
{
    [TestFixture]
    internal class WindowsCommandInterfaceTests
    {
        [Test]
        public void SetMove_Works()
        {
            var coordinate = new Coordinate(4, 3);
            var boardCommand = BoardCommand.MarkCell;
            var testObj = new WindowsCommandInterface();
            testObj.SetMove(coordinate, boardCommand);
            var fakeBoard = new FakeBoard();
            testObj.DoATurn(new FakeView(), fakeBoard);
            Assert.That(fakeBoard.CalledCell, Is.EqualTo(coordinate));
            Assert.That(fakeBoard.CalledCommand, Is.EqualTo(boardCommand));
        }
    }
}