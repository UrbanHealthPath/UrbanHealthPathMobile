using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class MainView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {
        [SerializeField] private Button profileButton, helpButton, settingsButton, startPathButton, checkPathButton, exitButton;
        [SerializeField] private Header header;
        [SerializeField] private RectTransform popupArea;
        public RectTransform PopupArea => popupArea;

        public void Start()
        {
            profileButton.onClick.AddListener(DisplayProfile);
            settingsButton.onClick.AddListener(DisplaySettings);
            helpButton.onClick.AddListener(DisplayHelpMenu);
            startPathButton.onClick.AddListener(StartPath);
            checkPathButton.onClick.AddListener(CheckPath);
            exitButton.onClick.AddListener(QuitApplication);
        }

        public void Initialize()
        {
            
        }
        public void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void StopDisplay()
        {
            this.gameObject.SetActive(false);
        }

        private void DisplayProfile()
        {
            ViewManager.GetInstance().OpenView(ViewType.Profile);
        }

        private void DisplaySettings()
        {
            ViewManager.GetInstance().OpenView(ViewType.Settings);
        }

        private void DisplayHelpMenu()
        {
            ViewManager.GetInstance().OpenView(ViewType.Help);
        }

        private void StartPath()
        {
            Debug.Log("Start path");
            ViewManager.GetInstance().OpenView(ViewType.PathChoice);
        }

        private void CheckPath()
        {
            Debug.Log("Check path");
        }
        private void QuitApplication()
        {
            Debug.Log("Quit");
            
         //   ViewManager.GetInstance().OpenView(ViewType.Main);
           // transform.GetComponent<>()
        }

        public void OnDestroy()
        {
            profileButton.onClick.RemoveListener(DisplayProfile);
            settingsButton.onClick.RemoveListener(DisplaySettings);
            helpButton.onClick.RemoveListener(DisplayHelpMenu);
            startPathButton.onClick.RemoveListener(StartPath);
            checkPathButton.onClick.RemoveListener(CheckPath);
            exitButton.onClick.RemoveListener(QuitApplication);
        }

        public void Initialize(Initializer initializer)
        {
            throw new NotImplementedException();
        }
    }
}
