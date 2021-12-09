using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class ApplicationInfoView : MonoBehaviour, IDisplayable
    {

        [SerializeField] private Button backButton, forwardButton;
        
        public void Start()
        {
            backButton.onClick.AddListener(GoBack);
            forwardButton.onClick.AddListener(GoForward);
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

        private void GoBack()
        {
            ViewManager.GetInstance().OpenView(ViewType.Login);
        }

        private void GoForward()
        {
            ViewManager.GetInstance().OpenView(ViewType.IconInfo);
        }
        public void OnDestroy()
        {
            backButton.onClick.RemoveListener(GoBack);
            forwardButton.onClick.RemoveListener(GoForward);
        }
    }
}
