using System;
using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    public static class ConsoleKeyHelper
    {
        public static ConsoleKeyInfo KeyInfoFactory(ConsoleKey key)
        {
            var keyChar = LookupKeyChar(key);
            return new ConsoleKeyInfo(keyChar, key, false, false, false);
        }

        private static char LookupKeyChar(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Delete:
                    return '\b';
                case ConsoleKey.Enter:
                    return '\r';
                case ConsoleKey.D1:
                    return '1';
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    return '\0';
                default:
                    throw new ArgumentException("Unsupported Console Key.");
            }
        }
    }

    [TestFixture]
    public class CommandInterfaceTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new ConsoleOutput(0);
            _fakeConsoleWrapper = new FakeConsoleWrapper();
            _testObj._consoleWrapper = _fakeConsoleWrapper;
            _historyManager = _testObj.HistoryManager;
        }

        private readonly ConsoleKeyInfo _backSpace = ConsoleKeyHelper.KeyInfoFactory(ConsoleKey.Delete);
        private readonly ConsoleKeyInfo _return = ConsoleKeyHelper.KeyInfoFactory(ConsoleKey.Enter);
        private readonly ConsoleKeyInfo _1 = ConsoleKeyHelper.KeyInfoFactory(ConsoleKey.D1);
        private readonly ConsoleKeyInfo _upArrow = ConsoleKeyHelper.KeyInfoFactory(ConsoleKey.UpArrow);
        private readonly ConsoleKeyInfo _downArrow = ConsoleKeyHelper.KeyInfoFactory(ConsoleKey.DownArrow);
        private ConsoleOutput _testObj;
        private FakeConsoleWrapper _fakeConsoleWrapper;
        private CommandHistoryManager _historyManager;

        private void Tick(ConsoleKeyInfo keyInfo)
        {
            _fakeConsoleWrapper.SetKeyInfo(keyInfo);
            _testObj.Tick();
        }

        private void SetCommand(string s)
        {
            _testObj.CurrentCommand = s;
            _historyManager.SetWorkingBuffer(s);
        }

        [Test]
        public void BackspaceRemovesLastCharacter()
        {
            SetCommand("c 1,1");
            _fakeConsoleWrapper.SetKeyInfo(_backSpace);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,"));
        }

        [Test]
        public void CallingResetOnObjectNotReadyForProcessingDoesNothing()
        {
            SetCommand("c 1,1");
            _testObj.Reset();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,1"));
        }

        [Test]
        public void DownArrowDoesNothingIfLessThanTwoCommandsAreInBuffer()
        {
            _fakeConsoleWrapper.SetKeyInfo(_downArrow);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo(""));

            SetCommand("c 0,0");
            _fakeConsoleWrapper.SetKeyInfo(_downArrow);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 0,0"));
        }

        [Test]
        public void DownArrowRetrievesCachedCommandWhenGoingPastBufferEnd()
        {
            SetCommand("c 0,0"); // 0
            Tick(_return);
            SetCommand("j");
            Tick(_downArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("j"));
            Tick(_downArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("j"));
        }

        [Test]
        public void DownArrowRetrievesNextCommandIfTwoOrMoreCommandsAreInBuffer()
        {
            SetCommand("c 0,0"); // 0
            Tick(_return);
            SetCommand("c 1,1"); // 1
            Tick(_return);
            SetCommand("c 2,2"); // 2
            Tick(_return);
            Tick(_upArrow);
            Tick(_downArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 2,2"));
            Tick(_backSpace);
            Tick(_downArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 2,"));
        }

        [Test]
        public void EnterReadiesCommandForProcessing()
        {
            SetCommand("c 1,1");
            _testObj.HasCommandToProcess = false;
            _fakeConsoleWrapper.SetKeyInfo(_return);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,1"));
            Assert.IsTrue(_testObj.HasCommandToProcess);
        }

        [Test]
        public void NonSpecialKeyAddsCharacterToEndOfCommand()
        {
            SetCommand("c 1,");
            _fakeConsoleWrapper.SetKeyInfo(_1);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,1"));
        }

        [Test]
        public void ResetClearsCommandAndMakesUnavailable()
        {
            SetCommand("c 0,0");
            _testObj.HasCommandToProcess = true;
            _testObj.Reset();
            Assert.That(_testObj.GetCommand(), Is.EqualTo(""));
            Assert.IsFalse(_testObj.HasCommandToProcess);
        }

        [Test]
        public void UpArrowDoesNothingIfLessThanTwoCommandsAreInBuffer()
        {
            _fakeConsoleWrapper.SetKeyInfo(_upArrow);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo(""));

            SetCommand("c 0,0");
            _fakeConsoleWrapper.SetKeyInfo(_upArrow);
            _testObj.Tick();
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 0,0"));
        }

        [Test]
        public void UpArrowRetrievesPreviousCommandIfTwoOrMoreCommandsAreInBuffer()
        {
            SetCommand("c 0,0"); // 0
            Tick(_return);
            SetCommand("c 1,1"); // 1
            Tick(_return);
            SetCommand("c 2,2"); // 2
            Tick(_return);
            Tick(_upArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 2,2"));
            Tick(_upArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 1,1"));
            Tick(_upArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 0,0"));
            Tick(_upArrow);
            Assert.That(_testObj.GetCommand(), Is.EqualTo("c 0,0"));
        }
    }

    public class FakeConsoleWrapper : ConsoleWrapper
    {
        private ConsoleKeyInfo _keyInfo;

        public void SetKeyInfo(ConsoleKeyInfo keyInfo)
        {
            _keyInfo = keyInfo;
        }

        public override void WriteToWidth(char c)
        {
        }

        public override ConsoleKeyInfo ReadKey()
        {
            return _keyInfo;
        }

        public override bool KeyAvailable()
        {
            return true;
        }


        public override void SetCursorPosition(int i, int cursorLine)
        {
        }

        public override void Write(string s)
        {
        }
    }
}