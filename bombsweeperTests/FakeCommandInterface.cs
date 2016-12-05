using System;
using bombsweeper;

namespace bombsweeperTests
{
    internal class FakeCommandInterface : CommandInterface
    {
        private ConsoleKeyInfo? _keyInfo;

        public FakeCommandInterface() : base(new FakeView())
        {
        }

        public void SetCommand(string str)
        {
            CurrentCommand = str;
            HistoryManager.SetWorkingBuffer(str);
        }

        public void EnterCommand()
        {
            _keyInfo = ConsoleKeyHelper.KeyInfoFactory(ConsoleKey.Enter);
            Tick();
        }

        public void SetKeyInfo(ConsoleKeyInfo keyInfo)
        {
            _keyInfo = keyInfo;
        }

        public override void Tick()
        {
            if (_keyInfo != null)
            {
                ProcessKeyInfo(_keyInfo.Value);
                _keyInfo = null;
            }
        }

        protected override void ClearCommand()
        {
        }
    }

    internal class FakeView : IView
    {
        public void Clear()
        {
        }

        public void DisplayBoard(Board board)
        {
        }

        public void StatusDisplay(int numBombs, int elapsedSec)
        {
        }

        public void Quit()
        {
        }

        public void Lose()
        {
        }

        public void Win()
        {
        }

        public void DisplayFooter(Board board)
        {
        }

        public void DisplayRow(string line)
        {
        }

        public int GetCursorPosition()
        {
            return 0;
        }
    }
}