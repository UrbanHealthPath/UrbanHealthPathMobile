namespace PolSl.UrbanHealthPath.PathData
{
    public readonly struct Coordinates
    {
        public decimal X { get; }
        public decimal Y { get; }

        public Coordinates(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
    }
}