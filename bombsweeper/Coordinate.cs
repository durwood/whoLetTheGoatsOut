namespace bombsweeper
{
    public struct Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int column, int row)
        {
            X = column;
            Y = row;
        }
    }
}