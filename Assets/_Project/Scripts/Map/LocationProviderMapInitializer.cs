using Mapbox.Unity.Location;
using Mapbox.Unity.Map;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationProviderMapInitializer
    {
        private AbstractMap _map;

        private ILocationProvider _locationProvider;
        
        public LocationProviderMapInitializer(AbstractMap map, ILocationProvider locationProvider)
        {
            _map = map;
            _locationProvider = locationProvider;
            _locationProvider.LocationUpdated+=LocationProviderFirstLocationUpdate;
        }
        
        private void LocationProviderFirstLocationUpdate(Location location)
        {
            _locationProvider.LocationUpdated -= LocationProviderFirstLocationUpdate;
            _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
        }
    }
}
