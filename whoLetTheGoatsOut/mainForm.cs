﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    public partial class MainForm : Form, IGameView
    {
        private const int BoardOffset = 80;
        public static int NumGoats = 38;
        private readonly int _cellSize;
        private readonly Board _board;
        private readonly Random _random = new Random();
        private int _elapsedTime;

        private int _numBombs;
        private readonly Timer _timer;
        private WinformCellView[,] _winformCellViews;

        public MainForm(Board board)
        {
            var workingRectangle = Screen.PrimaryScreen.WorkingArea;
            FormHeight = workingRectangle.Height;
            var boardHeight = FormHeight - BoardOffset;
            _cellSize = boardHeight/board.GetSize() - 30;
            boardHeight = _cellSize * board.GetSize();
            FormHeight = boardHeight + BoardOffset + 40;
            FormWidth = _cellSize * board.GetSize() + 15;

            //Size = new Size(Width, Height);
            InitializeComponent();
            _board = board;
            Text = $"Who Let the Goats Out? ({board.SavedBoard})";
            _timer = new Timer {Interval = 1000}; // 1 second
            InitializeBoard();
        }

        public int FormWidth { get; set; }

        public int FormHeight { get; set; }

        public void DisplayBoard()
        {
            foreach (var cell in _winformCellViews)
                UpdateCellDisplay(cell);
        }

        public void UpdateStatusDisplay()
        {
            ElapsedTimeLabel.Text = $"Elapsed Time: {_elapsedTime}";
            if (_numBombs != _board.GetNumberOfUnmarkedBombs())
            {
                _numBombs = _board.GetNumberOfUnmarkedBombs();
                BombCountLabel.Text = $"Unmarked Bombs Remaining: {_numBombs}";
            }
        }

        public void ShowResult()
        {
            string result;
            _timer.Stop();
            if (_board.GameWon())
                result = "Congratulations, you won!";
            else if (_board.GameLost())
                result = "Loser.";
            else
                result = "Quitter.";

            MessageBox.Show(result);
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
                        BorderStyle = BorderStyle.None,
                        // BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(0 + col*_cellSize, 80 + row*_cellSize),
                        Name = $"Row{row}_Col{col}",
                        Size = new Size(_cellSize, _cellSize),
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

            if (mouseEvent.Button == MouseButtons.Right)
                _board.ToggleMark(sq.Row, sq.Col);
            else if (mouseEvent.Button == MouseButtons.Left)
                _board.Reveal(sq.Row, sq.Col);
            DisplayBoard();
            if (!_board.GameInProgress())
            {
                ShowResult();
                Close();
            }
        }

        private void UpdateCellDisplay(WinformCellView cell)
        {
            var modelCell = cell.ModelCell;
            if (modelCell.IsRevealed)
                if (modelCell.HasBomb)
                    cell.LoadGoatImage(cell.Col + cell.Row);
                else if (modelCell.NeighboringBombCount > 0)
                    cell.LoadBombCount(modelCell.NeighboringBombCount);
                else
                    cell.Image = null;
            else if (modelCell.IsMarked)
                cell.LoadIcon(BoardIcon.MarkGoat);
            else
                cell.LoadIcon(BoardIcon.BlockingFence);
        }

        private void mainForm_Closing(object sender, CancelEventArgs ee)
        {
            if (_board.GameInProgress())
                ShowResult();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            FormClosing += mainForm_Closing;
            Size = new Size(FormWidth, FormHeight);

            //PreviewGoatsAndIcons();
            UpdateStatusDisplay();
            RegisterTimer();
        }

        private void RegisterTimer()
        {
            _timer.Tick += timer_Tick;
            _timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var timer = sender as Timer;
            if (timer == null)
                return;

            _elapsedTime += 1;
            UpdateStatusDisplay();
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

        private void ElapsedTimeLabel_Click(object sender, EventArgs e)
        {

        }
    }
}