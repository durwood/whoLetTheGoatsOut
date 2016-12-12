using System;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly CommandParser _commandParser;
        private readonly int _cursorLine;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private int _elapsedSec;
        private int _numBombs;
        private IDisplay _output;

        public Game(Board board)
        {
            _commandParser = new CommandParser();
            _board = board;
            _cursorLine = ConsoleOutput.CalcCursorLine(board);
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _output = new ConsoleOutput(_cursorLine);
        }

        public void SetOutput(IDisplay output)
        {
            _output = output;
        }

        public void Run()
        {
            _output.Init();
            DisplayBoard();
            _output.Start(this);
            DisplayBoard();
            _output.ShowResult(_board);
        }

        public bool GameInProgress()
        {
            return _board.GameInProgress();
        }

        public void DoMove()
        {
            UpdateStatusDisplay();
            _output.Tick();
            Action<string> executeBoardCommand = ExecuteBoardCommand;

            if (_output.PumpOutputQueue(executeBoardCommand))
            {
                _output.Reset();
                DisplayBoard();
            }
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
                _output.UpdateStatus(_elapsedSec, _numBombs);
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


        private void DisplayBoard()
        {
            _output.Display(_board);
            if (_board.GameLost())
                _output.DisplayLose(_board);
        }
    }
}