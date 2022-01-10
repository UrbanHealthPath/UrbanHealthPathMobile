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
        public UnityAction FinishExerciseEvent { get; }
        public string HeaderText { get; }
        public string InformationAboutStation { get; }

        public StationViewInitializer(UnityAction sensorialEvent, UnityAction motorialEvent,
            UnityAction historicInfoEvent, UnityAction mainMenuEvent, UnityAction returnEvent,
            UnityAction finishExerciseEvent, string headerText, string info)
        {
            SensorialEvent += sensorialEvent;
            MotorialEvent += motorialEvent;
            HistoricInfoEvent += historicInfoEvent;
            MainMenuEvent += mainMenuEvent;
            ReturnEvent += returnEvent;
            FinishExerciseEvent += finishExerciseEvent;
            HeaderText = headerText;
            InformationAboutStation = info;
        }
    }
}