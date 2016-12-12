using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    public partial class MainForm : Form
    {
        public static int BoardSize = 9;
        public static int NumGoats = 38;
        private readonly Random _random = new Random();
        private readonly WindowsCommandInterface _commandInterface;
        private readonly Square[,] _squares = new Square[BoardSize, BoardSize];
        private readonly WinFormView _view;

        public MainForm()
        {
            InitializeComponent();
            var board = Board.Create(new Dictionary<string, bool> {["newBoard"] = true});
            _view = new WinFormView(this, board);
            var game = new Game(board, _view, _commandInterface);
            _view.DisplayBoard(board);
            game.ShowResult();
            
        }

   

        private void mainForm_Load(object sender, EventArgs e)
        {
            //PreviewGoatsAndIcons();
        }

        private void PreviewGoatsAndIcons()
        {
            //var icons = (BoardIcon[]) Enum.GetValues(typeof(BoardIcon));

            //var iconIdx = 0;
            //var goatIdx = 0;
            //foreach (var square in _squares)
            //    if (iconIdx < icons.Length)
            //        square.LoadIcon(icons[iconIdx++]);    
            //    else if (goatIdx < NumGoats)
            //        square.LoadGoatImage(goatIdx++);
            //    else
            //        break;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}