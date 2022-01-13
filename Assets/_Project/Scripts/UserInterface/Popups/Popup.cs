using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
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
}
