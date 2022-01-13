namespace PolSl.UrbanHealthPath.Utils.DistanceCalculator
{
    public interface IDistance
    {
        double InKilometers();
        double InMeters();

        bool LessThan(IDistance comparedDistance);
        bool GreaterThan(IDistance comparedDistance);
    }
}