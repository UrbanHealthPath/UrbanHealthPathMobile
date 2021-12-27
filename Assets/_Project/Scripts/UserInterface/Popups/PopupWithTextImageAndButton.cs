using System;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class PopupWithTextImageAndButton : MonoBehaviour, IPopup
    {
        [SerializeField] private Button buttonImHere;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image image;
        [SerializeField] private RectTransform popupArea;
        public RectTransform PopupArea => popupArea;

        public String TextContent { get; set; }

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
            TextContent = "Czy znajdujesz się w tym miejscu?";
            this.enabled = true;
            this.gameObject.SetActive(true);
            
            //textRef.SetText(textContent);

            text.text = TextContent;
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