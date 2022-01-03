using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationFactory
    {
        private FakeLocationProvider _fakeLocationProvider;

        private DeviceLocationProvider _deviceLocationProvider;

        private LocationFactoryMode _mode;

        private List<string> _latitudeLongitudeList;
        
        private ILocationProvider _locationProvider;

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
            _locationProvider.GetLocation();
        }
        public void ChangeMode(LocationFactoryMode mode)
        {
            _mode = mode;
            InjectLocationProvider();
        }
        
        private void Awake()
        { 
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
            }
        }
    }
}
