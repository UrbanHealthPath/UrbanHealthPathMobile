using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class PathChoiceView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button menuButton, tmpChoiceButton;

        public void Start()
        {
            menuButton.onClick.AddListener(GoToMainMenu);
            tmpChoiceButton.onClick.AddListener(StartPath);
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

        private void Return()
        {
            ViewManager.GetInstance().OpenView(ViewManager.GetInstance().LastViewType);
        }
        
        private void StartPath()
        {
            ViewManager.GetInstance().OpenView(ViewType.Path);
        }

        private void GoToMainMenu()
        {
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        public void OnDestroy()
        {
            menuButton.onClick.RemoveListener(GoToMainMenu);
            tmpChoiceButton.onClick.RemoveListener(StartPath);
        }
    }
}