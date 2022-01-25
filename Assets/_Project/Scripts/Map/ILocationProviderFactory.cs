using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    public interface ILocationProviderFactory
    {
        ILocationProvider CreateDeviceProvider();
        ILocationProvider CreateFakeProvider(List<Coordinates> coordinatesList);
    }
}