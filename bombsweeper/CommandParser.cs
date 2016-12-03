namespace bombsweeper
{
    public enum BoardCommand
    {
        QuitGame,
        RevealCell,
        MarkCell,
        UnknownCommand
    }

    public class CommandParser
    {
        private Coordinate _click;

        public virtual Coordinate GetCell()
        {
            return _click;
        }

        public virtual BoardCommand GetCommand(string input)
        {
            var items = input.ToUpper().Split(',', ' ');
            if (items.Length == 3)
            {
                int col;
                int row;
                if (int.TryParse(items[1], out col))
                {
                    col--;
                    row = (int)(items[2][0]) - 65;
                }
                else if (int.TryParse(items[2], out col))
                {
                    col--;
                    row = (int)(items[1][0]) - 65;
                }
                else
                    return BoardCommand.UnknownCommand;
                _click = new Coordinate { X = col, Y = row };
            }
            switch (items[0])
            {
                case "Q":
                    return BoardCommand.QuitGame;
                case "M":
                    return BoardCommand.MarkCell;
                case "C":
                    return BoardCommand.RevealCell;
                default:
                    return BoardCommand.UnknownCommand;
            }
        }
    }
}