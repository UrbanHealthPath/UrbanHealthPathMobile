using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    public class MainView : MonoBehaviour, IView, IPopupable
    {
        private RectTransform _view;
        
        [SerializeField] private Button profileButton, helpButton, settingsButton, startPathButton, exitButton;

        [SerializeField]
        private RectTransform _popupArea;
        public RectTransform PopupArea
        {
            get { return _popupArea;  }
        }
        public void Start()
        {
            _view = GetComponent<RectTransform>();
            profileButton.onClick.AddListener(DisplayProfile);
            settingsButton.onClick.AddListener(DisplaySettings);
            helpButton.onClick.AddListener(DisplayHelpMenu);
            startPathButton.onClick.AddListener(StartPath);
            exitButton.onClick.AddListener(QuitApplication);
        }

        // public void Initialize()
        // {
        //     
        // }
        public void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void Close()
        {
            this.enabled = false;
            this.gameObject.SetActive(false);
        }

        private void DisplayProfile()
        {
            Debug.Log("profile");
            
        }

        private void DisplaySettings()
        {
            Debug.Log("settings");
            
        }

        private void DisplayHelpMenu()
        {
            Debug.Log("Help");
            
        }

        private void StartPath()
        {
            Debug.Log("Start path");
            ViewManager.GetInstance().OpenView(ViewType.Path);
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
            exitButton.onClick.RemoveListener(QuitApplication);
        }
    }
}
