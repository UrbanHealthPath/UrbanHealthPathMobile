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

        [Serializable] public struct Popup
        {
            [SerializeField] private PopupType type;
            [SerializeField] private GameObject popupObject;

            public PopupType GetPopupType()
            {
                return type;
            }

            public GameObject GetPopupObject()
            {
                return popupObject;
            }
        }

        [SerializeField] private Popup[] popupsWithTypes;

        private Dictionary<PopupType, GameObject> _popups;

        private GameObject _currentPopup;

        private static PopupManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            _instance._currentPopup = null;
            _instance._popups = new Dictionary<PopupType, GameObject>();
            foreach (var popup in popupsWithTypes)
            {
                _popups.Add(popup.GetPopupType(), popup.GetPopupObject());
            }
        }

        public static PopupManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PopupManager>();
            }

            return _instance;
        }

        public GameObject OpenPopup(PopupType popupType, Initializer initializer = null)
        {
            _instance.CurrentPopupType = popupType;
            _instance._currentPopup.Destroy();
            if (popupType != PopupType.None)
            {
                _instance._currentPopup = Instantiate(_popups[popupType]);

                if (initializer != null)
                {
                    IInitializable initializable = _instance._currentPopup.GetComponent<IInitializable>();
                    initializable?.Initialize(initializer);
                }

                return _currentPopup;
            }

            return null;
        }

        public void CloseCurrentPopup()
        {
            _instance.CurrentPopupType = PopupType.None;
            _instance._currentPopup.Destroy();
        }
    }
}