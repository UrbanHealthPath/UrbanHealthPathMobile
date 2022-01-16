using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class SettingsController
    {
        private readonly ViewManager _viewManager;

        public SettingsController(ViewManager viewManager)
        {
            _viewManager = viewManager;
        }

        public void ShowSettings(Action returnButtonPressed, Action revertDefaultsButtonPressed)
        {
            IViewInitializationParameters initParams = new SettingsInitializationParameters(() => returnButtonPressed.Invoke(),
                () => revertDefaultsButtonPressed.Invoke(), OnChangeFontSizeButtonPressed, OnChangeThemeButtonPressed,
                OnToggleAudioButtonPressed);
            _viewManager.OpenView(ViewType.Settings, initParams);
        }

        private void OnChangeFontSizeButtonPressed()
        {
            
        }

        private void OnChangeThemeButtonPressed()
        {
            
        }

        private void OnToggleAudioButtonPressed()
        {
            
        }
    }
}