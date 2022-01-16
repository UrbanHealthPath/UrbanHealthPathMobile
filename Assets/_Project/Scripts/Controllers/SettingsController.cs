using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Views;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class SettingsController : BaseController
    {
        public SettingsController(ViewManager viewManager) : base(viewManager)
        {
        }

        public void ShowSettings(Action returnButtonPressed, Action revertDefaultsButtonPressed)
        {
            IViewInitializationParameters initParams = new SettingsInitializationParameters(() => returnButtonPressed.Invoke(),
                () => revertDefaultsButtonPressed.Invoke(), OnChangeFontSizeButtonPressed, OnChangeThemeButtonPressed,
                OnToggleAudioButtonPressed);
            ViewManager.OpenView(ViewType.Settings, initParams);
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