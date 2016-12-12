using System;

namespace bombsweeper
{
    public class ConsoleWrapper
    {
        public virtual bool KeyAvailable()
        {
            return Console.KeyAvailable;
        }

        public virtual ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public virtual void SetCursorPosition(int i, int cursorLine)
        {
            Console.SetCursorPosition(i, cursorLine);
        }

        public virtual void Write(string s)
        {
            Console.Write(s);
        }

        public virtual void WriteToWidth(char c)
        {
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
    public class ConsoleOutput : IDisplay
    {
        private readonly int _cursorLine;
        public readonly CommandHistoryManager HistoryManager;
        public string CurrentCommand;
        public bool HasCommandToProcess { get; set; }
        public ConsoleWrapper _consoleWrapper = new ConsoleWrapper();
        public ConsoleOutput(int cursorLine)
        {
            _cursorLine = cursorLine;
            CurrentCommand = "";
            HasCommandToProcess = false;
            HistoryManager = new CommandHistoryManager();
        }

        public string GetCommand()
        {
            return CurrentCommand;
        }

        public virtual void Tick()
        {
            if (_consoleWrapper.KeyAvailable())
                ProcessKeyInfo(_consoleWrapper.ReadKey());
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
            _consoleWrapper.SetCursorPosition(0, _cursorLine);
            _consoleWrapper.WriteToWidth(' ');
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

        public bool PumpOutputQueue(Action<string> executeBoardCommand)
        {
            if (HasCommandToProcess)
            {
                executeBoardCommand(GetCommand());
                return true;
            }
            return false;
        }

        public void Start(Game game)
        {
            do
            {
                game.DoMove();
            } while (game.GameInProgress());
        }

        private void RefreshDisplay()
        {
            _consoleWrapper.SetCursorPosition(0, _cursorLine);
            _consoleWrapper.Write("> " + CurrentCommand);
        }

        private const int LabelAllowance = 4;
        private const int _statusLine = 0;

        public const int BoardLine = 2;

        public void ShowResult(Board board)
        {
            if (board.GameWon())
                Console.WriteLine("Congratulations, you won!");
            else if (board.GameLost())
                Console.WriteLine("IsLoser.");
            else
                Console.WriteLine("Quitter.");
        }

        public void UpdateStatus(int elapsedSec, int numBombs)
        {
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine($"Bombs: {numBombs}  Elapsed Time: {elapsedSec}");
        }

        public virtual void Init()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }

        public void Display(Board board)
        {
            var size = board.GetSize();
            for (var row = 0; row < size; ++row)
                DisplayRow(board, row);
            DisplayFooter(size);
        }

        public virtual void DisplayLose(Board board)
        {
            int x, y;
            var cell = GetLosingBombCell(board.GetCells(), board.GetSize(), out x, out y);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y + BoardLine);
            Console.Write(cell);
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, CalcCursorLine(board));
        }

        private void GetConsoleXCoordinate(ref int x)
        {
            x = LabelAllowance + 1 + x*2;
        }

        private void DisplayRow(Board board, int row)
        {
            var cells = board.GetRow(row);

            var rowLabel = (char) (65 + row);
            var lineStart = $"{rowLabel,LabelAllowance - 1} ";
            Console.SetCursorPosition(0, BoardLine + row);
            Console.Write(lineStart);

            for (var col = 0; col < cells.Length; col++)
                DisplayCell(col, row, cells[col]);

            Console.WriteLine();
        }

        private void DisplayCell(int col, int row, Cell cell)
        {
            Console.SetCursorPosition(LabelAllowance + col*2, BoardLine + row);
            Console.Write(cell + " ");
        }

        private void DisplayFooter(int size)
        {
            if (size > 9)
                DisplayFooterTens(size);
            DisplayFooterOnes(size);
        }

        private void DisplayFooterOnes(int size)
        {
            Console.Write($"{"",LabelAllowance}");
            for (var col = 0; col < size; ++col)
                Console.Write($"{(col + 1)%10} ");
            Console.WriteLine();
        }

        private void DisplayFooterTens(int size)
        {
            Console.Write($"{"",LabelAllowance}");
            for (var col = 0; col < size; ++col)
                Console.Write($"{(col + 1)/10} ");
            Console.WriteLine();
        }

        private Cell GetLosingBombCell(Cell[,] cells, int size, out int x, out int y)
        {
            for (var row = 0; row < size; ++row)
                for (var col = 0; col < size; ++col)
                    if (cells[row, col].IsLoser)
                    {
                        x = col;
                        y = row;
                        GetConsoleXCoordinate(ref x);
                        return cells[row, col];
                    }
            x = 0;
            y = 0;
            return null;
        }

        public static int CalcCursorLine(Board board)
        {
            return BoardLine + board.GetSize() + 2;
        }
    }
}