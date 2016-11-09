using System;
using System.Drawing;
using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    public partial class MainForm : Form
    {
        public static int NumGoats = 38;
        public static int CellSize = 50;
        private readonly Board _board;
        private readonly Random _random = new Random();
        private Image _savedImage;
        private WinformCellView[,] _winformCellViews;

        public MainForm(Board board)
        {
            InitializeComponent();
            _board = board;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            _winformCellViews = new WinformCellView[_board.GetSize(), _board.GetSize()];
            var cells = _board.GetCells();
            for (var row = 0; row < _board.GetSize(); ++row)
                for (var col = 0; col < _board.GetSize(); ++col)
                {
                    var cell = new WinformCellView
                    {
                        Row = row,
                        Col = col,

                        ModelCell = cells[row, col],

                        BackColor = Color.MediumSeaGreen,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(0 + row*CellSize, 80 + col*CellSize),
                        Name = $"Row{row}_Col{col}",
                        Size = new Size(CellSize, CellSize),
                        TabIndex = 2,
                        TabStop = false,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    cell.LoadIcon(BoardIcon.BlockingFence);
                    cell.Click += Sq_Click;
                    Controls.Add(cell);
                    _winformCellViews[row, col] = cell;
                }
        }

        private void Sq_Click(object sender, EventArgs e)
        {
            var mouseEvent = e as MouseEventArgs;
            var sq = sender as WinformCellView;
            if ((sq == null) || (mouseEvent == null))
                return;

            if (mouseEvent?.Button == MouseButtons.Right)
            {
                _board.ToggleMark(sq.Row, sq.Col);
                UpdateBoardDisplay();
            }
            else if (mouseEvent?.Button == MouseButtons.Left)
            {
                _board.Reveal(sq.Row, sq.Col);
                UpdateBoardDisplay();
            }

            //var result = $"{mouseEvent.Button}-Clicked on ({sq.Col}, {sq.Row})";
            //MessageBox.Show(result);
        }

        private void UpdateBoardDisplay()
        {
            foreach (var cell in _winformCellViews)
                UpdateCellDisplay(cell);
        }

        private void UpdateCellDisplay(WinformCellView cell)
        {
            var modelCell = cell.ModelCell;
            if (modelCell.IsRevealed)
                if (modelCell.HasBomb)
                    cell.LoadGoatImage(47);
                else if (modelCell.NeighboringBombCount > 0)
                    cell.LoadBombCount(modelCell.NeighboringBombCount);
                else
                    cell.Image = null;
            else if (modelCell.IsMarked)
                cell.LoadIcon(BoardIcon.MarkGoat);
            else
                cell.LoadIcon(BoardIcon.BlockingFence);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            //PreviewGoatsAndIcons();
        }

        private void PreviewGoatsAndIcons()
        {
            var icons = (BoardIcon[]) Enum.GetValues(typeof(BoardIcon));

            var iconIdx = 0;
            var goatIdx = 0;
            foreach (var square in _winformCellViews)
                if (iconIdx < icons.Length)
                    square.LoadIcon(icons[iconIdx++]);
                else if (goatIdx < NumGoats)
                    square.LoadGoatImage(goatIdx++);
                else
                    break;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}