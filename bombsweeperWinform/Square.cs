using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace bombsweeperWinform
{
    public class Square : PictureBox
    {
        private bool color;
        private char piece;
        public int XPos;
        public int YPos;
        private readonly Assembly _assembly;
        private Stream _imageStream;

        public Square()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public void LoadGoatImage(int number)
        {
            var goatIndex = number % MainForm.NumGoats + 1;
            var filename = $"goat{goatIndex:D2}.jpg";
            LoadImage(filename);
        }

        private void LoadImage(string filename)
        {
            var image = $"bombsweeperWinform.{filename}";
            try
            {
                _imageStream = _assembly.GetManifestResourceStream(image);
                Image = new Bitmap(_imageStream);
            }
            catch
            {
                MessageBox.Show($"Error accessing image resource {image}");
            }
        }

        public enum BoardIcon { MarkGoat, One, Two, Three, Four, Five, Six, Seven, Eight }

        public void LoadIcon(BoardIcon boardIcon)
        {
            LoadImage("goat-icon.png");
        }
    }
}