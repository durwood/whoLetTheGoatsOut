namespace bombsweeper
{
    public interface ICommandInterface
    {
        void Tick();
        bool HasCommandToProcess { get; set; }
        string GetCommand();
        void Reset();
    }
}