using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class PathController : BaseController
    {
        public event Action<UrbanPath> PathStarted;
        public event Action<UrbanPath> PathCancelled;
        public event Action<UrbanPath> PathCompleted;

        private readonly IPathProgressManager _pathProgressManager;
        private readonly Action _returnToMainMenu;
        private readonly ILocationProviderFactory _locationProviderFactory;
        private readonly MapHolder _mapHolderPrefab;

        private UrbanPath _selectedPath;
        private MapHolder _mapHolder;

        private bool _isNextStationButtonActive;

        public PathController(ViewManager viewManager, PopupManager popupManager,
            IPathProgressManager pathProgressManager,
            Action returnToMainMenu, ILocationProviderFactory locationProviderFactory,
            MapHolder mapHolderPrefab) : base(viewManager, popupManager)
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
                        ? BuildButtonsForAvailablePaths(availablePaths,
                            path => ShowPathPresentationView(path, StartNewDemoPath))
                        : BuildButtonsForAvailablePaths(availablePaths,
                            path => ShowPathPresentationView(path, StartNewPath)),
                    () => _returnToMainMenu.Invoke());

            ViewManager.OpenView(ViewType.PathChoice, initParams);
        }

        public void ShowPathPresentationView(UrbanPath path, Action<UrbanPath> startPath)
        {
            int stationsCount = path.GetWaypointsOfType<Station>().Count;
            Texture mapTexture = new TextureFileAccessor(path.PreviewImage).GetMedia();

            IViewInitializationParameters initParams = new PathPresentationViewInitializationParameters(
                () => _returnToMainMenu.Invoke(), ReturnToPreviousView, () => startPath.Invoke(path),
                path.DisplayedName, stationsCount, path.ApproximateDistanceInMeters, mapTexture);

            ViewManager.OpenView(ViewType.PathPresentation, initParams);
        }

        public void ShowPathView(Action nextStationButtonPressed, Action nextStationButtonCanceled,
            Action helpButtonPressed)
        {
            if (_selectedPath is null)
            {
                return;
            }

            ViewManager.OpenView(ViewType.Path, new PathViewInitializationParameters(
                () => ShowConfirmation("Czy na pewno chcesz zakończyć?", CancelPath),
                BuildNextStationButton(nextStationButtonPressed, nextStationButtonCanceled),
                () => helpButtonPressed.Invoke(),
                () => _returnToMainMenu.Invoke(), _selectedPath.DisplayedName
            ));
        }

        private UnityAction BuildNextStationButton(Action clickedInactive, Action clickedActive)
        {
            return () =>
            {
                if (_isNextStationButtonActive)
                {
                    clickedActive.Invoke();
                }
                else
                {
                    clickedInactive.Invoke();
                }

                _isNextStationButtonActive = !_isNextStationButtonActive;
            };
        }

        public void CancelPath()
        {
            _pathProgressManager.CancelPath();
            OnPathCancelled(_selectedPath);
        }

        private void SelectPath(UrbanPath path)
        {
            _selectedPath = path;
        }

        private List<ListElement> BuildButtonsForAvailablePaths(IList<UrbanPath> availablePaths,
            Action<UrbanPath> presentPath)
        {
            return availablePaths.Select(path => new ListElement(path.DisplayedName, null, "",
                () => presentPath.Invoke(path))).ToList();
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
            _pathProgressManager.CheckpointReached += CheckpointReachedHandler;
            _pathProgressManager.StartNewPath();
            InitializeMapHolder(urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
            OnPathStarted(urbanPath);
        }

        private void StartNewDemoPath(UrbanPath urbanPath)
        {
            SelectPath(urbanPath);
            _pathProgressManager.CheckpointReached += CheckpointReachedHandler;
            _pathProgressManager.StartNewPath();
            InitializeMapHolderForDemoPath(urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
            OnPathStarted(urbanPath);
        }

        private void CheckpointReachedHandler(object sender, CheckpointReachedEventArgs e)
        {
            if (e.Checkpoint.WaypointId == _selectedPath.Waypoints[_selectedPath.Waypoints.Count - 1].Value.WaypointId)
            {
                CompletePath();
            }
            else
            {
                ReturnToPreviousView();
            }
        }

        private void CompletePath()
        {
            _pathProgressManager.CompletePath();
            OnPathComplete(_selectedPath);
        }

        private void FinishPath()
        {
            _pathProgressManager.CheckpointReached -= CheckpointReachedHandler;
            _selectedPath = null;
        }

        private void OnPathStarted(UrbanPath path)
        {
            PathStarted?.Invoke(path);
        }

        private void OnPathCancelled(UrbanPath path)
        {
            FinishPath();
            PathCancelled?.Invoke(path);
        }

        private void OnPathComplete(UrbanPath path)
        {
            FinishPath();
            PathCompleted?.Invoke(path);
        }

        protected override void ViewOpenedHandler(ViewType type)
        {
            base.ViewOpenedHandler(type);

            if (type == ViewType.Path)
            {
                _isNextStationButtonActive = false;
            }
        }
    }
}