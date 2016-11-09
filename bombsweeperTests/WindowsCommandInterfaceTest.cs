using NUnit.Framework;
using bombsweeperWinform;

namespace bombsweeperTests
{
    [TestFixture]
    public class WindowsCommandInterfaceTest
    {
        private WindowsCommandInterface _testObj;

        [SetUp]
        public void SetUp()
        {
            _testObj = new WindowsCommandInterface();
        }
        [Test]
        public void TestConstruction()
        {
            Assert.IsFalse(_testObj.HasCommandToProcess);
            Assert.AreEqual("", _testObj.GetCommand());
        }

        [Test]
        public void TestMark()
        {
            _testObj.Mark(1,5);
            Assert.IsTrue(_testObj.HasCommandToProcess);
            Assert.AreEqual("m 2,6", _testObj.GetCommand());
        }

        [Test]
        public void TestReveal()
        {
            _testObj.Reveal(2,3);
            Assert.IsTrue(_testObj.HasCommandToProcess);
            Assert.AreEqual("c 3,4", _testObj.GetCommand());
        }

        [Test]
        public void TestReset()
        {
            _testObj.Reveal(5,6);
            _testObj.Reset();
            Assert.IsFalse(_testObj.HasCommandToProcess);
            Assert.AreEqual("", _testObj.GetCommand());
        }
    }
}