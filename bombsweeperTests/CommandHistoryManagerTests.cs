using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class CommandHistoryManagerTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new CommandHistoryManager();
        }

        private CommandHistoryManager _testObj;

        [Test]
        public void GetNextReturnsEmptyStringWhenNothingAvailable()
        {
            _testObj.StoreCommand("Command0");
            Assert.That(_testObj.GetNextCommand(), Is.EqualTo(""));
            _testObj.GetPreviousCommand();
            Assert.That(_testObj.GetNextCommand(), Is.EqualTo(""));
        }

        [Test]
        public void GetNextReturnsNextHistoryEntries()
        {
            _testObj.StoreCommand("Command0");
            _testObj.StoreCommand("Command1");
            _testObj.GetPreviousCommand();
            _testObj.GetPreviousCommand();
            _testObj.GetNextCommand();
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command1"));
        }

        [Test]
        public void GetPreviousReturnsEarliestEntryIfUnableToAdvance()
        {
            _testObj.StoreCommand("Command0");
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command0"));
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command0"));
        }

        [Test]
        public void GetPreviousReturnsPreviousHistoryEntries()
        {
            _testObj.StoreCommand("Command0");
            _testObj.StoreCommand("Command1");
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command1"));
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command0"));
        }

        [Test]
        public void HasNoHistoryInitially()
        {
            Assert.IsFalse(_testObj.HasHistory());
        }

        [Test]
        public void UpAndDownWorksSmoothly()
        {
            _testObj.StoreCommand("Command0");
            _testObj.StoreCommand("Command1");
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command1"));
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command0"));
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command0"));
            Assert.That(_testObj.GetNextCommand(), Is.EqualTo("Command1"));
            Assert.That(_testObj.GetNextCommand(), Is.EqualTo(""));
            Assert.That(_testObj.GetNextCommand(), Is.EqualTo(""));
            Assert.That(_testObj.GetPreviousCommand(), Is.EqualTo("Command1"));
        }
    }
}