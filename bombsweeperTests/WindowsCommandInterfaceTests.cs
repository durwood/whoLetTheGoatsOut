using bombsweeper;
using NUnit.Framework;
using whoLetTheGoatsOut;

namespace bombsweeperTests
{
    [TestFixture]
    internal class WindowsCommandInterfaceTests
    {
        private FakeBoard _fakeBoard;
        private WindowsCommandInterface _testObj;
        private FakeView _fakeView;

        [SetUp]
        public void SetUp()
        {
            _testObj = new WindowsCommandInterface();
            _fakeBoard = new FakeBoard();
            _fakeView = new FakeView();
        }
        [Test]
        public void SetCell_Works()
        {
            _testObj.SetCell(4, 3);
            _testObj.DoATurn(_fakeView, _fakeBoard);
            Assert.That(_fakeBoard.CalledCell, Is.EqualTo(new Coordinate(4, 3)));
        }

        [Test]
        public void SetCommand_Works()
        {
            var command = BoardCommand.MarkCell;
            _testObj.SetCommand(command);
            _testObj.DoATurn(_fakeView, _fakeBoard);
            Assert.That(_fakeBoard.CalledCommand, Is.EqualTo(command));
        }
    }
}