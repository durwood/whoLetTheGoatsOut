using System;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly int _boardLine;
        private readonly int _cursorLine;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly CommandInterpreter _commandInterpreter;
        private readonly int _statusLine;
        private string _commandString;


        public Game(CommandInterpreter commandInterpreter, Board board)
        {
            Console.CursorVisible = false;
            _commandInterpreter = commandInterpreter;
            _board = board;
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
        }

        public void Run()
        {
            DisplayBoard();
            do
            {
                DisplayElapsedTime();
                DisplayCommandPrompt();
                if (ProcessCommand())
                {
                    ExecuteBoardCommand();
                    DisplayBoard();
                }
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

        private bool ProcessCommand()
        {
            if (!Console.KeyAvailable)
                return false;
            var keyInfo = Console.ReadKey();
            var keyChar = keyInfo.KeyChar;
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
            }
            else if ((keyChar == '\r') || (keyChar == '\n'))
            {
                ClearCommand();
                return true;
            }
            else if (keyChar == '\b')
            {
                _commandString = RemoveLastCharacter(_commandString);
                ClearCommand();
            }
            else
                _commandString = _commandString + keyChar;
            return false;
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
            var command = _commandInterpreter.GetCommand(_commandString);
            if (command != BoardCommand.UnknownCommand)
                if (command == BoardCommand.QuitGame)
                    _board.QuitGame();
                else
                {
                    var cell = _commandInterpreter.GetCell();
                    if (command == BoardCommand.RevealCell)
                        _board.Reveal(cell.X, cell.Y);
                    else if (command == BoardCommand.MarkCell)
                        _board.Mark(cell.X, cell.Y);
                }
            _commandString = "";
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