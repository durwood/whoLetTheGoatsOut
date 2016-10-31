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

        private void GetCommandFromHistory(ConsoleKey key)
        {
            if (!_historyManager.HasHistory())
                return;

            if (key == ConsoleKey.UpArrow)
                CurrentCommand = _historyManager.GetPreviousCommand();
            else if (key == ConsoleKey.DownArrow)
                CurrentCommand = _historyManager.GetNextCommand();
            ClearCommand();
        }

        private void SubmitCommand()
        {
            ClearCommand();
            HasCommandToProcess = true;
            _historyManager.StoreCommand(CurrentCommand);
        }

        protected void ProcessKeyInfo(ConsoleKeyInfo keyInfo)
        {
            var keyChar = keyInfo.KeyChar;

            if ((keyInfo.Key == ConsoleKey.UpArrow) || (keyInfo.Key == ConsoleKey.DownArrow))
                GetCommandFromHistory(keyInfo.Key);
            else if ((keyChar == '\r') || (keyChar == '\n'))
                SubmitCommand();
            else
                ModifyCurrentCommand(keyChar);
        }

        private void ModifyCurrentCommand(char keyChar)
        {
            if (keyChar == '\b')
            {
                CurrentCommand = RemoveLastCharacter(CurrentCommand);
                ClearCommand();
            }
            else
            {
                CurrentCommand = CurrentCommand + keyChar;
            }
            _historyManager.SetWorkingBuffer(CurrentCommand);
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