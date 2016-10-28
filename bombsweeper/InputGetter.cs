namespace bombsweeper
{
    public struct BoardClick
    {
        public int X;
        public int Y;
    }

    public enum BoardCommand
    {
        QuitGame,
        RevealCell,
        MarkCell,
        UnknownCommand
    }

    public class InputGetter
    {
        private BoardClick _click;

        public virtual BoardClick GetCell()
        {
            return _click;
        }

        public virtual BoardCommand GetCommand(string input)
        {
            var items = input.Split(',', ' ');
            if (items.Length == 3)
                _click = new BoardClick {X = int.Parse(items[1]) - 1, Y = int.Parse(items[2]) - 1};
            switch (items[0])
            {
                case "q":
                    return BoardCommand.QuitGame;
                case "m":
                    return BoardCommand.MarkCell;
                case "c":
                    return BoardCommand.RevealCell;
                default:
                    return BoardCommand.UnknownCommand;
            }
        }
    }
}