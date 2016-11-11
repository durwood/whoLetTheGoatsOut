using System;

namespace bombsweeper
{
    public class Cell
    {
        public const char Block = '\u25A0';
        public const char Check = '\u2713';
        public const char Bomb = '*';
        public const char Empty = ' ';
        private char _content;
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
            _content = Empty;
        }

        public bool HasBomb()
        {
            return _content == Bomb;
        }

        public override string ToString()
        {
            return (IsRevealed ? _content : IsMarked ? Check : Block).ToString();
        }

        public char Reveal()
        {
            if (!IsMarked)
                IsRevealed = true;
            return _content;
        }

        public void MarkAsLoser()
        {
            if (!HasBomb())
                throw new ArgumentException("IsLoser cell must contain bomb.");
            IsLoser = true;
        }

        public void AddBombsAroundCellCount(int number)
        {
            if (!HasBomb())
                _content = number.ToString()[0];
        }

        public void AddBomb()
        {
            _content = Bomb;
        }

        public void ClearContents()
        {
            _content = Empty;
        }

        public void ToggleMark()
        {
            if (!IsRevealed)
                IsMarked = !IsMarked;
        }
    }
}