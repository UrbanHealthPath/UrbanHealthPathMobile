using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class PopupConfirmArrivalInitializer : Initializer
    {
        public UnityAction ButtonAction { get; }
        public string Text { get; }
        public Texture2D Texture { get; }
        public PopupPayload Payload { get; }

        public PopupConfirmArrivalInitializer(UnityAction buttonAction, string text,
            Texture2D texture, PopupPayload payload)
        {
            ButtonAction = buttonAction;
            Text = text;
            Texture = texture;
            Payload = payload;
        }
    }
}