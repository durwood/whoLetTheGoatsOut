using System;

namespace bombsweeper
{
    public class Cell
    {
        public bool IsLoser;
        public bool IsMarked;
        public bool IsRevealed;

        public int NeighboringBombCount
        {
            get { return (_content == Bomb || _content == Empty) ? 0 : int.Parse(_content.ToString()); }
        }

        public Cell()
        {
            IsRevealed = false;
            HasBomb = false;
        }

        public int NeighboringBombCount { get; private set; }
        public bool HasBomb { get; private set; }

        public void Reveal()
        {
            if (!IsMarked)
                IsRevealed = true;
        }

        public void MarkAsLoser()
        {
            if (!HasBomb)
                throw new ArgumentException("IsLoser cell must contain bomb.");
            IsLoser = true;
        }

        public void AddBombsAroundCellCount(int number)
        {
            if (!HasBomb)
                NeighboringBombCount = number;
        }

        public void AddBomb()
        {
            HasBomb = true;
        }

        public void ClearContents()
        {
            HasBomb = false;
            NeighboringBombCount = 0;
        }

        public void ToggleMark()
        {
            if (!IsRevealed)
                IsMarked = !IsMarked;
        }

        public bool IsEmpty()
        {
            return !HasBomb && (NeighboringBombCount == 0);
        }
    }
}