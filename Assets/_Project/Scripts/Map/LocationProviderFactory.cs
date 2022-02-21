using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.Utils.PermissionManagement;

namespace PolSl.UrbanHealthPath.Map
{
    public class LocationProviderFactory : ILocationProviderFactory
    {
        public ILocationProvider CreateProvider()
        {
            return new DeviceLocationProvider();
        }

        public ILocationProvider CreateProvider(List<Coordinates> coordinates)
        {
            return new FakeLocationProvider(coordinates);
        }
    }
}
