using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class MainController
    {
        private readonly ViewManager _viewManager;
        private readonly PopupManager _popupManager;
        private readonly IPathProgressManager _pathProgressManager;

        private LoginController _loginController;
        private MenuController _menuController;
        private SettingsController _settingsController;
        private HelpController _helpController;

        public MainController(ViewManager viewManager, PopupManager popupManager, IPathProgressManager pathProgressManager)
        {
            _viewManager = viewManager;
            _popupManager = popupManager;
            _pathProgressManager = pathProgressManager;

            CreateControllers();
            SubscribeToMenuEvents();
            
            _loginController.ShowLoginScreenOnFirstRun(ReturnToMenu);
        }

        private void SubscribeToMenuEvents()
        {
            _menuController.SettingsButtonPressed += () => _settingsController.ShowSettings(
                ReturnToMenu, () => { }
            );

            _menuController.HelpButtonPressed += () => _helpController.ShowHelp(ReturnToMenu);
        }

        private void CreateControllers()
        {
            _loginController = new LoginController(_viewManager);
            _menuController = new MenuController(this, _viewManager);
            _settingsController = new SettingsController(_viewManager);
            _helpController = new HelpController(_viewManager);
        }

        private void ReturnToMenu()
        {
            _menuController.ShowMenu(_pathProgressManager.IsPathInProgress);
        }
    }
}