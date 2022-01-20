using System;
using System.Collections;

namespace PolSl.UrbanHealthPath.Map
{
    public interface ILocationUpdater
    {
        event Action<LocationUpdatedArgs> LocationUpdated;
        IEnumerator UpdateLocation();
    }
}