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
using PolSl.UrbanHealthPath.Utils.PersistentValue;
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

        private IPersistentValue<bool> _isFirstRun;

        private ViewManager _viewManager;

        private void Awake()
        {
            _logger = new UnityLogger();
            _isFirstRun = new BoolPrefsValue("is_first_run", true);

            _applicationData = LoadApplicationData();

            GameObject uiManager = Instantiate(_uiManager);

            _viewManager = uiManager.GetComponent<ViewManager>();
            _viewManager.Initialize();

            BuildUI();
            //(applicationData.UrbanPaths[0].Waypoints.Select(x => x.Value.Coordinates).ToList());
        }

        private IApplicationData LoadApplicationData()
        {
            string path = "ExampleData";

            ILoadersFactory loadersFactory = new JsonFilesLoadersFactory(path, new JsonAssetFileReader(),
                new JsonDataLoaderParsersFactory().Create());
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
            if (_isFirstRun.Value)
            {
                BuildLoginView();
            }
            else
            {
                BuildMainView();
            }
        }

        private void BuildLoginView()
        {
            _viewManager.OpenView(ViewType.Login,
                new LogInViewInitializationParameters(() => _logger.Log(LogVerbosity.Debug, "Log in not supported!"),
                    () =>
                    {
                        _isFirstRun.Value = false;
                        BuildMainView();
                    })
            );
        }

        private void BuildMainView()
        {
            _viewManager.OpenView(ViewType.Main,
                new MainViewInitializationParameters(BuildProfileView, BuildHelpView, BuildSettingsView, StartPath,
                    DemoPath, Application.Quit, "Rozpocznij ścieżkę", "Zobacz ścieżkę"));
        }

        private void DemoPath()
        {
            _viewManager.OpenView(ViewType.PathChoice, new PathChoiceViewInitializationParameters(
                GetAvailablePaths(path => _logger.Log(LogVerbosity.Debug, "Started demo path " + path.DisplayedName)),
                BuildMainView));
        }

        private void StartPath()
        {
            _viewManager.OpenView(ViewType.PathChoice, new PathChoiceViewInitializationParameters(
                GetAvailablePaths(path => _logger.Log(LogVerbosity.Debug, "Started path " + path.DisplayedName)),
                BuildMainView));
        }

        private void BuildSettingsView()
        {
            _viewManager.OpenView(ViewType.Settings, new SettingsInitializationParameters(BuildMainView, () => { },
                () => { }, () => { }, () => { }));
        }

        private void BuildHelpView()
        {
            _viewManager.OpenView(ViewType.Help, new HelpViewInitializationParameters(new List<ListElement>()
            {
                new ListElement("Przykładowy przycisk pomocy", null, "Pomoc", () => { })
            }, BuildMainView));
        }

        private void BuildProfileView()
        {
            _viewManager.OpenView(ViewType.Profile,
                new ProfileViewInitializationParameters(new List<ListElement>(), BuildStatisticsView,
                    BuildAchievementsView, Share, BuildMainView));
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

        private List<ListElement> GetAvailablePaths(Action<UrbanPath> pathChosenAction)
        {
            return _applicationData.UrbanPaths.Select(x => new ListElement(x.DisplayedName, null, "Ścieżka",
                () => pathChosenAction(x))).ToList();
        }
    }
}