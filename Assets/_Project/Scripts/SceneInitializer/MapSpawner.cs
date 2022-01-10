using Mapbox.Unity.Map;
using UnityEngine;

namespace PolSl.UrbanHealthPath.SceneInitializer
{
    public class MapSpawner
    {
        public AbstractMap SpawnMap(AbstractMap map)
        {
            return Object.Instantiate(map);
        }
    }
}
