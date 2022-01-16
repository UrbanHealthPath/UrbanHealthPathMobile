using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.PathData.DataLoaders;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManager;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class MainController : BaseController
    {
        private readonly PopupManager _popupManager;
        private readonly IPathProgressManager _pathProgressManager;
        private readonly IApplicationData _applicationData;
        private readonly MapHolder _mapHolderPrefab;
        private readonly CoroutineManager _coroutineManager;

        private LoginController _loginController;
        private MenuController _menuController;
        private SettingsController _settingsController;
        private HelpController _helpController;
        private PathController _pathController;
        private StationController _stationController;

        public MainController(ViewManager viewManager, PopupManager popupManager,
            IPathProgressManager pathProgressManager, IApplicationData applicationData, MapHolder mapHolderPrefab,
            CoroutineManager coroutineManager) : base(viewManager)
        {
            _popupManager = popupManager;
            _pathProgressManager = pathProgressManager;
            _applicationData = applicationData;
            _mapHolderPrefab = mapHolderPrefab;
            _coroutineManager = coroutineManager;

            CreateControllers();
            SubscribeToMenuEvents();
            SubscribeToPathEvents();

            _loginController.ShowLoginScreenOnFirstRun(ReturnToMenu);
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
                _pathController.ShowPathView(() => _stationController.ShowNextStationConfirmation(path),
                    _helpController.ShowHelp);
            _pathController.PathCancelled += path => ReturnToMenu();
        }

        private void CreateControllers()
        {
            _loginController = new LoginController(ViewManager);
            _menuController = new MenuController(ViewManager);
            _settingsController = new SettingsController(ViewManager);
            _helpController = new HelpController(ViewManager);
            _pathController = new PathController(ViewManager, _pathProgressManager, ReturnToMenu,
                new LocationProviderFactory(new LocationPermissionRequester()), _mapHolderPrefab);
            _stationController = new StationController(ViewManager, _popupManager, _coroutineManager, _pathProgressManager);
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
    }
}