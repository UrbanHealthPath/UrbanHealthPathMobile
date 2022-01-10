using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class ConfirmationPopup :MonoBehaviour, IPopup, IInitializable, IDisplayable
    {
        public RectTransform PopupArea => popupArea;

        [SerializeField] private Button buttonFirst, buttonSecond;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform popupArea;
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        public void Initialize(Initializer initializer)
        {
            if (initializer is ConfirmationPopupInitializer init)
            {
                text.text = init.Information;

                if (init.NumberOfButtons == 0)
                {
                    buttonFirst.gameObject.SetActive(false);
                    buttonSecond.gameObject.SetActive(false);
                }
                else if (init.NumberOfButtons == 1)
                {
                    buttonFirst.gameObject.SetActive(true);
                    buttonSecond.gameObject.SetActive(false);

                    if (init.ButtonTexts.Length >= 1)
                    {
                        buttonFirst.GetComponentInChildren<TextMeshProUGUI>().text = init.ButtonTexts[0];
                    }

                    if (init.Actions.Length >= 1)
                    {
                        buttonFirst.onClick.AddListener(init.Actions[0]);
                    }
                }
                else
                {
                    buttonFirst.gameObject.SetActive(true);
                    buttonSecond.gameObject.SetActive(true);

                    if (init.ButtonTexts.Length >= 2)
                    {
                        buttonFirst.GetComponentInChildren<TextMeshProUGUI>().text = init.ButtonTexts[0];
                        buttonSecond.GetComponentInChildren<TextMeshProUGUI>().text = init.ButtonTexts[1];
                    }

                    if (init.Actions.Length >= 2)
                    {
                        buttonFirst.onClick.AddListener(() => init.Actions[0]?.Invoke());
                        buttonSecond.onClick.AddListener(() => init.Actions[1]?.Invoke());
                    }
                }
            }
        }

        public void Display()
        {
            throw new NotImplementedException();
        }

        public void StopDisplay()
        {
            throw new NotImplementedException();
        }
        private void OnDisable()
        {
            buttonFirst.onClick.RemoveAllListeners();
            buttonSecond.onClick.RemoveAllListeners();
        }
    }
}