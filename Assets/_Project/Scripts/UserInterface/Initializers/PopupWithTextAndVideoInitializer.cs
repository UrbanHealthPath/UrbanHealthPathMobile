using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine.Video;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class PopupWithTextAndVideoInitializer : Initializer
    {
        public string Text { get; }
        public string HeaderText { get; }
        public VideoClip Clip { get; }
        public PopupPayload Payload { get; }

        public PopupWithTextAndVideoInitializer(string headerText, string text, VideoClip clip, PopupPayload payload)
        {
            HeaderText = headerText;
            Text = text;
            Clip = clip;
            Payload = payload;
        }
    }
}