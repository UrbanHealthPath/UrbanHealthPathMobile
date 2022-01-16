using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class MenuController
    {
        public event Action ProfileButtonPressed;
        public event Action HelpButtonPressed;
        public event Action SettingsButtonPressed;
        public event Action TopMenuButtonPressed;
        public event Action BottomMenuButtonPressed;

        private readonly MainController _mainController;
        private readonly ViewManager _viewManager;

        public MenuController(MainController mainController, ViewManager viewManager)
        {
            _mainController = mainController;
            _viewManager = viewManager;
        }

        public void ShowMenu(bool isPathInProgress)
        {
            string upperButtonText = isPathInProgress ? "Rozpocznij nową ścieżkę" : "Rozpocznij ścieżkę";
            string lowerButtonText = isPathInProgress ? "Kontynuuj ścieżkę" : "Demonstracja ścieżki";

            MainViewInitializationParameters initParams = new MainViewInitializationParameters(OnProfileButtonPressed,
                OnHelpButtonPressed, OnSettingsButtonPressed,
                () => OnTopMenuButtonPressed(isPathInProgress), () => OnBottomMenuButtonPressed(isPathInProgress),
                OnQuitApplicationButtonPressed, upperButtonText, lowerButtonText);

            _viewManager.OpenView(ViewType.Main, initParams);
        }

        private void OnProfileButtonPressed()
        {
            ProfileButtonPressed?.Invoke();
        }

        private void OnHelpButtonPressed()
        {
            HelpButtonPressed?.Invoke();
        }

        private void OnSettingsButtonPressed()
        {
            SettingsButtonPressed?.Invoke();
        }

        private void OnTopMenuButtonPressed(bool isPathInProgress)
        {
            TopMenuButtonPressed?.Invoke();
        }

        private void OnBottomMenuButtonPressed(bool isPathInProgress)
        {
            BottomMenuButtonPressed?.Invoke();
        }

        private void OnQuitApplicationButtonPressed()
        {
            Application.Quit();
        }
    }
}