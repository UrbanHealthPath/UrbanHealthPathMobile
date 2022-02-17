using System;
using Mapbox.Unity.Location;


namespace PolSl.UrbanHealthPath.Map
{
    public interface ILocationProvider
    {
        public Location GetLocation();
    }
}
