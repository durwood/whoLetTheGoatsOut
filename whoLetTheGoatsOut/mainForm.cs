using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using bombsweeper;

namespace bombsweeperWinform
{
    public partial class MainForm : Form, IUi
    {
        public static int BoardSize = 9;
        public static int NumGoats = 38;
        public static int CellSize = 50;
        private readonly Random _random = new Random();
        private readonly Square[,] _squares = new Square[BoardSize, BoardSize];
        private Image _savedImage;
        private readonly Game _game;

        public MainForm()
        {
            InitializeComponent();
            _game = new Game(BoardGenerator.Build9(), this, new WindowsCommandInterface());
            for (var col = 0; col < 9; ++col)
                for (var row = 0; row < 9; ++row)
                {
                    var sq = new Square
                    {
                        XPos = row,
                        YPos = col,
                        BackColor = Color.MediumSeaGreen,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(0 + row*CellSize, 80 + col*CellSize),
                        Name = $"Row{row}_Col{col}",
                        Size = new Size(CellSize, CellSize),
                        TabIndex = 2,
                        TabStop = false,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    sq.Click += Sq_Click;
                    Controls.Add(sq);
                    _squares[col, row] = sq;
                }
        }

        private void Sq_Click(object sender, EventArgs e)
        {
            var mouseEvent = e as MouseEventArgs;
            var sq = sender as Square;
            if ((sq == null) || (mouseEvent == null))
                return;

            if (mouseEvent?.Button == MouseButtons.Right)
                //sq.LoadIcon(BoardIcon.MarkGoat);
                sq.Image = _savedImage;
            else if (mouseEvent?.Button == MouseButtons.Left)
            {
                //sq.LoadGoatImage(_random.Next(1, NumGoats));
                _savedImage = sq.Image;
                sq.Image = null;
            }

            var result = $"{mouseEvent?.Button}-Clicked on ({sq?.XPos}, {sq?.YPos})";
            MessageBox.Show(result);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            var thread = new Thread(() => _game.Run());
            FormClosing += (object sender1, FormClosingEventArgs e1) => thread.Abort();
            thread.Start();
            //PreviewGoatsAndIcons();
        }

        private void PreviewGoatsAndIcons()
        {
            var icons = (BoardIcon[]) Enum.GetValues(typeof(BoardIcon));

            var iconIdx = 0;
            var goatIdx = 0;
            foreach (var square in _squares)
            {
                if (iconIdx < icons.Length)
                    square.LoadIcon(icons[iconIdx++]);
                else if (goatIdx < NumGoats)
                    square.LoadGoatImage(goatIdx++);
                else
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void HighlightLosingCell(int x, int y, Cell cell)
        {
        }

        public void Display(Board board)
        {
            board.Display(this);
        }

        public void UpdateStatus(int bombs, int sec)
        {
        }

        public void UpdateRow(int row, Cell[] rowOfCells)
        {
            int column = 0;
            foreach (var cell in rowOfCells)
            {
                if (cell.IsRevealed)
                    if (cell.HasBomb())
                        _squares[row, column].LoadGoatImage(5);
                    else if (cell.ToString() == " ")
                        _squares[row, column].Image = null;
                    else
                        _squares[row, column].LoadIcon(GetIconForContent(cell.ToString()));
                else if (cell.IsMarked)
                    _squares[row, column].LoadIcon(BoardIcon.MarkGoat);
                else
                    _squares[row, column++].LoadIcon(BoardIcon.BlockingFence);
            }
        }

        private BoardIcon GetIconForContent(string value)
        {
            var icons = (BoardIcon[])Enum.GetValues(typeof(BoardIcon));
            var index = int.Parse(value);
            return icons[index];

        }

        public void DisplayFooter(int size)
        {
        }
    }
}