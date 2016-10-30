namespace bombsweeper
{
    public class Cell
    {
        public const char Block = '\u25A0';
        public const char Check = '\u2713';
        public const char Bomb = '*';
        public const char Empty = ' ';
        public char Content;
        public bool IsRevealed;

        public Cell()
        {
            IsRevealed = false;
            Content = Empty;
        }

        public bool HasBomb()
        {
            return Content == Bomb;
        }

        public string Display()
        {
            return (IsRevealed ? Content : Block).ToString();
        }

        public char Reveal()
        {
            IsRevealed = true;
            return Content;
        }

        internal void AddAdjacencyNumber(int number)
        {
            Content = number.ToString()[0];
        }

        public void AddBomb()
        {
            Content = Bomb;
        }

        public void ClearContents()
        {
            Content = Empty;
        }
    }
}