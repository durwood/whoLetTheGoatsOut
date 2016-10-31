using System;

namespace bombsweeper
{
    public class CommandInterface
    {
        private readonly int _cursorLine;
        protected string _commandString;
        public bool HasCommandToProcess;

        public CommandInterface(int cursorLine)
        {
            _cursorLine = cursorLine;
            _commandString = "";
            HasCommandToProcess = false;
        }

        public string GetCommand()
        {
            return _commandString;
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
            }
            else if ((keyChar == '\r') || (keyChar == '\n'))
            {
                ClearCommand();
                HasCommandToProcess = true;
            }
            else if (keyChar == '\b')
            {
                _commandString = RemoveLastCharacter(_commandString);
                ClearCommand();
            }
            else
                _commandString = _commandString + keyChar;
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
            HasCommandToProcess = false;
            _commandString = "";
            ClearCommand();
        }

        private void RefreshDisplay()
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write("> " + _commandString);
        }
    }
}