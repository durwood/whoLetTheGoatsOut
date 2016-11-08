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
        private int _elapsedSec;
        private int _numBombs;
        private IUi _ui;

        public Game(Board board, IUi ui)
        {
            _ui = ui;
            _commandParser = new CommandParser();
            _board = board;
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _commandInterface = new CommandInterface(_cursorLine);
        }

        public void Run()
        {
            DisplayBoard();
            do
            {
                UpdateStatusDisplay();
                _commandInterface.Tick();
                if (_commandInterface.HasCommandToProcess)
                {
                    ExecuteBoardCommand(_commandInterface.GetCommand());
                    _commandInterface.Reset();
                    DisplayBoard();
                }
            } while (_board.GameInProgress());
            DisplayBoard();
            ShowResult();
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
            {
                Console.SetCursorPosition(0, _statusLine);
                Console.WriteLine($"Bombs: {_numBombs}  Elapsed Time: {_elapsedSec}");
            }
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

        private void ShowResult()
        {
            if (_board.GameWon())
                Console.WriteLine("Congratulations, you won!");
            else if (_board.GameLost())
                Console.WriteLine("IsLoser.");
            else
                Console.WriteLine("Quitter.");
        }

        private void DisplayBoard()
        {
            Console.SetCursorPosition(0, _boardLine);
            _board.Display();
            if (_board.GameLost())
            {
                int x, y;
                var cell = _board.GetLosingBombCell(out x, out y);
                var savedColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y + _boardLine);
                Console.Write(cell);
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, _cursorLine);
            }
        }
    }
}