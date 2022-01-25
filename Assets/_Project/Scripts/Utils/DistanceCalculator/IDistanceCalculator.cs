using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Utils.DistanceCalculator
{
    public interface IDistanceCalculator
    {
        IDistance CalculateDistance(Coordinates from, Coordinates to);
    }
}