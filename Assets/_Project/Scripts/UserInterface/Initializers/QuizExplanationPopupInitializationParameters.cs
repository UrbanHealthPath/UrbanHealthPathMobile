using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    /// <summary>
    /// A class that contains initialization parameters for QuizExplanationPopup.
    /// </summary>
    public class QuizExplanationPopupInitializationParameters : IPopupInitializationParameters
    {
        public PopupPayload Payload { get;  }
        public string FirstExplanation { get; }
        public string SecondExplanation { get; }
        public string ThirdExplanation { get; }
        public string FourthExplanation { get; }
        public Texture FirstTexture { get; }
        public Texture SecondTexture { get; }
        public Texture ThirdTexture { get; }
        public Texture FourthTexture { get; }

        public QuizExplanationPopupInitializationParameters(PopupPayload payload, string firstExplanation,
            string secondExplanation, string thirdExplanation, string fourthExplanation, Texture firstTexture,
            Texture secondTexture, Texture thirdTexture, Texture fourthTexture)
        {
            Payload = payload;
            FirstExplanation = firstExplanation;
            SecondExplanation = secondExplanation;
            ThirdExplanation = thirdExplanation;
            FourthExplanation = fourthExplanation;
            FirstTexture = firstTexture;
            SecondTexture = secondTexture;
            ThirdTexture = thirdTexture;
            FourthTexture = fourthTexture;
        }
    }
}
