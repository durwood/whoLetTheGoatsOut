using System;

namespace bombsweeper
{
    public class GameConsoleView
    {
        private readonly int _boardLine;
        private readonly Board _boardModel;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly int _statusLine;
        private int _elapsedSec;
        private int _numBombs;

        public GameConsoleView(Board boardModel)
        {
            _statusLine = 0;
            _boardModel = boardModel;
            _statusLine = 0;
            _boardLine = 2;
            CursorLine = _boardLine + boardModel.GetSize() + 2;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            Console.Clear();
            Console.CursorVisible = false;
        }

        public int CursorLine { get; }

        public void UpdateStatusDisplay()
        {
            var needToDisplay = false;
            var elapsedSec = _elapsedSecondsCalculator.ElapsedSec();
            if (elapsedSec != _elapsedSec)
            {
                needToDisplay = true;
                _elapsedSec = elapsedSec;
            }
            var numBombs = _boardModel.GetNumberOfUnmarkedBombs();
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

        public void ShowResult()
        {
            if (_boardModel.GameWon())
                Console.WriteLine("Congratulations, you won!");
            else if (_boardModel.GameLost())
                Console.WriteLine("Loser.");
            else
                Console.WriteLine("Quitter.");
        }

        public void DisplayBoard()
        {
            var boardConsoleView = new BoardConsoleView(_boardModel, _boardLine);
            Console.SetCursorPosition(0, _boardLine);
            boardConsoleView.DisplayBoard();
            Console.SetCursorPosition(0, CursorLine);
        }
    }
}