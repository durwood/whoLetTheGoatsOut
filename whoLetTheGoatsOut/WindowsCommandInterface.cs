using bombsweeper;

namespace bombsweeperWinform
{
    internal class WindowsCommandInterface : ICommandInterface
    {
        private string _command = "";

        public void Tick()
        {
        }

        public bool HasCommandToProcess { get; set; }
        public string GetCommand()
        {
            return _command;
        }

        public void Reset()
        {
        }

        public void Mark(int x, int y)
        {
            HasCommandToProcess = true;
            _command = $"m {x},{y}";
        }

        public void Reveal(int x, int y)
        {
        }
    }
}