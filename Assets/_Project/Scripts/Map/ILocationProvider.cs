using System;
using Mapbox.Unity.Location;


namespace PolSl.UrbanHealthPath.Map
{
    //Interface which implementations provide location of the player.
    public interface ILocationProvider
    {
        public Location GetLocation();
    }
}
