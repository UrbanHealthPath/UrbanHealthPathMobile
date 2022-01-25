using Mapbox.Utils;


namespace PolSl.UrbanHealthPath.PathData
{
    public readonly struct Coordinates
    {
        public double X { get; }
        public double Y { get; }

        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Vector2d(Coordinates coordinates) => new Vector2d(coordinates.X, coordinates.Y);
    }
}