using System.Collections.Generic;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.Navigation;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.DataLoaders;
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

        [SerializeField] private NavigationPointProvider _navigationPointProvider;

        private ApplicationDataLoader _applicationDataLoader;
        
        private void Awake()
        {
            InitializeSceneMapSceneComponents();
        }

        private void LoadApplicationData()
        {
            string path = "UrbanHealthPathMobile\\Assets\\_Project\\Resources\\ExampleData\\";
        }

        private void InitializeSceneMapSceneComponents()
        {
            ILocationProvider locationProvider =
                new LocationFactory(new LocationPermissionRequester()).CreateFakeProvider(new List<Coordinates>());//if fake it needs to take take the stations from the file
            InitializeWithAbstractMap(locationProvider);
            _locationProviderRotator.Initialize(locationProvider);
            _coroutinesManager.Initialize(locationProvider);
            _coroutinesManager.StartCoroutines();
        }

        private void InitializeWithAbstractMap(ILocationProvider locationProvider)
        {
            AbstractMap map = new MapSpawner().SpawnMap(_mapPrefab);
            new LocationProviderMapInitializer(map, locationProvider);
            _playerLocationTransformer.Initialize(map, locationProvider);
            _navigationPointProvider.Initialize(_mapPrefab, new Coordinates());//need to take the first station from file
            _stationFactory.Initialize(map, locationProvider, new List<Coordinates>());//needs to take the stations from file
        }
    }
}
