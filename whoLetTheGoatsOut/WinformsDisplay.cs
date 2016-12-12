using System;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    internal class WinformsDisplay : IDisplay
    {
        private readonly MainForm _mainForm;
        private Game _game;

        public WinformsDisplay(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public void Init()
        {
        }

        public void Display(Board board)
        {
        }

        public void DisplayLose(Board board)
        {
        }

        public void ShowResult(Board board)
        {
        }

        public void UpdateStatus(int elapsedSec, int numBombs)
        {
            _mainForm.SetElapsedSeconds(elapsedSec);
        }

        public void Tick()
        {
        }

        public bool HasCommandToProcess { get; }
        public string GetCommand()
        {
            return "q";
        }

        public void Reset()
        {
        }

        public void Start(Game game)
        {
            _game = game;
        }

        public bool PumpOutputQueue(Action<string> executeBoardCommand)
        {
            return false;
        }
    }
}