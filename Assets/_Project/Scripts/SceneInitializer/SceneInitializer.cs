using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Camera;
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
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.SceneInitializer
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private CoroutinesManager _coroutinesManager;

        [SerializeField] private MapHolder _mapHolderPrefab;

        [SerializeField] private GameObject _uiManager;

        [SerializeField] private UnityEngine.Camera _mainCamera;

        private ITextLogger _logger;
        private IApplicationData _applicationData;
        private IPersistentValue<bool> _isFirstRun;
        private ViewManager _viewManager;
        private MapHolder _mapHolder;

        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
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

        private void InitializeDemoMap(List<Coordinates> coordinatesList)
        {
            DestroyMap();
            
            ILocationProvider locationProvider =
                new LocationFactory(new LocationPermissionRequester()).CreateFakeProvider(coordinatesList);
            
            _mapHolder = Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(locationProvider, coordinatesList);
            _mainCamera.enabled = false;
            _mapHolder.Camera.enabled = true;
            _coroutinesManager.Initialize(locationProvider);
            _coroutinesManager.StartCoroutines();
        }

        private void InitializeMap(List<Coordinates> coordinatesList)
        {
            DestroyMap();
            
            ILocationProvider locationProvider =
                new LocationFactory(new LocationPermissionRequester()).CreateDeviceProvider();

            _mapHolder = Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(locationProvider, coordinatesList);
            _mainCamera.enabled = false;
            _mapHolder.Camera.enabled = true;
            _coroutinesManager.Initialize(locationProvider);
            _coroutinesManager.StartCoroutines();
        }

        private void DestroyMap()
        {
            if (_mapHolder is null)
            {
                return;
            }

            _coroutinesManager.StopLocationCoroutine();
            _mapHolder.Camera.enabled = false;
            _mainCamera.enabled = true;
            Destroy(_mapHolder.gameObject);
            _mapHolder = null;
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
                new MainViewInitializationParameters(BuildProfileView, () => BuildHelpView(BuildMainView), BuildSettingsView, StartPath,
                    () =>
                    {
                        UrbanPath path = _applicationData.UrbanPaths[0];
                        StartUrbanPath(path);
                        BuildPathView(path);
                    }, Application.Quit, "Rozpocznij ścieżkę", "Zobacz ścieżkę"));
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

        private void StartUrbanPath(UrbanPath urbanPath)
        {
            InitializeDemoMap(urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
        }

        private void BuildPathView(UrbanPath path)
        {
            _viewManager.OpenView(ViewType.Path, new PathViewInitializationParameters(
                BuildMainView, ShowNextStation, () => BuildHelpView(() => BuildPathView(path)), BuildMainView, path.DisplayedName
            ));
        }

        private void ShowNextStation()
        {
            
        }
        
        private void BuildSettingsView()
        {
            _viewManager.OpenView(ViewType.Settings, new SettingsInitializationParameters(BuildMainView, () => { },
                () => { }, () => { }, () => { }));
        }

        private void BuildHelpView(UnityAction returnAction)
        {
            _viewManager.OpenView(ViewType.Help, new HelpViewInitializationParameters(new List<ListElement>()
            {
                new ListElement("Przykładowy przycisk pomocy", null, "Pomoc", () => { })
            }, returnAction));
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

        private void CancelPath()
        {
            _coroutinesManager.StopLocationCoroutine();
        }
    }
}