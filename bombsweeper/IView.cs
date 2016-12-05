namespace bombsweeper
{
    public interface IView
    {
        void Clear();
        void DisplayBoard(Board board);
        void StatusDisplay(int numBombs, int elapsedSec);
        void Quit();
        void Lose();
        void Win();
        void DisplayFooter(Board board);
        void DisplayRow(string line);
        int GetCursorPosition();
    }
}