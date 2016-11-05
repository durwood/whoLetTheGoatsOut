using System;

namespace bombsweeper
{
    public class Cell
    {
        public const char Block = '\u25A0';
        public const char Check = '\u2713';
        public const char Bomb = '*';
        public const char Empty = ' ';
        public char Content;
        public bool IsMarked;
        public bool IsRevealed;
        public bool Loser;

        public Cell()
        {
            IsRevealed = false;
            Content = Empty;
        }

        public bool HasBomb()
        {
            return Content == Bomb;
        }

        public override string ToString()
        {
            return (IsRevealed ? Content : IsMarked ? Check : Block).ToString();
        }

        public char Reveal()
        {
            IsRevealed = true;
            if (IsMarked)
                IsMarked = false;
            return Content;
        }

        internal void MarkAsLoser()
        {
            Loser = true;
            if (!HasBomb())
                throw new ArgumentException("Loser cell must contain bomb.");
        }

        public void AddAdjacencyNumber(int number)
        {
            if (!HasBomb())
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

        public void ToggleMark()
        {
            if (!IsRevealed)
                IsMarked = !IsMarked;
        }
    }
}