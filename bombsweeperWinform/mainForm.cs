using System;
using System.Drawing;
using System.Windows.Forms;

namespace bombsweeperWinform
{
    public partial class MainForm : Form
    {
        private readonly Square[,] _square = new Square[9, 9];

        public MainForm()
        {
            InitializeComponent();
            for (var ii = 0; ii < 9; ++ii)
                for (var jj = 0; jj < 9; ++jj)
                {
                    _square[ii, jj] = new Square();
                    var sq = _square[ii, jj];
                    sq.XPos = ii;
                    sq.YPos = jj;
                    sq.BackColor = SystemColors.ActiveCaption;
                    sq.BorderStyle = BorderStyle.FixedSingle;
                    sq.Location = new Point(57 + ii*40, 109 + jj*40);
                    sq.Name = "chessBox1";
                    sq.Size = new Size(40, 40);
                    sq.TabIndex = 2;
                    sq.TabStop = false;
                    sq.Click += Sq_Click;
                    Controls.Add(sq);
                }
        }

        private void Sq_Click(object sender, EventArgs e)
        {
            var mouseEvent = e as MouseEventArgs;
            var sq = sender as Square;
            var result = $"{mouseEvent?.Button}-Clicked on ({sq?.XPos}, {sq?.YPos})";
            MessageBox.Show(result);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
        }
    }
}