namespace bombsweeper
{
    public interface IView
    {
        void Initialize();
        void SetBoard(Board board);
        void DisplayBoard();
        void UpdateStatusDisplay(int elapsedSec);
        void Win();
        void Lose();
        void Quit();
        void Clear();
    }
}