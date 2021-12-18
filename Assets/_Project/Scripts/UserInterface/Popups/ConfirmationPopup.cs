using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class ConfirmationPopup : Popup, IPopup
    {
        [SerializeField] private Button buttonYes, buttonNo;
        [SerializeField] private TextMeshProUGUI text;   
        [SerializeField] private RectTransform popupArea;
        public RectTransform PopupArea => popupArea;

        private UnityAction _confirmed, _notConfirmed;

        private void Awake()
        {

        }

        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        public void Initialize(UnityAction confirmed, UnityAction notConfirmed, string question)
        {
            _confirmed = confirmed;
            _notConfirmed = notConfirmed;
            text.text = question;
            buttonYes.onClick.AddListener(_confirmed);
            buttonNo.onClick.AddListener(_notConfirmed);
        }

        private void OnDestroy()
        {
            buttonYes.onClick.RemoveListener(_confirmed);
            buttonNo.onClick.RemoveListener(_notConfirmed);
        }
    }
}
