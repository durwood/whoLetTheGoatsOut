using System;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly CommandInterface _commandInterface;
        private readonly CommandParser _commandParser;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly IView _view;
        private int _elapsedSec;
        private int _numBombs;

        public Game(Board board, IView consoleView)
        {
            _commandParser = new CommandParser();
            _board = board;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _view = consoleView;
            _commandInterface = new CommandInterface(_view);
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
                    var commandString = _commandInterface.GetCommand();
                    ExecuteBoardCommand(_commandParser.GetCell(), _commandParser.GetCommand(commandString));
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

        private void ExecuteBoardCommand(Coordinate getCell, BoardCommand boardCommand)
        {
            var command = boardCommand;
            if (command != BoardCommand.UnknownCommand)
                if (command == BoardCommand.QuitGame)
                    _board.QuitGame();
                else
                {
                    var cell = getCell;
                    if (command == BoardCommand.RevealCell)
                        _board.Reveal(cell.Y, cell.X);
                    else if (command == BoardCommand.MarkCell)
                        _board.ToggleMark(cell.Y, cell.X);
                }
        }
    }
}