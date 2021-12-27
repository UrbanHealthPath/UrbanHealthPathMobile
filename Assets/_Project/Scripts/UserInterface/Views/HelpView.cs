using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class HelpView : MonoBehaviour, IDisplayable, IInitializable
    {
        [SerializeField] private Button returnButton;
        [SerializeField] private Header header;
        [SerializeField] private ListPanel list;

        public void Start()
        {
            returnButton.onClick.AddListener(Return);
            HeaderInitializer initializer = new HeaderInitializer("Pomoc");
            header.Initialize(initializer);
        }

        public void Initialize(Initializer initializer)
        {
            // I don't know if it even should be there. Maybe we should initialize list from there, not from outside?
            throw new System.NotImplementedException();
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