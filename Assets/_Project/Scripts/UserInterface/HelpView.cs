using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath
{
    public class HelpView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button menuButton, returnButton;

        public void Start()
        {
            returnButton.onClick.AddListener(Return);
            menuButton.onClick.AddListener(GoToMainMenu);
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

        private void GoToMainMenu()
        {
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        public void OnDestroy()
        {
            returnButton.onClick.RemoveListener(Return);
            menuButton.onClick.RemoveListener(GoToMainMenu);
        }
    }
}
