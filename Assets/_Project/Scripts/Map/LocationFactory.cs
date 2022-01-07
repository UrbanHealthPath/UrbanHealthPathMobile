using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationFactory
    {
        private LocationFactoryMode _mode;

        private List<string> _latitudeLongitudeList;
        
        private ILocationProvider _locationProvider;
        
        private Location _currentLocation;
        
        public LocationFactory(LocationFactoryMode mode)
        {
            _mode = mode;
            _latitudeLongitudeList = new List<string>();
            InjectLocationProvider();
        }

        public LocationFactory(List<string> latitudeLongitudeList)
        {
            _latitudeLongitudeList = new List<string>();
            latitudeLongitudeList.AddRange(latitudeLongitudeList);
            _mode = LocationFactoryMode.Fake;
            InjectLocationProvider();
        }

        public LocationFactoryMode Mode
        {
            get
            {
                return _mode;
            }
            private set
            {
                _mode = value;
            }
        }
        
        public ILocationProvider LocationProvider
        {
            get
            {
                return _locationProvider;
            }
            set
            {
                _locationProvider = value;
            }
        }

        public void PollCurrentLocation()
        {
            _currentLocation = _locationProvider.GetLocation();
        }
        
        public void ChangeMode(LocationFactoryMode mode)
        {
            _mode = mode;
            InjectLocationProvider();
        }
        
        private void InjectLocationProvider()
        {
            if (_mode == LocationFactoryMode.Device && new LocationPermissionRequester().RequestPermission())
            {
                _locationProvider = new DeviceLocationProvider();
            }
            else
            {
                _locationProvider = new FakeLocationProvider(_latitudeLongitudeList);
                _mode = LocationFactoryMode.Fake;
            }
        }
    }
}
