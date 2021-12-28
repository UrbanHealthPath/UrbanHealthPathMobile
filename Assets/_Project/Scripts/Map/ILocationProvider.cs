using System;
using Mapbox.Unity.Location;


namespace PolSl.UrbanHealthPath.Navigation
{
    public interface ILocationProvider
    {
        public Location GetLocation();
        
        public event Action<Location> OnLocationUpdated;
    }
}
