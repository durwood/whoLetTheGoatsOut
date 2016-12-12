using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace whoLetTheGoatsOut
{
    public class Square : PictureBox
    {
        private readonly ResourceLoader _resourceLoader;
        public int Col;
        public int Row;

        public Square()
        {
            _resourceLoader = new ResourceLoader(Assembly.GetExecutingAssembly());
        }

        public void LoadIcon(BoardIcon icon)
        {
            try
            {
                if (icon == BoardIcon.Empty)
                    Image = null;
                else
                {
                    var iconImage = _resourceLoader.GetIcon(icon);
                    Image = new Bitmap(iconImage);
                }
            }
            catch
            {
                MessageBox.Show($"Error accessing image resource for BoardIcon {icon}");
            }
        }

        public void LoadGoatImage(int number)
        {
            try
            {
                var goatImage = _resourceLoader.GetGoatImage(number);
                Image = new Bitmap(goatImage);
            }
            catch
            {
                MessageBox.Show($"Error accessing image resource for Goat number {number}");
            }
        }
    }
}