using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using bombsweeper;

namespace whoLetTheGoatsOut
{
    public class WinformCellView : PictureBox
    {
        private readonly ResourceLoader _resourceLoader;
        public int Col;
        public Cell ModelCell;
        public int Row;

        public WinformCellView()
        {
            _resourceLoader = new ResourceLoader(Assembly.GetExecutingAssembly());
        }


        public void LoadIcon(BoardIcon icon)
        {
            try
            {
                var iconImage = _resourceLoader.GetIcon(icon);
                Image = new Bitmap(iconImage);
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

        public void LoadBombCount(int goatCount)
        {
            try
            {
                Stream bombCountStream;
                switch (goatCount)
                {
                    case 1:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.One);
                        break;
                    case 2:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.Two);
                        break;
                    case 3:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.Three);
                        break;
                    case 4:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.Four);
                        break;
                    case 5:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.Five);
                        break;
                    case 6:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.Six);
                        break;
                    case 7:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.Seven);
                        break;
                    case 8:
                        bombCountStream = _resourceLoader.GetIcon(BoardIcon.Eight);
                        break;
                    default:
                        bombCountStream = null;
                        break;
                }
                Image = new Bitmap(bombCountStream);
            }
            catch (Exception)
            {
                MessageBox.Show($"Error accessing image resource for Neighboring Goat count {goatCount}");
            }
        }
    }
}