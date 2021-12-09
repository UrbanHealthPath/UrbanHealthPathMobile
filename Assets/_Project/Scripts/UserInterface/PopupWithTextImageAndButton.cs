using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class PopupWithTextImageAndButton : MonoBehaviour, IPopup, IDisplayable
    {
        [SerializeField] private Button buttonImHere;
        
        [SerializeField] private TextMeshProUGUI text;
        
        [SerializeField] private Image image;

        [SerializeField] private RectTransform _popupArea;
        public RectTransform PopupArea
        {
            get { return _popupArea; }
        }

        public String textContent { get; set; }

        private void Awake()
        {
            buttonImHere.onClick.AddListener(ConfirmArrival);
        }

        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        public void Display()
        {
            textContent = "Czy znajdujesz się w tym miejscu?";
            this.enabled = true;
            this.gameObject.SetActive(true);
            
            //textRef.SetText(textContent);

            text.text = textContent;
        }

        public void StopDisplay()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        private void ConfirmArrival()
        {
            PopupManager.GetInstance().ClosePopup();
            ViewManager.GetInstance().OpenView(ViewType.Station);
        }
        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}