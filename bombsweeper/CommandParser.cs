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
            var items = input.Split(',', ' ');
            if (items.Length == 3)
                _click = new Coordinate {X = int.Parse(items[1]) - 1, Y = int.Parse(items[2]) - 1};
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