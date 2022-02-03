using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    public interface ILocationProviderFactory
    {
        ILocationProvider CreateProvider();
        ILocationProvider CreateProvider(List<Coordinates> coordinatesList);
    }
}