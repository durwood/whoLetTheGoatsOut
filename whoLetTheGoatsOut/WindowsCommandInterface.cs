using bombsweeper;

namespace whoLetTheGoatsOut
{
    public class WindowsCommandInterface : ICommandInterface
    {
        private Coordinate _cell;
        private BoardCommand _command;

        public void DoATurn(IView view, Board board)
        {
            board.ExecuteBoardCommand(_cell, _command);
        }


        public void SetCell(int col, int row)
        {
            _cell = new Coordinate(col, row);
        }

        public void SetCommand(BoardCommand command)
        {
            _command = command;
        }
    }
}