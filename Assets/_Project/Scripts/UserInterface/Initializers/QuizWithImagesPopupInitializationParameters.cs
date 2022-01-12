using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class QuizWithImagesPopupInitializationParameters : IPopupInitializationParameters
    {
        public string Question { get;  }
        
        public PopupPayload Payload { get;  }

        public QuizElementOption[] QuizElementOptions { get; }
        
        public QuizWithImagesPopupInitializationParameters(string question, PopupPayload payload, QuizElementOption[] quizElementOptions)
        {
            Question = question;
            Payload = payload;
            QuizElementOptions = quizElementOptions;
        }

    }
}