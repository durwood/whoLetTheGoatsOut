using System;
using System.Drawing;
using System.Windows.Forms;

namespace bombsweeperWinform
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
            for (var col = 0; col < 9; ++col)
                for (var row = 0; row < 9; ++row)
                {
                    var sq = new Square
                    {
                        XPos = row,
                        YPos = col,
                        BackColor = Color.MediumSeaGreen,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(0 + row*CellSize, 0 + col*CellSize),
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
            PreviewGoatsAndIcons();
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
    }
}