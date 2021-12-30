using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class StationViewInitializer : Initializer
    {
        public UnityAction SensorialEvent { get; }
        public UnityAction MotorialEvent { get; }
        public UnityAction HistoricInfoEvent { get; }
        public UnityAction MainMenuEvent { get; }
        public UnityAction ReturnEvent { get; }
        public string HeaderText { get; }

        public StationViewInitializer(UnityAction sensorialEvent, UnityAction motorialEvent,
            UnityAction historicInfoEvent, UnityAction mainMenuEvent, UnityAction returnEvent, string headerText)
        {
            SensorialEvent += sensorialEvent;
            MotorialEvent += motorialEvent;
            HistoricInfoEvent += historicInfoEvent;
            MainMenuEvent += mainMenuEvent;
            ReturnEvent += returnEvent;
            HeaderText = headerText;
        }
    }
}