using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class PopupWithTextAndVideo : MonoBehaviour, IPopup, IInitializable
    {
        public RectTransform PopupArea => popupArea;

        [SerializeField] private Header header;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private RectTransform popupArea;
        [SerializeField] private VideoPlayer videoPlayer;
        
        public void Initialize(Initializer initializer)
        {
            if (initializer is PopupWithTextAndVideoInitializer init)
            {
                header.Initialize(init.HeaderText);
                text.text = init.Text;

                videoPlayer.clip = init.Clip;
                videoPlayer.Play();

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