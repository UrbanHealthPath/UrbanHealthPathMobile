using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class StationView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {
        [SerializeField] private Button sensorialButton, motorialButton, historicInfoButton, mainMenuButton, returnButton;

        [SerializeField] private Header header;

        [SerializeField] private RectTransform _popupArea;

        public RectTransform PopupArea
        {
            get { return _popupArea; }
        }
        public void Initialize(Initializer initializer)
        {
            if (initializer is StationViewInitializer init)
            {
                motorialButton.onClick.AddListener(() => init.MotorialEvent?.Invoke());
                sensorialButton.onClick.AddListener(() => init.SensorialEvent?.Invoke());
                historicInfoButton.onClick.AddListener(() => init.HistoricInfoEvent?.Invoke());
                mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                
                header.Initialize(init.HeaderText);
            }
        }

        public void Display()
        {
            gameObject.SetActive(true);
        }

        public void StopDisplay()
        {
            gameObject.SetActive(false);
        }
        
        public void OnDisable()
        {
            sensorialButton.onClick.RemoveAllListeners();
            motorialButton.onClick.RemoveAllListeners();
            historicInfoButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();
            returnButton.onClick.RemoveAllListeners();
        }
    }
}