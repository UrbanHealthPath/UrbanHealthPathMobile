using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class PopupWithContents : MonoBehaviour, IPopup
    {
        [SerializeField]
        private RectTransform _popupArea;
        public RectTransform PopupArea
        {
            get { return _popupArea;  }
        }
        
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.anchoredPosition = new Vector2(payload.Position.x, payload.Position.y);
            // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            // rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            // rectTransform.SetPositionAndRotation(position, rotation);

        }

        public void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}