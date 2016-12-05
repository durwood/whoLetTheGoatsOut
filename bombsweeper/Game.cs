using System;
using System.Collections.Generic;

namespace bombsweeper
{
    public class ConsoleView : IView
    {
        private Board _board;
        private int _boardLine;
        private int _cursorLine;
        private int _statusLine;

        public void Initialize()
        {
            Console.CursorVisible = false;
        }

        public void SetBoard(Board board)
        {
            _board = board;
            _boardLine = 2;
            _statusLine = 0;
            _cursorLine = _boardLine + board.GetSize() + 2;
        }

        public void DisplayBoard()
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


        public void UpdateStatusDisplay(int elapsedSec)
        {
            var numBombs = _board.GetNumberOfUnmarkedBombs();
            var cursorTop = Console.CursorTop;
            var cursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine($"Bombs: {numBombs}  Elapsed Time: {elapsedSec}");
            //This is weird and we should something? test it? something?
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }
    }

    public interface IView
    {
        void Initialize();
        void SetBoard(Board board);
        void DisplayBoard();
        void UpdateStatusDisplay(int elapsedSec);
    }

    public class FormView : IView
    {
        public void Initialize()
        {
        }

        public void SetBoard(Board board)
        {
        }

        public void DisplayBoard()
        {
        }


        public void UpdateStatusDisplay(int elapsedSec)
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
        private readonly CommandInterface _commandInterface;
        private readonly CommandParser _commandParser;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly IView _view;

        public Game(Board board, IView view)
        {
            _view = view;
            view.Initialize();
            _commandParser = new CommandParser();
            _board = board;
            var boardLine = 2;
            var cursorLine = boardLine + board.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _commandInterface = new CommandInterface(cursorLine);
        }


        public static Game Create(Dictionary<string, bool> options, IView view)
        {
            var board = CreateBoard(options);
            view.SetBoard(board);
            return new Game(board, view);
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
            _view.DisplayBoard();
            do
            {
                UpdateStatusDisplay();
                _commandInterface.Tick();
                if (_commandInterface.HasCommandToProcess)
                {
                    ExecuteBoardCommand(_commandInterface.GetCommand());
                    _commandInterface.Reset();
                    _view.DisplayBoard();
                }
            } while (_board.GameInProgress());
            _view.DisplayBoard();
            ShowResult();
        }

        private void UpdateStatusDisplay()
        {
            var elapsedSec = _elapsedSecondsCalculator.ElapsedSec();
            _view.UpdateStatusDisplay(elapsedSec);
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
    }
}