using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace bombsweeperWinform
{
    public class Square : PictureBox
    {
        private readonly ResourceLoader _resourceLoader;
        private bool color;
        public int GoatNumber;
        public BoardIcon Icon;
        private char piece;
        public int XPos;
        public int YPos;

        public Square()
        {
            var assembly = Assembly.GetExecutingAssembly();
            _resourceLoader = new ResourceLoader(assembly);
        }

        public void LoadIcon(BoardIcon icon)
        {
            GoatNumber = 0;
            Icon = icon;
            Image = new Bitmap(_resourceLoader.GetIcon(icon));
        }

        public void LoadGoatImage(int number)
        {
            GoatNumber = number;
            Image = new Bitmap(_resourceLoader.GetGoatImage(number));
        }
    }
}