using System;
using System.Threading.Tasks;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly InputGetter _inputGetter;
        private int _statusLine = 0;
        private int _boardLine = 2;
        private int _cursorLine;


        public Game(InputGetter inputGetter, Board board)
        {
            _inputGetter = inputGetter;
            _board = board;
            _statusLine = 0;
            _boardLine = 2;
            _cursorLine = _boardLine + board.GetSize() + 2;
        }

        public void Run()
        {
            int seconds = 0;
            do
            {
                DisplayElapsedTime(seconds++);
                System.Threading.Thread.Sleep(1000);
                TickBoardAsync();
            }
            while (_board.GameInProgress());
            ShowBoard();
            ShowResult();
        }

        private void DisplayElapsedTime(int elapsedTime)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            Console.SetCursorPosition(0,_statusLine);
            Console.WriteLine(elapsedTime);
            Console.SetCursorPosition(left, top);
        }
        private void TickBoard()
        {
            ShowBoard();
            Console.SetCursorPosition(0, _cursorLine);
            var inputString = GetInput();
            ExecuteBoardCommand(inputString);
        }

        private async void TickBoardAsync()
        {
            ShowBoard();
            var inputString = await Task.Run(() => GetInput());
            ExecuteBoardCommand(inputString);
        }

        private string GetInput()
        {
            return Console.ReadLine();
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
            Console.SetCursorPosition(0,_boardLine);
            Console.Write(_board.Display(true));
        }

        private void ClearCommand()
        {
            Console.SetCursorPosition(0, _cursorLine);
            Console.Write(new string(' ', Console.WindowWidth));
        }
    }
}