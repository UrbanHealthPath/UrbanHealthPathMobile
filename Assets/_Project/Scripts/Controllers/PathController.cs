using System;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class PathController : BaseController
    {
        public event Action<UrbanPath> PathStarted;
        public event Action<UrbanPath> PathCancelled;
        
        private readonly IPathProgressManager _pathProgressManager;
        private readonly Action _returnToMainMenu;
        private readonly ILocationProviderFactory _locationProviderFactory;
        private readonly MapHolder _mapHolderPrefab;

        private UrbanPath _selectedPath;
        private MapHolder _mapHolder;

        public PathController(ViewManager viewManager, IPathProgressManager pathProgressManager,
            Action returnToMainMenu, ILocationProviderFactory locationProviderFactory, MapHolder mapHolderPrefab) : base(viewManager)
        {
            _pathProgressManager = pathProgressManager;
            _returnToMainMenu = returnToMainMenu;
            _locationProviderFactory = locationProviderFactory;
            _mapHolderPrefab = mapHolderPrefab;
        }

        public void ShowPathSelectionView(IList<UrbanPath> availablePaths, bool areDemoPaths)
        {
            IViewInitializationParameters initParams =
                new PathChoiceViewInitializationParameters(
                    areDemoPaths
                        ? BuildButtonsForAvailablePaths(availablePaths, StartNewDemoPath)
                        : BuildButtonsForAvailablePaths(availablePaths, StartNewPath),
                    () => _returnToMainMenu.Invoke());

            ViewManager.OpenView(ViewType.PathChoice, initParams);
        }

        public void ShowPathView(Action nextStationButtonPressed, Action helpButtonPressed)
        {
            if (_selectedPath is null)
            {
                return;
            }
            
            ViewManager.OpenView(ViewType.Path, new PathViewInitializationParameters(
                CancelPath, () => nextStationButtonPressed.Invoke(), () => helpButtonPressed.Invoke(),
                () => _returnToMainMenu.Invoke(), _selectedPath.DisplayedName
            ));
        }
        
        public void CancelPath()
        {
            _pathProgressManager.CancelPath();
            OnPathCancelled(_selectedPath);
            _selectedPath = null;
        }

        private void SelectPath(UrbanPath path)
        {
            _selectedPath = path;
        }

        private List<ListElement> BuildButtonsForAvailablePaths(IList<UrbanPath> availablePaths,
            Action<UrbanPath> startPath)
        {
            return availablePaths.Select(path => new ListElement(path.DisplayedName, null, "",
                () => startPath.Invoke(path))).ToList();
        }

        private void InitializeMapHolderForDemoPath(List<Coordinates> coordinatesList)
        {
            DestroyMap();

            ILocationProvider locationProvider = _locationProviderFactory.CreateFakeProvider(coordinatesList);
            ILocationUpdater locationUpdater = new LocationUpdater(locationProvider);

            _mapHolder = UnityEngine.Object.Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(locationUpdater, coordinatesList);

            locationUpdater.UpdateLocation();
        }

        private void InitializeMapHolder(List<Coordinates> coordinatesList)
        {
            DestroyMap();

            ILocationProvider locationProvider = _locationProviderFactory.CreateDeviceProvider();
            ILocationUpdater locationUpdater = new LocationUpdater(locationProvider);

            _mapHolder = UnityEngine.Object.Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(locationUpdater, coordinatesList);
        }

        private void DestroyMap()
        {
            if (_mapHolder is null)
            {
                return;
            }

            UnityEngine.Object.Destroy(_mapHolder.gameObject);
            _mapHolder = null;
        }

        private void StartNewPath(UrbanPath urbanPath)
        {
            SelectPath(urbanPath);
            _pathProgressManager.StartNewPath();
            InitializeMapHolder(urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
            OnPathStarted(urbanPath);
        }

        private void StartNewDemoPath(UrbanPath urbanPath)
        {
            SelectPath(urbanPath);
            _pathProgressManager.StartNewPath();
            InitializeMapHolderForDemoPath(urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
            OnPathStarted(urbanPath);
        }

        private void OnPathStarted(UrbanPath path)
        {
            PathStarted?.Invoke(path);
        }
        
        private void OnPathCancelled(UrbanPath path)
        {
            PathCancelled?.Invoke(path);
        }
    }
}