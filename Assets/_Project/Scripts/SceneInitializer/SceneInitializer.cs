using System.Collections.Generic;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.Player;
using UnityEngine;

namespace PolSl.UrbanHealthPath.SceneInitializer
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private LocationProviderRotator _locationProviderRotator;

        [SerializeField] private PlayerLocationTransformer _playerLocationTransformer;
        
        [SerializeField] private StationFactory _stationFactory;

        [SerializeField] private CoroutinesManager _coroutinesManager;

        [SerializeField] private AbstractMap _mapPrefab;
        
        private void Awake()
        {
            ILocationProvider locationProvider = new LocationFactory(new LocationPermissionRequester()).CreateFakeProvider(new List<Coordinates>());
            AbstractMap map = new MapSpawner().SpawnMap(_mapPrefab);
            new LocationProviderMapInitializer(map, locationProvider);
            _locationProviderRotator.Initialize(locationProvider);
            _playerLocationTransformer.Initialize(map, locationProvider);
            _coroutinesManager.Initialize(locationProvider);
            _coroutinesManager.StartCoroutines();
            _stationFactory.Initialize(map, locationProvider, new List<Coordinates>());
        }
    }
}
