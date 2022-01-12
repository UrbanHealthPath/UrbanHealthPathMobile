using System.Collections.Generic;
using System.Linq;
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

        private void Awake()
        {
            IApplicationData applicationData = LoadApplicationData();
            InitializeSceneMapSceneComponents(applicationData.UrbanPaths[0].Waypoints.Select(x => x.Value.Coordinates).ToList());
        }

        private IApplicationData LoadApplicationData()
        {
            string path = "ExampleData";

            ILoadersFactory loadersFactory = new JsonFilesLoadersFactory(path, new JsonAssetFileReader(), new JsonDataLoaderParsersFactory().Create());
            IApplicationDataLoader applicationDataLoader = new ApplicationDataLoader(loadersFactory);

            return applicationDataLoader.LoadData();
        }

        private void InitializeSceneMapSceneComponents(List<Coordinates> coordinatesList)
        {
            ILocationProvider locationProvider =
                new LocationFactory(new LocationPermissionRequester()).CreateFakeProvider(coordinatesList);
            InitializeWithAbstractMap(locationProvider, coordinatesList);
            _locationProviderRotator.Initialize(locationProvider);
            _coroutinesManager.Initialize(locationProvider);
            _coroutinesManager.StartCoroutines();
        }

        private void InitializeWithAbstractMap(ILocationProvider locationProvider, List<Coordinates> coordinatesList)
        {
            AbstractMap map = new MapSpawner().SpawnMap(_mapPrefab);
            new LocationProviderMapInitializer(map, locationProvider);
            _playerLocationTransformer.Initialize(map, locationProvider);
            _navigationPointProvider.Initialize(_mapPrefab, coordinatesList[0]);
            _stationFactory.Initialize(map, locationProvider, coordinatesList);
        }
    }
}
