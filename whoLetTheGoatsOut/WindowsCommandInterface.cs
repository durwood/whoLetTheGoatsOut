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
            HasCommandToProcess = false;
            _command = "";
        }

        public void Mark(int x, int y)
        {
            SetCommand(x, y, 'm');
        }

        private void SetCommand(int x, int y, char type)
        {
            HasCommandToProcess = true;
            _command = $"{type} {++x},{++y}";
        }

        public void Reveal(int x, int y)
        {
            SetCommand(x, y, 'c');
        }
    }
}