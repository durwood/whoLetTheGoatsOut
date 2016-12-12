using System;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    internal class WinformsDisplay : IDisplay
    {
        private readonly MainForm _mainForm;

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
    }
}