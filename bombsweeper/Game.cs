using System;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly int _boardLine;
        private readonly int _cursorLine;
        private readonly InputGetter _inputGetter;
        private readonly int _statusLine;


        public Game(InputGetter inputGetter, Board board)
        {
            Console.CursorVisible = false;
            _inputGetter = inputGetter;
            _board = board;
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
        }

        public void Run()
        {
            double elapsedSec = 0;
            var startTime = DateTime.Now;
            var commandString = "";
            do
            {
                var elapsedTicks = DateTime.Now.Ticks - startTime.Ticks;
                var newElapsedSec = new TimeSpan(elapsedTicks).TotalSeconds;
                if ((int) newElapsedSec != (int) elapsedSec)
                {
                    elapsedSec = newElapsedSec;
                    DisplayElapsedTime((int) elapsedSec);
                }
                ShowBoard();
                commandString = UpdateCommand(commandString);
            } while (_board.GameInProgress());
            ShowBoard();
            ShowResult();
        }

        private void DisplayElapsedTime(int elapsedTime)
        {
            Console.SetCursorPosition(0, _statusLine);
            Console.WriteLine(elapsedTime);
        }

        private string UpdateCommand(string commandString)
        {
            ShowCommand(commandString);

            if (Console.KeyAvailable)
            {
                var newKey = Console.ReadKey().KeyChar;
                if ((newKey == '\r') || (newKey == '\n'))
                {
                    ExecuteBoardCommand(commandString);
                    ClearCommand();
                    return "";
                }
                return commandString + newKey;
            }
            return commandString;
        }

        private void ShowCommand(string curString)
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write("> " + curString);
        }


        private void ExecuteBoardCommand(string input)
        {
            var command = _inputGetter.GetCommand(input);
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

        private void ShowBoard()
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