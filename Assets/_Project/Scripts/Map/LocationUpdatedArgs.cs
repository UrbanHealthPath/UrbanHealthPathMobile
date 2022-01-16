using Mapbox.Unity.Location;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationUpdatedArgs
    {
        public Location Location { get; }
        
        public LocationUpdatedArgs(Location location)
        {
            Location = location;
        }
    }
}