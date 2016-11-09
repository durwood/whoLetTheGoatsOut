using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    public class WinformGameController : IGameController
    {
        private readonly Board _board;

        public WinformGameController(Board board)
        {
            _board = board;
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(_board));
        }
    }
}