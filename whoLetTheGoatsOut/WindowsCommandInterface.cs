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
            view.DisplayBoard(board);
            //ToDo maybe this should be game.showResult()
            if (board.GameLost())
                view.Lose();
        }

        public void SetMove(Coordinate coordinate, BoardCommand command)
        {
            _cell = coordinate;
            _command = command;
        }
    }
}