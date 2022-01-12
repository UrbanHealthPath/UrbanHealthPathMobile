using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class MainView : MonoBehaviour, IInitializable, IPopupable
    {
        public RectTransform PopupArea => popupArea;

        [SerializeField] private Button profileButton;
        [SerializeField] private Button helpButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button upperButtonOnMap;
        [SerializeField] private Button lowerButtonOnMap;
        [SerializeField] private Button exitButton;
        [SerializeField] private Header header;
        [SerializeField] private RectTransform popupArea;
        [SerializeField] private TextMeshProUGUI lowerButtonText;
        [SerializeField] private TextMeshProUGUI upperButtonText;

        public void Awake()
        {
            header.Initialize("Menu");
        }

        public void Initialize(Initializer initializer)
        {
            if (initializer is MainViewInitializer init)
            {
                profileButton.onClick.AddListener(() => init.ProfileEvent?.Invoke());
                settingsButton.onClick.AddListener(() => init.SettingsEvent?.Invoke());
                helpButton.onClick.AddListener(() => init.HelpEvent?.Invoke());
                upperButtonOnMap.onClick.AddListener(() => init.UpperButtonEvent?.Invoke());
                lowerButtonOnMap.onClick.AddListener(() => init.LowerButtonEvent?.Invoke());
                exitButton.onClick.AddListener(() => init.ExitEvent?.Invoke());
                lowerButtonText.text = init.LowerButtonText;
                upperButtonText.text = init.UpperButtonText;
            }
        }

        public void OnDisable()
        {
            profileButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            helpButton.onClick.RemoveAllListeners();
            upperButtonOnMap.onClick.RemoveAllListeners();
            lowerButtonOnMap.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}