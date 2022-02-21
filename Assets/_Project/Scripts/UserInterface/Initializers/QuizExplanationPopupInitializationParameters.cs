using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class QuizExplanationPopupInitializationParameters : IPopupInitializationParameters
    {
        public PopupPayload Payload { get;  }
        public string[] Explanations { get; }
        public Texture[] Images { get; }

        public QuizExplanationPopupInitializationParameters(PopupPayload payload, string[] explanations, Texture[] images)
        {
            Payload = payload;
            Explanations = explanations;
            Images = images;
        }
    }
}
