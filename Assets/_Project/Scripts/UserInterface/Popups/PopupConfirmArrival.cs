using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class PopupConfirmArrival : MonoBehaviour, IPopup, IInitializable
    {
        public RectTransform PopupArea => popupArea;

        [SerializeField] private Button buttonImHere;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform popupArea;
        [SerializeField] private ImageFitter fitter;
        
        public void Initialize(Initializer initializer)
        {
            if (initializer is PopupConfirmArrivalInitializer init)
            {
                buttonImHere.onClick.AddListener(init.ButtonAction);
                text.text = init.Text;
                InitSizeAndPosition(init.Payload);
                fitter.InitializeImage(init.Texture);
            }
        }

        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }
        
    }
}