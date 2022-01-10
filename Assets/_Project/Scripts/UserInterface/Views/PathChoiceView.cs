using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class PathChoiceView : MonoBehaviour, IDisplayable, IInitializable
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Header header;
        [SerializeField] private ListPanel list;
        public void Awake()
        {
            header.Initialize("Wybór ścieżki");
        }
        public void Initialize(Initializer initializer)
        {
            if (initializer is PathChoiceViewInitializer init)
            {
                list.Initialize(init.Elements);
                menuButton.onClick.AddListener(()=>init.MainMenuEvent?.Invoke());
            }
        }
        public void Display()
        {
        }

        public void StopDisplay()
        {
        }

        public void OnDisable()
        {
            menuButton.onClick.RemoveAllListeners();
        }
    }
}