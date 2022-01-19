using PolSl.UrbanHealthPath.UserInterface.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class StationViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction<ChangingButton> SensorialEvent { get; }
        public UnityAction<ChangingButton> MotorialEvent { get; }
        public UnityAction<ChangingButton> HistoricInfoEvent { get; }
        public UnityAction MainMenuEvent { get; }
        public UnityAction ReturnEvent { get; }
        public UnityAction<ButtonWithAudio> PlayAction { get; }
        public AudioClip AudioClip { get; }
        public string HeaderText { get; }
        public string InformationAboutStation { get; }
        
        public StationViewInitializationParameters(UnityAction<ChangingButton> sensorialEvent, UnityAction<ChangingButton> motorialEvent,
            UnityAction<ChangingButton> historicInfoEvent, UnityAction mainMenuEvent, UnityAction returnEvent,
            string headerText, string info, UnityAction<ButtonWithAudio> playAction, AudioClip audioClip)
        {
            SensorialEvent += sensorialEvent;
            MotorialEvent += motorialEvent;
            HistoricInfoEvent += historicInfoEvent;
            MainMenuEvent += mainMenuEvent;
            ReturnEvent += returnEvent;
            HeaderText = headerText;
            InformationAboutStation = info;
            PlayAction = playAction;
            AudioClip = audioClip;
        }
    }
}