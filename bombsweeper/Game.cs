using System;

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
                var command = _inputGetter.GetCommand();
                if (command == UserCommand.QuitGame)
                    break;
                var cell = _inputGetter.GetCell();
                if (command == UserCommand.RevealCell)
                    _board.Reveal(cell.X, cell.Y);
                //else if (command == UserCommand.MarkCell)
                //    _board.Mark(cell.X, cell.Y);
            } while (_board.GameInProgress());
            ShowBoard();
            ShowResult();
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