using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class PopupManager : MonoBehaviour
    {
        public PopupType CurrentPopupType { get; private set; }
        
        [SerializeField] private Popup[] popupsWithTypes;

        private Dictionary<PopupType, GameObject> _popups;

        private GameObject _currentPopup;
        
        public void Initialize()
        {
            _currentPopup = null;
            _popups = new Dictionary<PopupType, GameObject>();
            
            foreach (var popup in popupsWithTypes)
            {
                _popups.Add(popup.GetPopupType(), popup.GetPopupObject());
            }
        }

        public GameObject OpenPopup(PopupType popupType, IPopupInitializationParameters initializationParameters = null)
        {
            CurrentPopupType = popupType;
            _currentPopup.Destroy();
            if (popupType != PopupType.None)
            {
                _currentPopup = Instantiate(_popups[popupType]);

                if (initializationParameters != null)
                {
                    IInitializablePopup initializablePopup = _currentPopup.GetComponent<IInitializablePopup>();
                    initializablePopup?.Initialize(initializationParameters);
                }

                return _currentPopup;
            }

            return null;
        }

        public void CloseCurrentPopup()
        {
            if (CurrentPopupType != PopupType.None)
            {
                CurrentPopupType = PopupType.None;
                _currentPopup.Destroy();
            }
        }
    }
}