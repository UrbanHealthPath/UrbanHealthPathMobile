using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PolSl.UrbanHealthPath.Controllers;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.DataLoaders;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Tools.TextLogger;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManager;
using PolSl.UrbanHealthPath.Utils.PersistentValue;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using ILocationProvider = PolSl.UrbanHealthPath.Map.ILocationProvider;
using Random = UnityEngine.Random;

namespace PolSl.UrbanHealthPath.SceneInitializer
{
    public class SceneInitializer : MonoBehaviour
    {
        [FormerlySerializedAs("_coroutinesManager")] [SerializeField] private CoroutineManager _coroutineManager;

        [SerializeField] private MapHolder _mapHolderPrefab;

        [SerializeField] private GameObject _uiManager;

        private ITextLogger _logger;
        private IApplicationData _applicationData;
        private IPathProgressManager _pathProgressManager;
        private IPersistentValue<bool> _isFirstRun;
        private ViewManager _viewManager;
        private PopupManager _popupManager;
        private MapHolder _mapHolder;
        private UrbanPath _currentPath;
        private Exercise _currentExercise;
        private ILocationProvider _currentLocationProvider;

        private void Awake()
        {
            _logger = new UnityLogger();

            _applicationData = LoadApplicationData();
            _pathProgressManager =
                new PathProgressManager(new JsonFilePathProgressPersistor(Application.dataPath + "/progress.json",
                    JsonSerializer.Create()));

            GameObject uiManager = Instantiate(_uiManager);

            _viewManager = uiManager.GetComponent<ViewManager>();
            _viewManager.Initialize();

            _popupManager = uiManager.GetComponent<PopupManager>();
            _popupManager.Initialize();

            MainController mainController = new MainController(_viewManager, _popupManager, _pathProgressManager);

            //BuildUI();
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
            ILocationUpdater locationUpdater = new LocationUpdater(locationProvider);

            _mapHolder = Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(locationUpdater, coordinatesList);
            
            locationUpdater.UpdateLocation();
        }

        private void InitializeMap(List<Coordinates> coordinatesList)
        {
            DestroyMap();

            ILocationProvider locationProvider =
                new LocationFactory(new LocationPermissionRequester()).CreateDeviceProvider();
            ILocationUpdater locationUpdater = new LocationUpdater(locationProvider);

            _currentLocationProvider = locationProvider;            
            
            _mapHolder = Instantiate(_mapHolderPrefab);
            _mapHolder.Initialize(locationUpdater, coordinatesList);
        }

        private void DestroyMap()
        {
            if (_mapHolder is null)
            {
                return;
            }
            
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
            MainViewInitializationParameters initParams;

            if (_pathProgressManager.IsPathInProgress)
            {
                initParams = new MainViewInitializationParameters(BuildProfileView,
                    () => BuildHelpView(BuildMainView), BuildSettingsView,
                    CancelPath,
                    BuildPathView, Application.Quit, "Rozpocznij nową ścieżkę", "Kontynuuj ścieżkę");
            }
            else
            {
                initParams = new MainViewInitializationParameters(BuildProfileView,
                    () => BuildHelpView(BuildMainView), BuildSettingsView,
                    StartPath, StartDemoPath, Application.Quit, "Rozpocznij ścieżkę", "Zobacz ścieżkę");
            }

            _viewManager.OpenView(ViewType.Main, initParams);
        }

        private void StartDemoPath()
        {
            _viewManager.OpenView(ViewType.PathChoice, new PathChoiceViewInitializationParameters(
                GetAvailablePaths(path =>
                {
                    _logger.Log(LogVerbosity.Debug, "Started demo path " + path.DisplayedName);
                    _currentPath = path;
                    StartNewDemoPath(_currentPath);
                    BuildPathView();
                }),
                BuildMainView));
        }

        private void StartPath()
        {
            _viewManager.OpenView(ViewType.PathChoice, new PathChoiceViewInitializationParameters(
                GetAvailablePaths(path =>
                {
                    _logger.Log(LogVerbosity.Debug, "Started path " + path.DisplayedName);
                    _currentPath = path;
                    StartNewPath(_currentPath);
                    BuildPathView();
                }),
                BuildMainView));
        }

        private void StartNewPath(UrbanPath urbanPath)
        {
            _pathProgressManager.StartNewPath();
            InitializeMap(urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
        }

        private void StartNewDemoPath(UrbanPath urbanPath)
        {
            _pathProgressManager.StartNewPath();
            InitializeDemoMap(urbanPath.Waypoints.Select(x => x.Value.Coordinates).ToList());
        }

        private void BuildPathView()
        {
            _viewManager.OpenView(ViewType.Path, new PathViewInitializationParameters(
                CancelPath, () => ShowNextStationConfirmation(GetNextStation()), () => BuildHelpView(BuildPathView),
                BuildMainView, _currentPath.DisplayedName
            ));
        }

        private void ShowNextStationConfirmation(Station nextStation)
        {
            StartCoroutine(ShowNextStationConfirmationPopup(nextStation, () =>
            {
                _popupManager.CloseCurrentPopup();
                BuildStationView(nextStation);
            }));
        }

        private IEnumerator ShowNextStationConfirmationPopup(Station nextStation, UnityAction confirmationButtonAction)
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new TextureFileAccessor(nextStation.Image).GetMedia();
            RectTransform transform = _viewManager.CurrentView.GetComponent<PathView>().PopupArea;

            _popupManager.OpenPopup(PopupType.ConfirmArrival,
                new PopupConfirmArrivalInitializationParameters(confirmationButtonAction, "Czy jesteś tutaj?", texture,
                    new PopupPayload(transform)));
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
                new ListElement("Przykładowy przycisk pomocy", null, "Pomoc", () => { _logger.Log(LogVerbosity.Debug, "Pomoc");})
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
            _pathProgressManager.CancelPath();
            _currentPath = null;
            BuildMainView();
        }

        private Station GetNextStation()
        {
            PathProgressCheckpoint lastCheckpoint = _pathProgressManager.LastCheckpoint;
            IList<Waypoint> waypoints = _applicationData.Waypoints;

            if (lastCheckpoint is null)
            {
                return (Station) waypoints.FirstOrDefault(x => x is Station);
            }

            Waypoint lastWaypoint = waypoints.Single(x => x.WaypointId == lastCheckpoint.WaypointId);
            int indexOfLastWaypoint = waypoints.IndexOf(lastWaypoint);

            for (int i = indexOfLastWaypoint + 1; i < waypoints.Count; i++)
            {
                if (waypoints[i] is Station station)
                {
                    return station;
                }
            }

            return null;
        }

        private void BuildStationView(Station station)
        {
            //TODO: Gets random exercise, fix it

            _viewManager.OpenView(ViewType.Station);

            IReadOnlyList<Exercise> gameExercises = station.GetExercisesOfCategory(ExerciseCategory.Game);
            IReadOnlyList<Exercise> motoricalExercises = station.GetExercisesOfCategory(ExerciseCategory.Motorical);
            IReadOnlyList<Exercise> sensorialExercises = station.GetExercisesOfCategory(ExerciseCategory.Sensorial);

            Exercise gameExercise = gameExercises.Count > 0
                ? gameExercises.ElementAt(Random.Range(0, gameExercises.Count))
                : null;
            Exercise motoricalExercise = motoricalExercises.Count > 0
                ? motoricalExercises.ElementAt(Random.Range(0, motoricalExercises.Count))
                : null;
            Exercise sensorialExercise = sensorialExercises.Count > 0
                ? sensorialExercises.ElementAt(Random.Range(0, sensorialExercises.Count))
                : null;

            HistoricalFact fact = station.HistoricalFacts.Count > 0
                ? station.HistoricalFacts.ElementAt(Random.Range(0, station.HistoricalFacts.Count))
                : null;

            UnityAction<ChangingButton> sensorialEvent = null;
            UnityAction<ChangingButton> motoricalEvent = null;
            UnityAction<ChangingButton> gameEvent = null;

            if (gameExercise != null)
            {
                gameEvent = (btn) => CreatePopupForExercise(gameExercise);
            }

            if (motoricalExercise != null)
            {
                motoricalEvent = (btn) => CreatePopupForExercise(motoricalExercise);
            }

            if (sensorialExercise != null)
            {
                sensorialEvent = (btn) => CreatePopupForExercise(sensorialExercise);
            }

            StationViewInitializationParameters initParams =
                new StationViewInitializationParameters(sensorialEvent, motoricalEvent,
                    gameEvent, () => FinishStation(station), () =>
                    {
                        ClearPopup();
                        BuildPathView();
                    },
                    station.DisplayedName, fact?.Description ?? station.DisplayedName);

            _viewManager.InitializeCurrentView(initParams);
        }

        private void CreatePopupForExercise(Exercise exercise)
        {
            _currentExercise = exercise;
            StartCoroutine(CreatePopupForExerciseEnumerator(exercise));
        }

        private IEnumerator CreatePopupForExerciseEnumerator(Exercise exercise)
        {
            yield return new WaitForEndOfFrame();
            IPopupable popupableView = _viewManager.CurrentView.GetComponent<IPopupable>();

            ClearPopup();

            switch (exercise.Levels[0])
            {
                case VideoExerciseLevel videoExerciseLevel:
                    _popupManager.OpenPopup(PopupType.WithTextAndVideo,
                        new PopupWithTextAndVideoInitializationParameters(exercise.DisplayedName,
                            videoExerciseLevel.Description,
                            new VideoFileAccessor(videoExerciseLevel.VideoFile).GetMedia(),
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                case ImageExerciseLevel imageExerciseLevel:
                    _popupManager.OpenPopup(PopupType.WithTextAndImage,
                        new PopupWithTextAndImageInitializationParameters(exercise.DisplayedName,
                            imageExerciseLevel.Description,
                            new TextureFileAccessor(imageExerciseLevel.ImageFile).GetMedia(),
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                case ImageSelectionExerciseLevel imageSelectionExerciseLevel:
                    List<QuizElementOption> quizElementOptions = new List<QuizElementOption>();

                    foreach (LateBoundValue<MediaFile> image in imageSelectionExerciseLevel.Images)
                    {
                        Texture2D texture = new TextureFileAccessor(image).GetMedia();
                        bool isCorrect = imageSelectionExerciseLevel.CorrectAnswers.Contains(imageSelectionExerciseLevel.Images.IndexOf(image));

                        quizElementOptions.Add(new QuizElementOption(texture,
                            () => _logger.Log(LogVerbosity.Debug, isCorrect ? "Correct answer" : "Wrong answer")));
                    }

                    _popupManager.OpenPopup(PopupType.QuizWithImages,
                        new QuizWithImagesPopupInitializationParameters(imageSelectionExerciseLevel.Question,
                            new PopupPayload(popupableView.PopupArea),
                            quizElementOptions.ToArray()
                        ));
                    break;
                case TextExerciseLevel textExerciseLevel:
                    _popupManager.OpenPopup(PopupType.WithTextAndImage,
                        new PopupWithTextAndImageInitializationParameters(exercise.DisplayedName,
                            textExerciseLevel.Description,
                            null,
                            new PopupPayload(popupableView.PopupArea)));
                    break;
                default:
                    break;
            }
        }

        private void FinishStation(Station station)
        {
            _logger.Log(LogVerbosity.Debug, $"Finished station {station.WaypointId}");

            _pathProgressManager.AddCheckpoint(new PathProgressCheckpoint(station.WaypointId, DateTime.Now));

            ClearPopup();

            if (IsPathFinished())
            {
                FinishPath();
            }
            else
            {
                _currentLocationProvider.GetLocation();
                BuildPathView();
            }
        }

        private void FinishExercise(Exercise exercise)
        {
            _logger.Log(LogVerbosity.Debug, $"Exercise {exercise.ExerciseId} completed!");
        }

        private bool IsPathFinished()
        {
            return _pathProgressManager.LastCheckpoint.WaypointId ==
                   _currentPath.Waypoints[_currentPath.Waypoints.Count - 1].Value.WaypointId;
        }

        private void FinishPath()
        {
            _pathProgressManager.CompletePath();
            BuildMainView();
        }

        private void ClearPopup()
        {
            if (_popupManager.CurrentPopupType != PopupType.None)
            {
                _popupManager.CloseCurrentPopup();
            }
        }
    }
}