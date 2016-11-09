namespace bombsweeper
{
    public interface IUi
    {
        void HighlightLosingCell(int x, int y, Cell cell);
        void Display(Board board);
        void UpdateStatus(int bombs, int sec);
    }
}