using Mapbox.CheapRulerCs;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Utils.DistanceCalculator
{
    public class DistanceCalculator : IDistanceCalculator
    {
        public IDistance CalculateDistance(Coordinates from, Coordinates to)
        {
            double[] fromDoubles = {from.X, from.Y};
            double[] toDoubles = {to.X, to.Y};
            
            CheapRuler ruler = new CheapRuler(fromDoubles[1], CheapRulerUnits.Meters);
            
            return Distance.FromMeters(ruler.Distance(fromDoubles, toDoubles));
        }
    }
}