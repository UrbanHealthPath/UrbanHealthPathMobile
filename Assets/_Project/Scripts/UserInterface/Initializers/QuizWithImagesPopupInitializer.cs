using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class QuizWithImagesPopupInitializer : Initializer
    {
        public string Question { get;  }
        
        public PopupPayload Payload { get;  }
        
        public Texture2D Texture1 { get; }
        public UnityAction ButtonTexture1Action { get; }
        
        public Texture2D Texture2 { get; }
        public UnityAction ButtonTexture2Action { get; }
        
        public Texture2D Texture3 { get; }
        public UnityAction ButtonTexture3Action { get; }
        
        public Texture2D Texture4 { get; }
        public UnityAction ButtonTexture4Action { get; }
        
        public QuizWithImagesPopupInitializer(string question, PopupPayload payload, Texture2D texture1, UnityAction buttonTexture1Action, Texture2D texture2, UnityAction buttonTexture2Action, Texture2D texture3, UnityAction buttonTexture3Action, Texture2D texture4, UnityAction buttonTexture4Action)
        {
            Question = question;
            Payload = payload;
            Texture1 = texture1;
            ButtonTexture1Action += buttonTexture1Action;
            Texture2 = texture2;
            ButtonTexture2Action += buttonTexture2Action;
            Texture3 = texture3;
            ButtonTexture3Action += buttonTexture3Action;
            Texture4 = texture4;
            ButtonTexture4Action += buttonTexture4Action;
        }

    }
}