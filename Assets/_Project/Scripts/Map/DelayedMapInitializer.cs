using Mapbox.Unity.Map;

namespace PolSl.UrbanHealthPath.Map
{
    public class DelayedMapInitializer
    {
        private readonly AbstractMap _map;
        private readonly ILocationUpdater _locationUpdater;
        
        public DelayedMapInitializer(AbstractMap map, ILocationUpdater locationUpdater)
        {
            _map = map;
            _locationUpdater = locationUpdater;
            _locationUpdater.LocationUpdated += InitializeMapAfterFirstLocationUpdate;
        }
        
        private void InitializeMapAfterFirstLocationUpdate(LocationUpdatedArgs args)
        {
            _locationUpdater.LocationUpdated -= InitializeMapAfterFirstLocationUpdate;
            _map.Initialize(args.Location.LatitudeLongitude, _map.AbsoluteZoom);
        }
    }
}
