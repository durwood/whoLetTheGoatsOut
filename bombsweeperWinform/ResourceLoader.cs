using System.IO;
using System.Reflection;

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
            var image = $"bombsweeperWinform.ImageResources.{filename}";
            return _assembly.GetManifestResourceStream(image);
        }

        public Stream GetIcon(BoardIcon icon)
        {
            return GetImage(_icons.GetResourceName(icon));
        }
    }
}