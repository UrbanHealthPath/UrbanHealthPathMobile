using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class MainView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {
        [SerializeField] private Button profileButton,
            helpButton,
            settingsButton,
            startPathButton,
            checkPathButton,
            endPathButton,
            continuePathButton,
            exitButton;

        [SerializeField] private Header header;
        [SerializeField] private RectTransform popupArea;
        public RectTransform PopupArea => popupArea;

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
                startPathButton.onClick.AddListener(() => init.StartPathEvent?.Invoke());
                checkPathButton.onClick.AddListener(() => init.CheckPathEvent?.Invoke());
                endPathButton.onClick.AddListener(()=>init.EndPathEvent?.Invoke());
                continuePathButton.onClick.AddListener(()=>init.ContinuePathEvent?.Invoke());
                exitButton.onClick.AddListener(()=>init.ExitEvent?.Invoke());
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
            profileButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            helpButton.onClick.RemoveAllListeners();
            startPathButton.onClick.RemoveAllListeners();
            checkPathButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}