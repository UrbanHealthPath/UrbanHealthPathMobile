using System.Collections;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationProviderMapUpdater : MonoBehaviour
    {
        [SerializeField] private AbstractMap _map;
        
        private LocationFactory _locationFactory;
        
        private bool _isMapInitialized;

        private bool _initialized;

        private Coroutine _deviceLocationPollCoroutine;

        private bool isPolling = false;
        
        public LocationFactory LocationFactory
        {
            get
            {
                return _locationFactory;
            }
            private set
            {
                _locationFactory = value;
            }
        }

        public void Initialize(LocationFactoryMode mode)
        {
            
            _initialized = true;
            _locationFactory = new LocationFactory(mode);
            _locationFactory.LocationProvider.LocationUpdated+=LocationProviderFirstLocationUpdate;
        }

        public void UpdateFakeLocation()
        {
            if (_locationFactory.Mode == LocationFactoryMode.Fake)
            {
                _locationFactory.PollCurrentLocation();
            }
        }

        public void ChangePollLocationDeviceCoroutineStatus()
        {
            if (!isPolling)
            {
                if (_locationFactory.Mode == LocationFactoryMode.Device && _initialized && _isMapInitialized)
                {
                    _deviceLocationPollCoroutine = StartCoroutine((PollDeviceLocation()));
                    isPolling = true;
                }
            }
            else
            {
                StopCoroutine(PollDeviceLocation());
                isPolling = false;
            }
        }
        
        private void LocationProviderFirstLocationUpdate(Location location)
        {
            _locationFactory.LocationProvider.LocationUpdated -= LocationProviderFirstLocationUpdate;
            _map.OnInitialized += () =>
            {
                _isMapInitialized = true;
            };
            _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
        }

        private IEnumerator PollDeviceLocation()
        {
            if (_locationFactory.Mode == LocationFactoryMode.Device)
            {
                while (_isMapInitialized && _initialized)
                {
                    _locationFactory.PollCurrentLocation();
                    yield return new WaitForSeconds(1.0f);
                }
            }
        }
    }
}
