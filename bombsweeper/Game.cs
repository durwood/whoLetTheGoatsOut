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
            _cursorLine = _boardLine + board.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _commandInterface = new CommandInterface(_cursorLine);
            _view = consoleView;
        }

        public void Run()
        {
            _view.Clear();
            _view.DisplayBoard(_board);
            do
            {
                UpdateStatusDisplay();
                _commandInterface.Tick();
                if (_commandInterface.HasCommandToProcess)
                {
                    ExecuteBoardCommand(_commandInterface.GetCommand());
                    _commandInterface.Reset();
                    _view.DisplayBoard(_board);
                }
            } while (_board.GameInProgress());
            _view.DisplayBoard(_board);
            ShowResult();
        }

        private void ShowResult()
        {
            if (_board.GameWon())
                _view.Win();
            else if (_board.GameLost())
                _view.Lose();
            else
                _view.Quit();
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
                _view.StatusDisplay(_numBombs, _elapsedSec);
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
}