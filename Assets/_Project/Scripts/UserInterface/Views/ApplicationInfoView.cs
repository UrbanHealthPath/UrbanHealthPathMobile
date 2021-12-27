using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class ApplicationInfoView : MonoBehaviour, IDisplayable
    {

        [SerializeField] private Button backButton, forwardButton;
        [SerializeField] private Header header;
        
        public void Start()
        {
            backButton.onClick.AddListener(GoBack);
            forwardButton.onClick.AddListener(GoForward);

            HeaderInitializer initializer = new HeaderInitializer("Wyjaśnienie ikon");
            header.Initialize(initializer);
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
