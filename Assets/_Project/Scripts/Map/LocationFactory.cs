using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationFactory
    {
        private LocationPermissionRequester _permissionRequester;

        public LocationFactory(LocationPermissionRequester permissionRequester)
        {
            _permissionRequester = permissionRequester;
        }
        public ILocationProvider CreateDeviceProvider()
        {
            if (_permissionRequester.RequestPermission())
            {
                return new DeviceLocationProvider();
            }

            return new FakeLocationProvider(new List<Coordinates>());
        }

        public ILocationProvider CreateFakeProvider(List<Coordinates> latitudeLongitudeList)
        {
            return new FakeLocationProvider(latitudeLongitudeList);
        }
    }
}
