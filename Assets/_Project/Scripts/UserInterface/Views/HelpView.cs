using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class HelpView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button returnButton;

        public void Start()
        {
            returnButton.onClick.AddListener(Return);
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
        public void OnDestroy()
        {
            returnButton.onClick.RemoveListener(Return);
        }
    }
}
