using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace bombsweeperWinform
{
    public partial class ResourceLoader
    {
        private readonly Assembly _assembly;
        private readonly Dictionary<BoardIcon, string> _iconMap = new Dictionary<BoardIcon, string>();

        public ResourceLoader(Assembly assembly)
        {
            _assembly = assembly;
            InitializeIconMap();
        }

        private void InitializeIconMap()
        {
            _iconMap.Add(BoardIcon.MarkGoat, "goat-icon.png");
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

        public Stream GetIcon(BoardIcon boardIcon)
        {
            return GetImage(_iconMap[boardIcon]);
        }
    }
}