using System;
using bombsweeper;

namespace bombsweeperTests
{
    internal class FakeCommandInterface : CommandInterface
    {
        private ConsoleKeyInfo? _keyInfo;

        public FakeCommandInterface() : base(0)
        {
        }

        public void SetCommand(string str)
        {
            CommandString[_commandIndex] = str;
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

        public void SetCommandIndex(int commandIndex)
        {
            _commandIndex = commandIndex;
        }
    }
}