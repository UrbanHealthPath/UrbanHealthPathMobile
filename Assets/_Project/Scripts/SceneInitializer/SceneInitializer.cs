using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.Navigation;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.DataLoaders;
using PolSl.UrbanHealthPath.Player;
using PolSl.UrbanHealthPath.Tools.TextLogger;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Views;
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

        [SerializeField] private GameObject _uiManager;

        private ITextLogger _logger;

        private IApplicationData _applicationData;
        
        
        private void Awake()
        {
            _logger = new UnityLogger();
            
            _applicationData = LoadApplicationData();

            GameObject uiManager = Instantiate(_uiManager);

            BuildMainView();
            //(applicationData.UrbanPaths[0].Waypoints.Select(x => x.Value.Coordinates).ToList());
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

        private void BuildUI()
        {
            BuildMainView();
        }

        private void BuildMainView()
        {
            ViewManager.GetInstance().OpenView(ViewType.Main,
                new MainViewInitializer(BuildProfileView, BuildHelpView, BuildSettingsView, StartPath,
                    DemoPath, Application.Quit, "Rozpocznij ścieżkę", "Zobacz ścieżkę"));
        }

        private void DemoPath()
        {
            throw new NotImplementedException();
        }

        private void StartPath()
        {
            ViewManager.GetInstance().OpenView(ViewType.PathChoice, new PathChoiceViewInitializer(
                _applicationData.UrbanPaths.Select(x => 
                    new ListElement(x.DisplayedName, null, "Ścieżka", () => _logger.Log(LogVerbosity.Debug, "Path started"))).ToList(), 
                BuildMainView));
        }
        

        private void BuildSettingsView()
        {
            throw new NotImplementedException();
        }

        private void BuildHelpView()
        {
            //ViewManager.GetInstance().OpenView(ViewType.Help, new HelpViewInitializer())
        }

        private void BuildProfileView()
        {
            ViewManager.GetInstance().OpenView(ViewType.Profile,
                new ProfileViewInitializer(BuildStatisticsView, BuildAchievementsView, Share, BuildProfileView));
        }

        private void Share()
        {
            _logger.Log(LogVerbosity.Debug, "Shared");
        }

        private void BuildAchievementsView()
        {
            _logger.Log(LogVerbosity.Debug, "Achievements");
        }

        private void BuildStatisticsView()
        {
            _logger.Log(LogVerbosity.Debug, "Statistics");
        }
    }
}
