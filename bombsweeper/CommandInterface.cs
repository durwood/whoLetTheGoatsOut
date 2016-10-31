using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace bombsweeper
{
    public class CommandInterface
    {
        private readonly int _cursorLine;
        protected List<string> CommandHistory = new List<string>();
        protected int _historyIndex;
        protected string CurrentCommand;
        public bool HasCommandToProcess;

        public CommandInterface(int cursorLine)
        {
            _cursorLine = cursorLine;
            CurrentCommand = "";
            HasCommandToProcess = false;
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

        private bool NoHistory()
        {
            return CommandHistory.Count == 0;
        }
        protected void ProcessKeyInfo(ConsoleKeyInfo keyInfo)
        {
            var keyChar = keyInfo.KeyChar;
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (NoHistory())
                    return;
                CurrentCommand = CommandHistory[_historyIndex];
                if (_historyIndex > 0)
                    _historyIndex -= 1;
                ClearCommand();
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (NoHistory())
                    return;
                if (_historyIndex == CommandHistory.Count - 1)
                {
                    if (CurrentCommand == CommandHistory[_historyIndex])
                        CurrentCommand = "";
                    else
                        CurrentCommand = CommandHistory[_historyIndex];
                }
                else
                {
                    CurrentCommand = CommandHistory[_historyIndex];
                    _historyIndex += 1;
                }
                ClearCommand();
            }
            else if ((keyChar == '\r') || (keyChar == '\n'))
            {
                ClearCommand();
                HasCommandToProcess = true;
                SaveCommandToHistory();
            }
            else if (keyChar == '\b')
            {
                CurrentCommand = RemoveLastCharacter(CurrentCommand);
                ClearCommand();
                ResetHistoryIndex();
            }
            else
            {
                CurrentCommand = CurrentCommand + keyChar;
                ResetHistoryIndex();
            }
        }

        private void SaveCommandToHistory()
        {
            CommandHistory.Add(CurrentCommand);
            ResetHistoryIndex();
        }

        private void ResetHistoryIndex()
        {
            _historyIndex = CommandHistory.Count - 1;
            //_cachedCommand = CurrentCommand;
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