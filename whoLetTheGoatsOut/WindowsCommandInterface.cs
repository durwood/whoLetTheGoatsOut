using bombsweeper;

namespace bombsweeperWinform
{
    internal class WindowsCommandInterface : ICommandInterface
    {
        public void Tick()
        {
        }

        public bool HasCommandToProcess { get; set; }
        public string GetCommand()
        {
            return "";
        }

        public void Reset()
        {
        }

        public void Mark(int sqXPos, int sqYPos)
        {
        }

        public void Reveal(int sqXPos, int sqYPos)
        {
        }
    }
}