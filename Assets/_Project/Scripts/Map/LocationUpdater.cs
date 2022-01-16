using System;
using Mapbox.Unity.Location;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationUpdater : ILocationUpdater
    {
        public event Action<LocationUpdatedArgs> LocationUpdated;
        
        private readonly ILocationProvider _locationProvider;

        public LocationUpdater(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }

        public void UpdateLocation()
        {
            Location location = _locationProvider.GetLocation();
            LocationUpdated?.Invoke(new LocationUpdatedArgs(location));
        }
    }
}