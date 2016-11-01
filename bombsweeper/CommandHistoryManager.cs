using System.Collections.Generic;

namespace bombsweeper
{
    public class CommandHistoryManager
    {
        private string _workingBuffer;
        protected List<string> CommandHistory = new List<string>();
        protected int HistoryIndex;

        public bool HasHistory()
        {
            return CommandHistory.Count > 0;
        }

        public string GetPreviousCommand()
        {
            var prevCommand = CommandHistory[HistoryIndex];
            if (HistoryIndex > 0)
                HistoryIndex -= 1;
            return prevCommand;
        }

        public string GetNextCommand()
        {
            if (HistoryIndex == CommandHistory.Count - 1)
                return _workingBuffer;
            HistoryIndex += 1;
            return CommandHistory[HistoryIndex];
        }

        public void StoreCommand(string command)
        {
            CommandHistory.Add(command);
            HistoryIndex = CommandHistory.Count - 1;
            SetWorkingBuffer("");
        }

        public void SetWorkingBuffer(string command)
        {
            _workingBuffer = command;
            HistoryIndex = CommandHistory.Count - 1;
        }
    }
}