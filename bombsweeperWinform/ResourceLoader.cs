using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace bombsweeperWinform
{
    public class ResourceLoader
    {
        private readonly Assembly _assembly;
        private readonly BoardIcons _icons;

        public ResourceLoader(Assembly assembly)
        {
            _assembly = assembly;
            _icons = new BoardIcons();
        }

        public Stream GetGoatImage(int number)
        {
            var goatIndex = number%MainForm.NumGoats + 1;
            var filename = $"goat{goatIndex:D2}.jpg";
            return GetImage(filename);
        }

        private Stream GetImage(string filename)
        {
            var image = $"bombsweeperWinform.{filename}";
            try
            {
                return _assembly.GetManifestResourceStream(image);
            }
            catch
            {
                MessageBox.Show($"Error accessing image resource {image}");
            }
            return null;
        }

        public Stream GetIcon(BoardIcon icon)
        {
            return GetImage(_icons.GetResourceName(icon));
        }
    }
}