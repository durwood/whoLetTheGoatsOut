using System;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class CommandInterfaceTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new FakeCommandInterface();
        }

        private readonly ConsoleKeyInfo _backSpace = KeyInfoFactory('\b', ConsoleKey.Delete);
        private readonly ConsoleKeyInfo _return = KeyInfoFactory('\r', ConsoleKey.Enter);
        private readonly ConsoleKeyInfo _1 = KeyInfoFactory('1', ConsoleKey.D1);
        private ConsoleKeyInfo _upArrow = KeyInfoFactory('\0', ConsoleKey.UpArrow);
        private ConsoleKeyInfo _downArrow = KeyInfoFactory('\0', ConsoleKey.DownArrow);
        private FakeCommandInterface _testObj;

        private static ConsoleKeyInfo KeyInfoFactory(char keyChar, ConsoleKey key)
        {
            return new ConsoleKeyInfo(keyChar, key, false, false, false);
        }

        [Test]
        public void BackspaceRemovesLastCharacter()
        {
            _testObj.SetCommand("c 1,1");
            _testObj.SetKeyInfo(_backSpace);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,"));
        }

        [Test]
        public void EnterReadiesCommandForProcessing()
        {
            _testObj.SetCommand("c 1,1");
            _testObj.HasCommandToProcess = false;
            _testObj.SetKeyInfo(_return);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,1"));
            Assert.IsTrue(_testObj.HasCommandToProcess);
        }

        [Test]
        public void NonSpecialKeyAddsCharacterToEndOfCommand()
        {
            _testObj.SetCommand("c 1,");
            _testObj.SetKeyInfo(_1);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,1"));
        }

        [Test]
        public void ResetClearsCommandAndMakesUnavailable()
        {
            _testObj.SetCommand("c 0,0");
            _testObj.HasCommandToProcess = true;
            _testObj.Reset();
            Assert.That(_testObj.GetCommand(), Is.EqualTo(""));
            Assert.IsFalse(_testObj.HasCommandToProcess);
        }
    }
}