using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class PopupManager : MonoBehaviour
    {
        [Serializable]
        public struct Popup
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

        private GameObject _currentPopup = null;
        
        public PopupType CurrentPopupType { get; private set; }

        private static PopupManager _instance = null;

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
        }

        private void Start()
        {
            _instance._currentPopup = null;
            _instance._popups = new Dictionary<PopupType, GameObject>();
            foreach (var popup in _instance.popupsWithTypes)
            {
                _instance._popups.Add(popup.GetPopupType(), popup.GetPopupObject());
            }
        }

        public static PopupManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PopupManager>();
            }

            return _instance;
        }

        public IPopup OpenPopup(PopupType popupType, [CanBeNull] PopupPayload payload = null)
        {
            _instance.CurrentPopupType = popupType;
            _instance._currentPopup.Destroy();
            if (popupType != PopupType.None)
            {
                _instance._currentPopup = Instantiate(_popups[popupType]);
                IPopup iPopup = _currentPopup.GetComponent<IPopup>();

                if (payload != null)
                {
                    iPopup.InitSizeAndPosition(payload);
                    iPopup.Display();
                }

                return _currentPopup.GetComponent<IPopup>();
            }

            return null;
        }

        public void ClosePopup()
        {
            _instance.CurrentPopupType = PopupType.None;
            _instance._currentPopup.Destroy();
        }
    }
}