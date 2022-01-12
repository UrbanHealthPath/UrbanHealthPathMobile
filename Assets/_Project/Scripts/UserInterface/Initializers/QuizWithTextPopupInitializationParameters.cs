using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class QuizWithTextPopupInitializationParameters : IPopupInitializationParameters
    {
        public string Question { get;  }
        public PopupPayload Payload { get;  }
        public string Text1 { get; }
        public UnityAction ButtonText1Action { get; }
        
        public string Text2 { get; }
        public UnityAction ButtonText2Action { get; }
        
        public string Text3 { get; }
        public UnityAction ButtonText3Action { get; }
        
        public string Text4 { get; }
        public UnityAction ButtonText4Action { get; }
        
        public QuizWithTextPopupInitializationParameters(string question, PopupPayload payload, string text1, 
            UnityAction buttonText1Action, string text2, UnityAction buttonText2Action, string text3, 
            UnityAction buttonText3Action, string text4, UnityAction buttonText4Action)
        {
            Question = question;
            Payload = payload;
            Text1 = text1;
            ButtonText1Action += buttonText1Action;
            Text2 = text2;
            ButtonText2Action += buttonText2Action;
            Text3 = text3;
            ButtonText3Action += buttonText3Action;
            Text4 = text4;
            ButtonText4Action += buttonText4Action;
        }
    }
}