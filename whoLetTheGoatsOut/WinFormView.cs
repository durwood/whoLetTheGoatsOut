using System;
using System.Drawing;
using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    public class WinFormView : IView
    {
        public static int CellSize = 50;
        private readonly MainForm _mainForm;
        private readonly Board _board;
        private readonly WindowsCommandInterface _commandInterface;
        private Image _savedImage;
        private readonly Square[,] _squares = new Square[MainForm.BoardSize, MainForm.BoardSize];

        public WinFormView(MainForm mainForm, Board board)
        {
            _mainForm = mainForm;
            _board = board;
            _commandInterface = new WindowsCommandInterface();

            for (var row = 0; row < MainForm.BoardSize; ++row)
                for (var col = 0; col < MainForm.BoardSize; ++col)
                {
                    var sq = new Square
                    {
                        Row = row,
                        Col = col,
                        BackColor = Color.MediumSeaGreen,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(0 + col * CellSize, 80 + row * CellSize),
                        Name = $"Row{row}_Col{col}",
                        Size = new Size(CellSize, CellSize),
                        TabIndex = 2,
                        TabStop = false,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    sq.Click += Sq_Click;
                    sq.LoadIcon(BoardIcon.BlockingFence);
                    _mainForm.Controls.Add(sq);
                    _squares[row, col] = sq;
                }
        }

        public void Clear()
        {
        }

        public void DisplayBoard(Board board)
        {
            var cells = board.GetCells();

            for (var row = 0; row < MainForm.BoardSize; ++row)
                for (var col = 0; col < MainForm.BoardSize; ++col)
                {
                    var square = _squares[row, col];
                    var cell = cells[row, col];
                    if (cell.IsMarked)
                        square.LoadIcon(BoardIcon.MarkGoat);
                    if (cell.IsRevealed)
                        square.LoadIcon(GetBoardIcon(cell.NeighboringBombCount));
                    if(cell.IsLoser)
                        square.LoadGoatImage(1);
                }
        }

        private static BoardIcon GetBoardIcon(int cellNeighboringBombCount)
        {
            switch (cellNeighboringBombCount)
            {
                case 0:
                    return BoardIcon.Empty;
                case 1:
                    return BoardIcon.One;
                case 2:
                    return BoardIcon.Two;
                case 3:
                    return BoardIcon.Three;
                case 4:
                    return BoardIcon.Four;
                case 5:
                    return BoardIcon.Five;
                case 6:
                    return BoardIcon.Six;
                case 7:
                    return BoardIcon.Seven;
                case 8:
                    return BoardIcon.Eight;
                default:
                    throw new ArgumentException("cellNeigboringBombCount");
            }
        }

        public void StatusDisplay(int numBombs, int elapsedSec)
        {
        }

        public void Quit()
        {
        }

        public void Lose()
        {
        }

        public void Win()
        {
        }

        public void DisplayFooter(Board board)
        {
        }

        public void DisplayRow(string line)
        {
        }

        public int GetCursorPosition()
        {
            return 0;
        }

        private void Sq_Click(object sender, EventArgs e)
        {
            var mouseEvent = e as MouseEventArgs;
            var sq = sender as Square;
            if ((sq == null) || (mouseEvent == null))
                return;

            //if (mouseEvent?.Button == MouseButtons.Right)
            //    sq.Image = _savedImage;
            //else if (mouseEvent?.Button == MouseButtons.Left)
            //{
            //    _savedImage = sq.Image;
            //    sq.Image = null;
            //}


            var command = BoardCommand.UnknownCommand;
            //string text;
            if (mouseEvent.Button == MouseButtons.Right)
            {
                //text = $"Right-Clicked Col: {sq.Col}, Row: {sq.Row}\n Image Pasted to cell.";
                command = BoardCommand.MarkCell;
            }
            else if (mouseEvent.Button == MouseButtons.Left)
            {
                //text = $"Left-Clicked Col: {sq.Col}, Row: {sq.Row}\n Image Cut from cell.";
                command = BoardCommand.RevealCell;
            }
            //else
            //    text = $"{mouseEvent?.Button}-Clicked";

            var coordinate = new Coordinate(sq.Col, sq.Row);
            _commandInterface.SetMove(coordinate, command);
            _commandInterface.DoATurn(this, _board);

            //MessageBox.Show(text);
        }
    }
}