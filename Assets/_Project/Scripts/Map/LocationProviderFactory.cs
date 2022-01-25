using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationProviderFactory : ILocationProviderFactory
    {
        private LocationPermissionRequester _permissionRequester;

        public LocationProviderFactory(LocationPermissionRequester permissionRequester)
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

        public ILocationProvider CreateFakeProvider(List<Coordinates> coordinates)
        {
            return new FakeLocationProvider(coordinates);
        }
    }
}
