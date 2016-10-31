using System;
using System.Collections.Generic;

namespace bombsweeper
{
    public class CommandInterface
    {
        private readonly int _cursorLine;
        protected List<string> CommandString = new List<string>();
        protected int _commandIndex;
        public bool HasCommandToProcess;

        public CommandInterface(int cursorLine)
        {
            _cursorLine = cursorLine;
            CommandString.Add("");
            _commandIndex = 0;
            HasCommandToProcess = false;
        }

        public string GetCommand()
        {
            return CommandString[_commandIndex];
        }

        public virtual void Tick()
        {
            if (Console.KeyAvailable)
                ProcessKeyInfo(Console.ReadKey());
            RefreshDisplay();
        }

        protected void ProcessKeyInfo(ConsoleKeyInfo keyInfo)
        {
            var keyChar = keyInfo.KeyChar;
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (_commandIndex > 0)
                    _commandIndex -= 1;
                ClearCommand();
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (_commandIndex < CommandString.Count - 1)
                    _commandIndex += 1;
                ClearCommand();
            }
            else if ((keyChar == '\r') || (keyChar == '\n'))
            {
                ClearCommand();
                HasCommandToProcess = true;
            }
            else if (keyChar == '\b')
            {
                CommandString[_commandIndex] = RemoveLastCharacter(CommandString[_commandIndex]);
                ClearCommand();
            }
            else
                CommandString[_commandIndex] = CommandString[_commandIndex] + keyChar;
        }

        private static string RemoveLastCharacter(string str)
        {
            return str.Length > 0 ? str.Substring(0, str.Length - 1) : str;
        }

        protected virtual void ClearCommand()
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        public void Reset()
        {
            if (HasCommandToProcess)
            {
                HasCommandToProcess = false;
                CommandString.Add("");
                _commandIndex++;
                ClearCommand();
            }
        }

        private void RefreshDisplay()
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write("> " + CommandString[_commandIndex]);
        }
    }
}