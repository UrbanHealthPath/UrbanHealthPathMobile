using System;
using Mapbox.Unity.Location;


namespace PolSl.UrbanHealthPath.Map
{
    public interface ILocationProvider
    {
        public event Action<Location> LocationUpdated;
        public Location GetLocation();
    }
}
