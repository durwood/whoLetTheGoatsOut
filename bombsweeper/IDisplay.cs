namespace bombsweeper
{
    public interface IDisplay
    {
        void Init();
        void Display(Board board);
        void DisplayLose(Board board);
        void ShowResult(Board board);
        void UpdateStatus(int elapsedSec, int numBombs);
    }
}