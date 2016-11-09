using System;

namespace bombsweeper
{
    public class CellConsoleView
    {
        public const string Block = "\u25A0";
        public const string Check = "\u2713";
        public const string Bomb = "*";
        public const string Empty = " ";
        private readonly Cell _cellModel;

        public CellConsoleView(Cell cellModel)
        {
            _cellModel = cellModel;
        }

        public void DisplayCell()
        {
            Console.Write(GetDisplayString());
        }

        private string GetDisplayString()
        {
            string outString;
            if (_cellModel.IsRevealed)
                if (_cellModel.HasBomb)
                    outString = Bomb;
                else if (_cellModel.IsEmpty())
                    outString = Empty;
                else
                    outString = _cellModel.NeighboringBombCount.ToString();
            else if (_cellModel.IsMarked)
                outString = Check;
            else
                outString = Block;
            return outString;
        }
    }
}