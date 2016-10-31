using System;
using bombsweeper;

namespace bombsweeperTests
{
    internal class FakeCommandInterface : CommandInterface
    {
        private ConsoleKeyInfo _keyInfo;

        public FakeCommandInterface() : base(0)
        {
        }

        public void SetCommand(string str)
        {
            _commandString = str;
        }

        public void SetKeyInfo(ConsoleKeyInfo keyInfo)
        {
            _keyInfo = keyInfo;
        }

        public override void Tick()
        {
            ProcessKeyInfo(_keyInfo);
        }

        protected override void ClearCommand()
        {
        }
    }
}