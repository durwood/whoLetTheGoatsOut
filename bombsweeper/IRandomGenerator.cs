namespace bombsweeper
{
    public interface IRandomGenerator
    {
        double NextDouble();
        int Next(int minValue, int maxValue);
    }
}