using System;

namespace bombsweeper
{
    public class CommandInterface : ICommandInterface
    {
        private readonly int _cursorLine;
        protected readonly CommandHistoryManager HistoryManager;

        private Coordinate _click;
        protected string CurrentCommand;
        public bool HasCommandToProcess;

        public CommandInterface(IView view)
        {
            _cursorLine = view.GetCursorPosition();
            CurrentCommand = "";
            HasCommandToProcess = false;
            HistoryManager = new CommandHistoryManager();
        }

        public void DoATurn(IView view, Board board)
        {
            Tick();
            if (HasCommandToProcess)
            {
                var commandString = GetCommand();
                board.ExecuteBoardCommand(GetCell(), GetCommand(commandString));
                Reset();
                view.DisplayBoard(board);
            }
        }

        public Coordinate GetCell()
        {
            return _click;
        }

        public BoardCommand GetCommand(string input)
        {
            var items = input.ToUpper().Split(',', ' ');
            if (items.Length == 3)
            {
                int col;
                int row;
                if (int.TryParse(items[1], out col))
                {
                    col--;
                    row = items[2][0] - 65;
                }
                else if (int.TryParse(items[2], out col))
                {
                    col--;
                    row = items[1][0] - 65;
                }
                else
                    return BoardCommand.UnknownCommand;
                _click = new Coordinate(col, row);
            }
            switch (items[0])
            {
                case "Q":
                    return BoardCommand.QuitGame;
                case "M":
                    return BoardCommand.MarkCell;
                case "C":
                    return BoardCommand.RevealCell;
                default:
                    return BoardCommand.UnknownCommand;
            }
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
            if (!HistoryManager.HasHistory())
                return;

            if (key == ConsoleKey.UpArrow)
                CurrentCommand = HistoryManager.GetPreviousCommand();
            else if (key == ConsoleKey.DownArrow)
                CurrentCommand = HistoryManager.GetNextCommand();
        }

        private void SubmitCommand()
        {
            HasCommandToProcess = true;
            HistoryManager.StoreCommand(CurrentCommand);
        }

        protected void ProcessKeyInfo(ConsoleKeyInfo keyInfo)
        {
            var key = keyInfo.Key;
            if ((key == ConsoleKey.UpArrow) || (key == ConsoleKey.DownArrow))
                GetCommandFromHistory(key);
            else if (key == ConsoleKey.Enter)
                SubmitCommand();
            else
                ModifyCurrentCommand(keyInfo);
            ClearCommand();
        }

        private void ModifyCurrentCommand(ConsoleKeyInfo keyInfo)
        {
            if ((keyInfo.Key == ConsoleKey.Backspace) || (keyInfo.Key == ConsoleKey.Delete))
                CurrentCommand = RemoveLastCharacter(CurrentCommand);
            else
                CurrentCommand = CurrentCommand + keyInfo.KeyChar;
            HistoryManager.SetWorkingBuffer(CurrentCommand);
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

    public interface ICommandInterface
    {
        void DoATurn(IView view, Board board);
    }
}