using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Scalers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class QuizWithImagesPopup : MonoBehaviour, IPopup, IInitializable
    {
        public RectTransform PopupArea => popupArea;

        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform popupArea;
        [SerializeField] private ImageFitter fitter1;
        [SerializeField] private ImageFitter fitter2;
        [SerializeField] private ImageFitter fitter3;
        [SerializeField] private ImageFitter fitter4;
        [SerializeField] private Button imageButton1;
        [SerializeField] private Button imageButton2;
        [SerializeField] private Button imageButton3;
        [SerializeField] private Button imageButton4;
        
        public void Initialize(Initializer initializer)
        {
            if (initializer is QuizWithImagesPopupInitializer init)
            {
                InitSizeAndPosition(init.Payload);
                text.text = init.Question;

                fitter1.InitializeImage(init.Texture1);
                fitter2.InitializeImage(init.Texture2);
                fitter3.InitializeImage(init.Texture3);
                fitter4.InitializeImage(init.Texture4);

                imageButton1.onClick.AddListener(() => init.ButtonTexture1Action?.Invoke());
                imageButton2.onClick.AddListener(() => init.ButtonTexture2Action?.Invoke());
                imageButton3.onClick.AddListener(() => init.ButtonTexture3Action?.Invoke());
                imageButton4.onClick.AddListener(() => init.ButtonTexture4Action?.Invoke());
            }
        }


        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        private void OnDisable()
        {
            Debug.Log("disable");
            imageButton1.onClick.RemoveAllListeners();
            imageButton2.onClick.RemoveAllListeners();
            imageButton3.onClick.RemoveAllListeners();
            imageButton4.onClick.RemoveAllListeners();
        }
    }
}