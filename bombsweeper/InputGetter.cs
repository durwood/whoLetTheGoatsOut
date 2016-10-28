using System;

namespace bombsweeper
{
    public struct BoardClick
    {
        public int X;
        public int Y;
    }

    public enum UserCommand
    {
        QuitGame, RevealCell, MarkCell,
        UnknownCommand
    }

    public class InputGetter
    {
        private BoardClick _click;

        virtual public BoardClick GetCell()
        {
            return _click;
        }

        virtual public UserCommand GetCommand()
        {
            var line = Console.ReadLine();
            var items = line.Split(',', ' ');
            if (items.Length == 3)
                _click = new BoardClick { X = int.Parse(items[1]) - 1, Y = int.Parse(items[2]) - 1 };
            switch (items[0])
            {
                case "q": return UserCommand.QuitGame;
                case "m": return UserCommand.MarkCell;
                case "c": return UserCommand.RevealCell;
                default: return UserCommand.UnknownCommand;
            }          
        }
    }
}