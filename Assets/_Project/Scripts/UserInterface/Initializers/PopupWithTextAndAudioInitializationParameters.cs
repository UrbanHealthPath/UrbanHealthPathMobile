
using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;


namespace PolSl.UrbanHealthPath
{
    public class PopupWithTextAndAudioInitializationParameters : IPopupInitializationParameters
    {
        public string HistoricalInformationText { get; }
        public PopupPayload Payload { get; }
        public AudioClip Clip { get; }
        public string ButtonTextOnState { get; }
        public string ButtonTextOffState { get; }

        public PopupWithTextAndAudioInitializationParameters(string historicalInformationText, PopupPayload payload,
            AudioClip clip, string buttonTextOnState, string buttonTextOffState)
        {
            HistoricalInformationText = historicalInformationText;
            Payload = payload;
            ButtonTextOffState = buttonTextOffState;
            ButtonTextOnState = buttonTextOnState;
            Clip = clip;
        }
    }
}
