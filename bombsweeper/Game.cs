namespace bombsweeper
{
    public class Game
    {
        private readonly Board _board;
        private readonly ICommandInterface _commandInterface;
        private readonly ElapsedSecondsCalculator _elapsedSecondsCalculator;
        private readonly IView _view;
        private int _elapsedSec;
        private int _numBombs;

        public Game(Board board, IView consoleView, ICommandInterface commandInterface)
        {
            _board = board;
            _elapsedSecondsCalculator = new ElapsedSecondsCalculator();
            _view = consoleView;
            _commandInterface = commandInterface;
        }

        public void Run()
        {
            _view.Clear();
            _view.DisplayBoard(_board);
            do
            {
                UpdateStatusDisplay();
                _commandInterface.DoATurn(_view, _board);
            } while (_board.GameInProgress());
            _view.DisplayBoard(_board);
            ShowResult();
        }

        public void ShowResult()
        {
            if (_board.GameWon())
                _view.Win();
            else if (_board.GameLost())
                _view.Lose();
            else
                _view.Quit();
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
                _view.StatusDisplay(_numBombs, _elapsedSec);
        }
    }
}