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
            Assert.AreEqual("m 1,5", _testObj.GetCommand());
        }

        [Test]
        public void TestReveal()
        {
            _testObj.Reveal(2,3);
            Assert.IsTrue(_testObj.HasCommandToProcess);
            Assert.AreEqual("c 2,3", _testObj.GetCommand());
        }
    }
}