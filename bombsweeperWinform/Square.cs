using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace bombsweeperWinform
{
    public class Square : PictureBox
    {
        private readonly ResourceLoader _resourceLoader;
        public int XPos;
        public int YPos;

        public Square()
        {
            _resourceLoader = new ResourceLoader(Assembly.GetExecutingAssembly());
        }

        public void LoadIcon(BoardIcon icon)
        {
            Image = new Bitmap(_resourceLoader.GetIcon(icon));
        }

        public void LoadGoatImage(int number)
        {
            Image = new Bitmap(_resourceLoader.GetGoatImage(number));
        }
    }
}