using System;

namespace PolSl.UrbanHealthPath.Map
{
    public interface ILocationUpdater
    {
        event Action<LocationUpdatedArgs> LocationUpdated;
        void UpdateLocation();
    }
}