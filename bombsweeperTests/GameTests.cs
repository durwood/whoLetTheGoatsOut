using System;
using bombsweeper;
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
            testObj.Run();
            Assert.That(fakeOutput.InitCalled, Is.True);
        }        
    }

    public class FakeOutput : IDisplay
    {
        public FakeOutput()
        {
            HasCommandToProcess = true;
        }

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

        public void Tick()
        {
        }

        public bool HasCommandToProcess { get; }
        public string GetCommand()
        {
            return "q";
        }

        public void Reset()
        {
        }

        public void Start(Game game)
        {
        }

        public bool PumpOutputQueue(Action<string> executeBoardCommand)
        {
            return true;
        }
    }
}