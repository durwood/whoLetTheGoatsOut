using System;
using System.Drawing;
using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    public partial class MainForm : Form
    {
        public static int BoardSize = 9;
        public static int NumGoats = 38;
        public static int CellSize = 50;
        private readonly Random _random = new Random();
        private readonly Square[,] _squares = new Square[BoardSize, BoardSize];
        private Image _savedImage;

        public MainForm()
        {
            InitializeComponent();

            var view = new WinFormView();
            var commandInterface = new WindowsCommandInterface();
            var game = new Game(new Board(BoardSize), view, commandInterface);
            for (var row = 0; row < BoardSize; ++row)
                for (var col = 0; col < BoardSize; ++col)
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
                    Controls.Add(sq);
                    _squares[row, col] = sq;
                }
            // This is where we want the game to run. Currently this doesn't work because nothing will cause the game to terminate
            //game.Run();

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

            string text;
            if (mouseEvent?.Button == MouseButtons.Right)
                text = $"Right-Clicked Col: {sq?.Col}, Row: {sq?.Row}\n Image Pasted to cell.";
            else if (mouseEvent?.Button == MouseButtons.Left)
                text = $"Left-Clicked Col: {sq?.Col}, Row: {sq?.Row}\n Image Cut from cell.";
            else
                text = $"{mouseEvent?.Button}-Clicked";
            MessageBox.Show(text);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            PreviewGoatsAndIcons();
        }

        private void PreviewGoatsAndIcons()
        {
            var icons = (BoardIcon[]) Enum.GetValues(typeof(BoardIcon));

            var iconIdx = 0;
            var goatIdx = 0;
            foreach (var square in _squares)
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