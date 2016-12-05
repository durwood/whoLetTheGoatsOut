﻿using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void RunCallsInit()
        {
            Board board = new Board(0);
            var testObj = new Game(board);
            var fakeOutput = new FakeOutput();
            testObj.SetOutput(fakeOutput);
            var fakeCommandInterface = new FakeCommandInterface();
            fakeCommandInterface.SetCommand("q");
            fakeCommandInterface.HasCommandToProcess = true;
            testObj.SetCommandInterface(fakeCommandInterface);
            testObj.Run();
            Assert.That(fakeOutput.InitCalled, Is.True);
        }        
    }

    public class FakeOutput : IDisplay
    {
        public bool InitCalled { get; private set; }

        public void Init()
        {
            InitCalled = true;
        }

        public void Display(Board board)
        {
            
        }

        public void DisplayLose(Board board)
        {
        }

        public void ShowResult(Board board)
        {

        }

        public void UpdateStatus(int elapsedSec, int numBombs)
        {
            
        }
    }
}