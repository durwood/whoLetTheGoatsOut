using System;
using System.Drawing;
using System.Security;
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
        private readonly Game _game;
        private readonly Random _random = new Random();
        private readonly Square[,] _squares = new Square[BoardSize, BoardSize];
        private int _goatCounter;
        private readonly WindowsCommandInterface _commandInterface;

        public MainForm()
        {
            InitializeComponent();
            _commandInterface = new WindowsCommandInterface();
            _game = new Game(BoardGenerator.Build9(), this, _commandInterface);
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

        public void HighlightLosingCell(int x, int y, Cell cell)
        {
        }

        public void Display(Board board)
        {
            board.Display(this);
        }

        private delegate void UpdateStatusCallback(int bombs, int sec);
        public void UpdateStatus(int bombs, int sec)
        {
            if (HiddenGoatCount.InvokeRequired)
            {
                Invoke(new UpdateStatusCallback(UpdateStatus), new object[] {bombs, sec});
            }
            else
            {
                HiddenGoatCount.Text = bombs.ToString();
                ElapsedTime.Text = sec.ToString();
            }
        }

        public void UpdateRow(int row, Cell[] rowOfCells)
        {
            var column = 0;
            foreach (var cell in rowOfCells)
            {
                var sq = _squares[column++, row];
                switch (cell.ToString())
                {
                    case " ":
                        sq.Image = null;
                        break;
                    case "*":
                        sq.LoadGoatImage(_goatCounter++);
                        break;
                    case "\u2713":
                        sq.LoadIcon(BoardIcon.MarkGoat);
                        break;
                    case "\u25A0":
                        sq.LoadIcon(BoardIcon.BlockingFence);
                        break;
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                        sq.LoadIcon(GetIconForContent(cell.ToString()));
                        break;
                }
            }
        }

        public void DisplayFooter(int size)
        {
        }

        private void Sq_Click(object sender, EventArgs e)
        {
            var mouseEvent = e as MouseEventArgs;
            var sq = sender as Square;
            if ((sq == null) || (mouseEvent == null))
                return;

            if (mouseEvent?.Button == MouseButtons.Right)
                _commandInterface.Mark(sq.XPos, sq.YPos);
            else if (mouseEvent?.Button == MouseButtons.Left)
                _commandInterface.Reveal(sq.XPos, sq.YPos);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            var thread = new Thread(() => _game.Run());
            FormClosing += (sender1, e1) => thread.Abort();
            thread.Start();
        }

        private BoardIcon GetIconForContent(string value)
        {
            var icons = (BoardIcon[]) Enum.GetValues(typeof(BoardIcon));
            var index = int.Parse(value);
            return icons[index];
        }
    }
}