using System;

namespace bombsweeper
{
    public class ConsoleGameController : IGameController
    {
        private readonly Board _board;
        private readonly CommandInterface _commandInterface;
        private readonly CommandParser _commandParser;
        private readonly GameConsoleView _gameConsoleView;

        public ConsoleGameController(Board board)
        {
            _board = board;
            _commandParser = new CommandParser();
            _gameConsoleView = new GameConsoleView(board);
            _commandInterface = new CommandInterface(_gameConsoleView.CursorLine);
        }

        public void Run()
        {
            _gameConsoleView.DisplayBoard();
            do
            {
                _gameConsoleView.UpdateStatusDisplay();
                _commandInterface.Tick();
                if (_commandInterface.HasCommandToProcess)
                {
                    ExecuteBoardCommand(_commandInterface.GetCommand());
                    _commandInterface.Reset();
                    _gameConsoleView.DisplayBoard();
                }
            } while (_board.GameInProgress());
            _gameConsoleView.DisplayBoard();
            _gameConsoleView.ShowResult();
            Console.ReadKey();
        }

        private void ExecuteBoardCommand(string commandString)
        {
            var command = _commandParser.GetCommand(commandString);
            if (command != BoardCommand.UnknownCommand)
                if (command == BoardCommand.QuitGame)
                    _board.QuitGame();
                else
                {
                    var cell = _commandParser.GetCell();
                    if (command == BoardCommand.RevealCell)
                        _board.Reveal(cell.Y, cell.X);
                    else if (command == BoardCommand.MarkCell)
                        _board.ToggleMark(cell.Y, cell.X);
                }
        }
    }
}