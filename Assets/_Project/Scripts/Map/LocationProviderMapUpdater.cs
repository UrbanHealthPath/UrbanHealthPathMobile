using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationProviderMapUpdater : MonoBehaviour
    {
        [SerializeField] private AbstractMap _map;
        
        private LocationFactoryMode _factoryMode = LocationFactoryMode.Fake;

        private LocationFactory _locationFactory;
        
        private ILocationProvider _locationProvider;
        
        private float _lerpTime = 1f;

        private bool _isMapInitialized;

        private bool _initialized;

        private Vector3 _startPosition;

        private Vector3 _endPosition;

        private Vector2d _startLatLong;

        private Vector2d _endLatLong;

        private float _lerpStartTime;

        public ILocationProvider LocationProvider
        {
            get
            {
                return _locationProvider;
            }
            private set
            {
                _locationProvider = value;
            }
        }

        public void Initialize(LocationFactoryMode mode)
        {
            _factoryMode = mode;
            _initialized = true;
            _locationFactory = new LocationFactory(_factoryMode);
            _locationProvider = _locationFactory.LocationProvider;
            _locationProvider.LocationUpdated+=LocationProviderFirstLocationUpdate;
        }
        
        private void LocationProviderFirstLocationUpdate(Location location)
        {
            _locationProvider.LocationUpdated -= LocationProviderFirstLocationUpdate;
            _map.OnInitialized += () =>
            {
                _isMapInitialized = true;
                _locationProvider.LocationUpdated += LocationProviderLocationUpdated;
            };
            _map.Initialize(_locationProvider.GetLocation().LatitudeLongitude, _map.AbsoluteZoom);
        }
        
        private void LocationProviderLocationUpdated(Location location)
        {
            if (_isMapInitialized && location.IsLocationUpdated)
            {
                StartLerping(location);
            }
        }
        
        private void StartLerping(Location location)
        {
            _lerpStartTime = Time.time;
            _lerpTime = Time.deltaTime;
            _startLatLong = _map.CenterLatitudeLongitude;
            _endLatLong = location.LatitudeLongitude;
            _startPosition = _map.GeoToWorldPosition(_startLatLong, false);
            _endPosition = _map.GeoToWorldPosition(_endLatLong, false);
        }
        
        private void LateUpdate()
        {
            if (_isMapInitialized &&_initialized)
            {
                float timeSinceStarted = Time.time - _lerpStartTime;
                float percentageComplete = timeSinceStarted / _lerpTime;
                _startPosition = _map.GeoToWorldPosition(_startLatLong, false);
                _endPosition = _map.GeoToWorldPosition(_endLatLong, false);
                var position = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);
                var latLong = _map.WorldToGeoPosition(position);
                _map.UpdateMap(latLong, _map.Zoom);
            }
        }
    }
}
