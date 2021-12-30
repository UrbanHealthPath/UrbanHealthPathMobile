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

        public void Awake()
        {
            header.Initialize("Pomoc");
        }

        public void Initialize(Initializer initializer)
        {
            if (initializer is HelpViewInitializer init)
            {
                list.Initialize(init.Elements);
                returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
            }
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
        
        public void OnDisable()
        {
            returnButton.onClick.RemoveAllListeners();
        }
    }
}