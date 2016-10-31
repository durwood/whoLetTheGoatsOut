using System;

namespace bombsweeper
{
    public class CommandInterface
    {
        private readonly int _cursorLine;
        protected readonly CommandHistoryManager _historyManager;
        protected string CurrentCommand;
        public bool HasCommandToProcess;

        public CommandInterface(int cursorLine)
        {
            _cursorLine = cursorLine;
            CurrentCommand = "";
            HasCommandToProcess = false;
            _historyManager = new CommandHistoryManager();
        }

        public string GetCommand()
        {
            return CurrentCommand;
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
                if (_historyManager.HasHistory())
                    CurrentCommand = _historyManager.GetPreviousCommand();
                ClearCommand();
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (_historyManager.HasHistory())
                    CurrentCommand = _historyManager.GetNextCommand();
                ClearCommand();
            }
            else if ((keyChar == '\r') || (keyChar == '\n'))
            {
                ClearCommand();
                HasCommandToProcess = true;
                _historyManager.StoreCommand(CurrentCommand);
            }
            else if (keyChar == '\b')
            {
                CurrentCommand = RemoveLastCharacter(CurrentCommand);
                ClearCommand();
                _historyManager.SetWorkingBuffer(CurrentCommand);
            }
            else
            {
                CurrentCommand = CurrentCommand + keyChar;
                _historyManager.SetWorkingBuffer(CurrentCommand);
            }
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
                CurrentCommand = "";
                ClearCommand();
            }
        }

        private void RefreshDisplay()
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write("> " + CurrentCommand);
        }
    }
}