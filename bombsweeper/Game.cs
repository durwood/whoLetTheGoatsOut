using System;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly int _boardLine;
        private readonly CommandInterface _commandInterface;
        private readonly CommandParser _commandParser;
        private readonly int _cursorLine;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly int _statusLine;
        private readonly IView _view;
        private int _elapsedSec;
        private int _numBombs;

        public Game(Board board, IView consoleView)
        {
            Console.CursorVisible = false;
            _commandParser = new CommandParser();
            _board = board;
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _commandInterface = new CommandInterface(_cursorLine);
            _view = consoleView;
        }

        public void Run()
        {
            _view.Clear();
            _view.DisplayBoard(_board, _boardLine, _cursorLine);
            do
            {
                UpdateStatusDisplay();
                _commandInterface.Tick();
                if (_commandInterface.HasCommandToProcess)
                {
                    ExecuteBoardCommand(_commandInterface.GetCommand());
                    _commandInterface.Reset();
                    _view.DisplayBoard(_board, _boardLine, _cursorLine);
                }
            } while (_board.GameInProgress());
            _view.DisplayBoard(_board, _boardLine, _cursorLine);
            _view.ShowResult(_board);
        }

        private void UpdateStatusDisplay()
        {
            var needToDisplay = false;
            var elapsedSec = _elapsedSecondsCalculator.ElapsedSec();
            if (elapsedSec != _elapsedSec)
            {
                needToDisplay = true;
                _elapsedSec = elapsedSec;
            }
            var numBombs = _board.GetNumberOfUnmarkedBombs();
            if (numBombs != _numBombs)
            {
                needToDisplay = true;
                _numBombs = numBombs;
            }
            if (needToDisplay)
                _view.StatusDisplay(_numBombs, _elapsedSec, _statusLine);
        }

        private void ExecuteBoardCommand(string commandString)
        {
            var command = _commandParser.GetCommand(commandString);
            if (command != BoardCommand.UnknownCommand)
                if (command == BoardCommand.QuitGame)
                    _board.QuitGame();
                else
                {
                    var cell = _commandParser.GetCell();
                    if (command == BoardCommand.RevealCell)
                        _board.Reveal(cell.Y, cell.X);
                    else if (command == BoardCommand.MarkCell)
                        _board.ToggleMark(cell.Y, cell.X);
                }
        }
    }

    public class ConsoleView : IView
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void DisplayBoard(Board board, int boardLine, int cursorLine)
        {
            Console.SetCursorPosition(0, boardLine);
            board.Display();
            if (board.GameLost())
            {
                int x, y;
                var cell = board.GetLosingBombCell(out x, out y);
                var savedColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y + boardLine);
                Console.Write(cell);
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, cursorLine);
            }
        }

        public void ShowResult(Board board)
        {
            if (board.GameWon())
                Console.WriteLine("Congratulations, you won!");
            else if (board.GameLost())
                Console.WriteLine("IsLoser.");
            else
                Console.WriteLine("Quitter.");
        }

        public void StatusDisplay(int numBombs, int elapsedSec, int statusLine)
        {
            Console.SetCursorPosition(0, statusLine);
            Console.WriteLine($"Bombs: {numBombs}  Elapsed Time: {elapsedSec}");
        }
    }

    public interface IView
    {
        void Clear();
        void DisplayBoard(Board board, int boardLine, int cursorLine);
        void ShowResult(Board board);
        void StatusDisplay(int numBombs, int elapsedSec, int statusLine);
    }
}