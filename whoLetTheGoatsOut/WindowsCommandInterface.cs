using System;
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


       

        public void SetMove(Coordinate coordinate, BoardCommand command)
        {
            _cell = coordinate;
            _command = command;
        }
    }
}