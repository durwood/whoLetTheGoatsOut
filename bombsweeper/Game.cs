using System;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly int _boardLine;
        private readonly int _cursorLine;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly InputGetter _inputGetter;
        private readonly int _statusLine;
        private string _commandString;


        public Game(InputGetter inputGetter, Board board)
        {
            Console.CursorVisible = false;
            _inputGetter = inputGetter;
            _board = board;
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
        }

        public void Run()
        {
            do
            {
                DisplayElapsedTime();
                DisplayBoard();
                DisplayCommandPrompt();
                ProcessCommand();
            } while (_board.GameInProgress());
            DisplayBoard();
            ShowResult();
        }

        private void DisplayElapsedTime()
        {
            var elapsedSec = _elapsedSecondsCalculator.NewElapsedSec();
            if (elapsedSec != null)
            {
                Console.SetCursorPosition(0, _statusLine);
                Console.WriteLine(elapsedSec.Value);
            }
        }

        // TODO: Only Display Board when new Board Command has been executed?
        // TODO: Change to return true and move ExecuteBoardCommand up?
        private void ProcessCommand()
        {
            if (!Console.KeyAvailable)
                return;
            var newKey = Console.ReadKey().KeyChar;
            if ((newKey == '\r') || (newKey == '\n'))
            {
                ExecuteBoardCommand();
                ClearCommand();
            }
            else if (newKey == '\b')
            {
                _commandString = RemoveLastCharacter(_commandString);
                ClearCommand();
            }
            else
                _commandString = _commandString + newKey;
        }

        private string RemoveLastCharacter(string str)
        {
            return str.Length > 0 ? str.Substring(0, str.Length - 1) : str;
        }

        private void DisplayCommandPrompt()
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write("> " + _commandString);
        }

        private void ExecuteBoardCommand()
        {
            var command = _inputGetter.GetCommand(_commandString);
            if (command != BoardCommand.UnknownCommand)
                if (command == BoardCommand.QuitGame)
                    _board.QuitGame();
                else
                {
                    var cell = _inputGetter.GetCell();
                    if (command == BoardCommand.RevealCell)
                        _board.Reveal(cell.X, cell.Y);
                    //else if (command == BoardCommand.MarkCell)
                    //    _board.Mark(cell.X, cell.Y);
                }
        }

        private void ShowResult()
        {
            if (_board.GameWon())
                Console.WriteLine("Congratulations, you won!");
            else if (_board.GameLost())
                Console.WriteLine("Loser.");
            else
                Console.WriteLine("Quitter.");
        }

        private void DisplayBoard()
        {
            Console.SetCursorPosition(0, _boardLine);
            Console.Write(_board.Display(true));
        }

        private void ClearCommand()
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}