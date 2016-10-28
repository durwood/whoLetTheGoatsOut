using System;
using System.Threading.Tasks;

namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly InputGetter _inputGetter;

        public Game(InputGetter inputGetter, Board board)
        {
            _inputGetter = inputGetter;
            _board = board;
        }

        public void Run()
        {
            do
            {
                ShowBoard();
                var inputString = GetInput();
                ExecuteBoardCommand(inputString);
            } while (_board.GameInProgress());
            ShowBoard();
            ShowResult();
        }

        private async Task<string> GetInputAsync()
        {
            return await Task.Run(() => GetInput());
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
            Console.Clear();
            Console.Write(_board.Display(true));
        }

        private void GetClick()
        {
        }
    }
}