using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using TMPro;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class PopupWithTextAndImage : MonoBehaviour, IPopup, IInitializable
    {
        public RectTransform PopupArea => popupArea;
        
        [SerializeField] private Header header;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform popupArea;
        [SerializeField] private ImageFitter fitter;
        [SerializeField] private RectTransform textArea;
        [SerializeField] private RectTransform imageArea;

        public void Initialize(Initializer initializer)
        {
            if (initializer is PopupWithTextAndImageInitializer init)
            {
                header.Initialize(init.HeaderText);
                if (init.Text == String.Empty || init.Text is null)
                {
                    textArea.gameObject.SetActive(false);
                }
                else
                {
                    text.text = init.Text;
                }

                if (init.Texture == null)
                {
                    imageArea.gameObject.SetActive(false);
                }
                else
                {
                    fitter.InitializeImage(init.Texture);
                }
                InitSizeAndPosition(init.Payload);
            }
        }

        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }
    }
}