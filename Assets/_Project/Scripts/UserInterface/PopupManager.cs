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
            public PopupType type;
            public GameObject popupObject;
        }

        [SerializeField] private Popup[] popupsWithTypes;

        private Dictionary<PopupType, GameObject> _popups;

        private GameObject _currentPopup = null;

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

            _popups = new Dictionary<PopupType, GameObject>();
            foreach (var popup in popupsWithTypes)
            {
                _popups.Add(popup.type, popup.popupObject);
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
            _currentPopup.Destroy();
            _currentPopup = Instantiate(_popups[popupType]);
            IPopup iPopup = _currentPopup.GetComponent<IPopup>();

            if (payload != null)
            {
                iPopup.InitSizeAndPosition(payload);
                Debug.Log(" h = " + payload.Size.y + " w = "+ payload.Size.x + " x = " + payload.Position.x);
            }

            return _currentPopup.GetComponent<IPopup>();
        }
    }
}