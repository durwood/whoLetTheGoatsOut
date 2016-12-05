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
            Board board = BoardGenerator.GetStandardBoard();
            var testObj = new Game(board);
            var fakeOutput = new FakeOutput();
            testObj.SetOutput(fakeOutput);
            testObj.Run();
            Assert.That(fakeOutput.InitCalled, Is.True);
        }        
    }

    public class FakeOutput : ConsoleOutput
    {
        public bool InitCalled { get; private set; }

        public override void Init()
        {
            InitCalled = true;
        }
    }
}