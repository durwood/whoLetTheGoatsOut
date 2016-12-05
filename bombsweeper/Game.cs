using System;
using System.Collections.Generic;

namespace bombsweeper
{
    public class ConsoleView : IView
    {
        public void Initialize()
        {
            Console.CursorVisible = false;
        }
    }

    public interface IView
    {
        void Initialize();
    }

    public class FormView : IView
    {
        public void Initialize()
        {
        }
    }

    public class Game
    {
        public static Dictionary<string, bool> DefaultArguments = new Dictionary<string, bool>
        {
            {"newBoard", false},
            {"simpleBoard", false}
        };

        private readonly Board _board;
        private readonly int _boardLine;
        private readonly CommandInterface _commandInterface;
        private readonly CommandParser _commandParser;
        private readonly int _cursorLine;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly int _statusLine;
        private int _elapsedSec;
        private int _numBombs;

        public Game(Board board, IView view)
        {
            view.Initialize();
            _commandParser = new CommandParser();
            _board = board;
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _commandInterface = new CommandInterface(_cursorLine);
        }


        public static Game Create(Dictionary<string, bool> options, IView view)
        {
            var board = CreateBoard(options);
            var game = new Game(board, view);
            return game;
        }

        private static Board CreateBoard(IReadOnlyDictionary<string, bool> options)
        {
            Board board;
            if (options["newBoard"])
            {
                var rnd = new RandomGenerator();
                var boardGenerator = new BoardGenerator(rnd);
                board = boardGenerator.GenerateBoard(9, 10);
            }
            else
                board = BoardGenerator.GetStandardBoard();
            return board;
        }

        public void Run()
        {
            Console.Clear();
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
                Console.BackgroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y + _boardLine);
                Console.Write(cell);
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, _cursorLine);
            }
        }
    }
}