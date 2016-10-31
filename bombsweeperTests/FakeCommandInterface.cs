﻿using System;
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
            CurrentCommand = str;
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

        public void SetCommandHistoryIndex(int commandIndex)
        {
            _historyIndex = commandIndex;
        }
    }
}