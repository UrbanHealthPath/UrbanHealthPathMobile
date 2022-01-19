using System;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.DataLoaders;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManager;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class MainController : BaseController
    {
        private readonly PopupManager _popupManager;
        private readonly IPathProgressManager _pathProgressManager;
        private readonly IApplicationData _applicationData;
        private readonly MapHolder _mapHolderPrefab;
        private readonly CoroutineManager _coroutineManager;
        private readonly Settings _settings;
        private readonly AudioSource _audioSource;

        private LoginController _loginController;
        private MenuController _menuController;
        private SettingsController _settingsController;
        private HelpController _helpController;
        private PathController _pathController;
        private StationController _stationController;
        private ExerciseController _exerciseController;

        public MainController(ViewManager viewManager, PopupManager popupManager,
            IPathProgressManager pathProgressManager, IApplicationData applicationData, MapHolder mapHolderPrefab,
            CoroutineManager coroutineManager, Settings settings, AudioSource audioSource) : base(viewManager)
        {
            _popupManager = popupManager;
            _pathProgressManager = pathProgressManager;
            _applicationData = applicationData;
            _mapHolderPrefab = mapHolderPrefab;
            _coroutineManager = coroutineManager;
            _settings = settings;
            _audioSource = audioSource;
        }

        public void Run()
        {
            SubscribeToSettingsEvents();
            
            CreateControllers();
            SubscribeToMenuEvents();
            SubscribeToPathEvents();

            _loginController.ShowLoginScreenOnFirstRun(ReturnToMenu);
        }

        private void SubscribeToSettingsEvents()
        {
            _settings.IsAudioEnabledChanged += ChangeAudioStatus;
        }
        
        private void SubscribeToMenuEvents()
        {
            _menuController.SettingsButtonPressed += () => _settingsController.ShowSettings(
                ReturnToMenu, () => { }
            );

            _menuController.HelpButtonPressed += _helpController.ShowHelp;

            _menuController.TopMenuButtonPressed += HandleTopButtonClick;

            _menuController.BottomMenuButtonPressed += HandleBottomButtonClick;
        }

        private void SubscribeToPathEvents()
        {
            _pathController.PathStarted += path =>
                _pathController.ShowPathView(() =>
                {
                    Station nextStation = GetNextStation(path);
                    _stationController.ShowNextStationConfirmation(nextStation,
                        () =>
                        {
                            _stationController.ShowStation(nextStation, _exerciseController.ShowPopupForExercise, exercise => _popupManager.CloseCurrentPopup(), (
                                () =>
                                {
                                    _pathProgressManager.AddCheckpoint(
                                        new PathProgressCheckpoint(nextStation.WaypointId, DateTime.Now));
                                } ));
                        });
                }, () =>
                {
                    _popupManager.CloseCurrentPopup();
                }, _helpController.ShowHelp);
            _pathController.PathCancelled += path => ReturnToMenu();
            _pathController.PathCompleted += path => ReturnToMenu();
        }

        private void CreateControllers()
        {
            _loginController = new LoginController(ViewManager);
            _menuController = new MenuController(ViewManager);
            _settingsController = new SettingsController(ViewManager, _settings);
            _helpController = new HelpController(ViewManager);
            _pathController = new PathController(ViewManager, _pathProgressManager, ReturnToMenu,
                new LocationProviderFactory(new LocationPermissionRequester()), _mapHolderPrefab);
            _stationController = new StationController(ViewManager, _popupManager, _coroutineManager, _pathProgressManager, _audioSource);
            _exerciseController = new ExerciseController(ViewManager, _popupManager, _coroutineManager);
        }

        private void ReturnToMenu()
        {
            _menuController.ShowMenu(_pathProgressManager.IsPathInProgress);
        }

        private void HandleTopButtonClick()
        {
            if (_pathProgressManager.IsPathInProgress)
            {
                _pathController.CancelPath();
            }
            else
            {
                _pathController.ShowPathSelectionView(_applicationData.UrbanPaths, false);
            }
        }

        private void HandleBottomButtonClick()
        {
            _pathController.ShowPathSelectionView(_applicationData.UrbanPaths, true);
        }
        
        private Station GetNextStation(UrbanPath path)
        {
            IList<Waypoint> pathWaypoints = path.Waypoints.Select(waypoint => waypoint.Value).ToList();
            
            PathProgressCheckpoint lastCheckpoint = _pathProgressManager.LastCheckpoint;

            if (lastCheckpoint is null)
            {
                return (Station) pathWaypoints.FirstOrDefault(x => x is Station);
            }

            Waypoint lastWaypoint = pathWaypoints.Single(x => x.WaypointId == lastCheckpoint.WaypointId);
            int indexOfLastWaypoint = pathWaypoints.IndexOf(lastWaypoint);

            for (int i = indexOfLastWaypoint + 1; i < pathWaypoints.Count; i++)
            {
                if (pathWaypoints[i] is Station station)
                {
                    return station;
                }
            }

            return null;
        }

        private void ChangeAudioStatus(bool isAudioEnabled)
        {
            _audioSource.mute = !isAudioEnabled;
        }
    }
}