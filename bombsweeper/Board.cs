using System;
using System.Linq;

namespace bombsweeper
{
    internal enum GameState
    {
        InProgress,
        Won,
        Lost,
        Quitted
    }

    public class Board
    {
        private const int LabelAllowance = 3;
        private readonly Cell[,] _cells;
        private readonly int _size;
        private GameState _gameState;
        private int _numBombs;
        private int _numMarked;

        public Board(int size)
        {
            _size = size;
            _cells = new Cell[size, size];
            for (var row = 0; row < size; ++row)
                for (var col = 0; col < size; ++col)
                    _cells[row, col] = new Cell();
            _gameState = GameState.InProgress;
        }

        public void AddBomb(int row, int col)
        {
            _cells[row, col].AddBomb();
            _numBombs++;
            PopulateAdjacencyCounts();
        }

        private void PopulateAdjacencyCounts()
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                {
                    if (_cells[row, col].HasBomb())
                        continue;
                    var bombsAroundCellCount = CountBombsAroundCell(row, col);
                    if (bombsAroundCellCount == 0)
                        _cells[row, col].ClearContents();
                    else
                        _cells[row, col].AddBombsAroundCellCount(bombsAroundCellCount);
                }
        }

        public Cell[,] GetCells()
        {
            return _cells;
        }

        private int CountBombsAroundCell(int row0, int col0)
        {
            var count = 0;
            for (var row = row0 - 1; row <= row0 + 1; ++row)
                for (var col = col0 - 1; col <= col0 + 1; ++col)
                {
                    if (NotValidCell(row, col) || IsSameCell(row0, col0, row, col))
                        continue;
                    if (_cells[row, col].HasBomb())
                        count++;
                }
            return count;
        }

        private static bool IsSameCell(int row0, int col0, int row1, int col1)
        {
            return (col1 == col0) && (row1 == row0);
        }

        private bool NotValidCell(int row, int col)
        {
            return (row < 0) || (row > _size - 1) || (col < 0) || (col > _size - 1);
        }

        public Cell GetLosingBombCell(out int x, out int y)
        {
            for (var row = 0; row < _size; ++row)
                for (var col = 0; col < _size; ++col)
                    if (_cells[row, col].IsLoser)
                    {
                        x = col;
                        y = row;
                        GetConsoleXCoordinate(ref x);
                        return _cells[row, col];
                    }
            x = 0;
            y = 0;
            return null;
        }

        private static void GetConsoleXCoordinate(ref int x)
        {
            x = LabelAllowance + 1 + x*2;
        }

        public void Display(IView view)
        {
            for (var row = 0; row < _size; ++row)
                DisplayRow(row, view);
            view.DisplayFooter(this);
        }

        private void DisplayRow(int row, IView view)
        {
            char rowLabel = (char)(65 + row);
            var rowString = string.Join(" ", GetRow(row).Select(c => c.ToString()));
            var line = string.Join(" ", $"{rowLabel,LabelAllowance}", $"{rowString}");
            view.DisplayRow(line);
        }

        private Cell[] GetRow(int row)
        {
            var offset = row*_size;
            return _cells.Cast<Cell>().Skip(offset).Take(_size).ToArray();
        }

        public override string ToString()
        {
            var cellStrings = _cells.Cast<Cell>().Select(cell => cell.ToString());
            return string.Join(" ", cellStrings);
        }

        public bool GameWon()
        {
            return _gameState == GameState.Won;
        }

        public bool GameLost()
        {
            return _gameState == GameState.Lost;
        }

        public bool GameInProgress()
        {
            return _gameState == GameState.InProgress;
        }

        public void QuitGame()
        {
            _gameState = GameState.Quitted;
        }

        public void Reveal(int row, int col)
        {
            var cell = _cells[row, col];
            var content = cell.Reveal();
            if (cell.IsRevealed)
                if (content == Cell.Bomb)
                {
                    cell.MarkAsLoser();
                    _gameState = GameState.Lost;
                    RevealAllBombs();
                }
                else
                {
                    RevealNeighbors(row, col);
                    if (NoFurtherCellsToReveal())
                        _gameState = GameState.Won;
                }
        }

        private bool NoFurtherCellsToReveal()
        {
            return !_cells.Cast<Cell>().Any(cell => !cell.IsRevealed && !cell.HasBomb());
        }

        private void RevealAllBombs()
        {
            foreach (var cell in _cells)
                if (!cell.IsMarked && cell.HasBomb())
                    cell.Reveal();
        }

        private void RevealNeighbors(int row0, int col0)
        {
            for (var row = row0 - 1; row <= row0 + 1; ++row)
                for (var col = col0 - 1; col <= col0 + 1; ++col)
                {
                    if (NotValidCell(row, col) || IsSameCell(row0, col0, row, col))
                        continue;

                    var cell = _cells[row, col];
                    if (!cell.HasBomb() && !cell.IsRevealed)
                    {
                        var content = cell.Reveal();
                        if (cell.IsRevealed && content == Cell.Empty)
                            RevealNeighbors(row, col);
                    }
                }
        }

        private void DisplayFooter()
        {
            if (_size > 9)
                DisplayFooterTens();
            DisplayFooterOnes();
        }

        private void DisplayFooterOnes()
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < _size; ++col)
                Console.Write($"{(col + 1)%10} ");
            Console.WriteLine();
        }

        private void DisplayFooterTens()
        {
            Console.Write($"{"",LabelAllowance + 1}");
            for (var col = 0; col < _size; ++col)
                Console.Write($"{(col + 1)/10} ");
            Console.WriteLine();
        }

        public int GetSize()
        {
            return _size;
        }

        public void ToggleMark(int row, int col)
        {
            _cells[row, col].ToggleMark();
            var markedCells = from Cell item in _cells where item.IsMarked select item;
            _numMarked = markedCells.Count();
        }

        public int GetNumberOfUnmarkedBombs()
        {
            return Math.Max(_numBombs - _numMarked, 0);
        }

        public void ExecuteBoardCommand(Coordinate getCell, BoardCommand boardCommand)
        {
            var command = boardCommand;
            if (command != BoardCommand.UnknownCommand)
                if (command == BoardCommand.QuitGame)
                    QuitGame();
                else
                {
                    var cell = getCell;
                    if (command == BoardCommand.RevealCell)
                        Reveal(cell.Y, cell.X);
                    else if (command == BoardCommand.MarkCell)
                        ToggleMark(cell.Y, cell.X);
                }
        }
    }
}