using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class CommandParserTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new CommandInterface(new FakeView());
        }

        private CommandInterface _testObj;

        [Test]
        public void CanProcessClickCommand()
        {
            var command = _testObj.GetCommand("c A,2");
            var cell = _testObj.GetCell();
            var expected = new Coordinate {X = 1, Y = 0};
            Assert.AreEqual(BoardCommand.RevealCell, command);
            Assert.AreEqual(cell, expected);
        }

        [Test]
        public void CanProcessCoordinatesInEitherOrderCommand()
        {
            _testObj.GetCommand("x A,2");
            var cell1 = _testObj.GetCell();

            _testObj.GetCommand("x 2,A");
            var cell2 = _testObj.GetCell();

            Assert.That(cell1, Is.EqualTo(cell2));
        }

        [Test]
        public void CanProcessMarkCommand()
        {
            var command = _testObj.GetCommand("m A,2");
            var cell = _testObj.GetCell();
            var expected = new Coordinate {X = 1, Y = 0};
            Assert.AreEqual(BoardCommand.MarkCell, command);
            Assert.AreEqual(cell, expected);
        }

        [Test]
        public void CanProcessQuitCommand()
        {
            var command = _testObj.GetCommand("q");
            Assert.AreEqual(BoardCommand.QuitGame, command);
        }

        [Test]
        public void CommandProcessingIsNotCaseSeNsItIvE()
        {
            var command1 = _testObj.GetCommand("c A,2");
            var cell1 = _testObj.GetCell();

            var command2 = _testObj.GetCommand("C a,2");
            var cell2 = _testObj.GetCell();

            Assert.That(command1, Is.EqualTo(command2));
            Assert.That(cell1, Is.EqualTo(cell2));
        }
    }
}