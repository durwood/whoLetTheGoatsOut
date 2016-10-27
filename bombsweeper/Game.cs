using System;

namespace bombsweeper
{

    public class Game
	{
	    private readonly Board _board;
        private InputGetter _inputGetter;

		public Game(InputGetter inputGetter, int boardSize)
		{
            _inputGetter = inputGetter;
			_board = new Board(boardSize);
		}

		public void Run()
		{
			Console.Write(_board.Display());
            _inputGetter.GetClick();
		}

        private void GetClick()
        {

        }
	}
	
}
