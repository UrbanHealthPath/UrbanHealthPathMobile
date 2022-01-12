using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class PathView : MonoBehaviour, IInitializable, IPopupable
    {
        public RectTransform PopupArea => popupArea;
        
        [SerializeField] private Button endPathButton, nextStationInfoButton, helpButton, mainMenuButton;
        [SerializeField] private Header header;
        [SerializeField] private RectTransform popupArea;
        [SerializeField] private AppearanceChangingButton appearanceChangingButton;

        private void Awake()
        {
            endPathButton.onClick.AddListener(appearanceChangingButton.SetDefaultAppearance);
            helpButton.onClick.AddListener(appearanceChangingButton.SetDefaultAppearance);
            mainMenuButton.onClick.AddListener(appearanceChangingButton.SetDefaultAppearance);
        }

        public void Initialize(Initializer initializer)
        {
            if (initializer is PathViewInitializer init)
            {
                header.Initialize(init.HeaderText);
                endPathButton.onClick.AddListener(() => init.EndPathEvent?.Invoke());
                helpButton.onClick.AddListener(()=>init.HelpEvent?.Invoke());
                nextStationInfoButton.onClick.AddListener(()=>init.NextStationInfoEvent.Invoke());
                mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
            }
        }
        
        public void OnDisable()
        {
            endPathButton.onClick.RemoveAllListeners();
            helpButton.onClick.RemoveAllListeners();
            nextStationInfoButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();
        }
    }
}