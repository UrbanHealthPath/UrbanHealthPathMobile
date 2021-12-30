using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class MainViewInitializer : Initializer
    {
        public UnityAction ProfileEvent { get; }
        public UnityAction HelpEvent { get; }
        public UnityAction SettingsEvent { get; }
        public UnityAction CheckPathEvent { get; }
        public UnityAction StartPathEvent { get; }
        public UnityAction ContinuePathEvent { get; }
        public UnityAction EndPathEvent { get; }
        public UnityAction ExitEvent { get; }

        public MainViewInitializer(UnityAction profileEvent, UnityAction helpEvent, UnityAction settingsEvent,
            UnityAction
                checkPathEvent, UnityAction startPathEvent, UnityAction continuePathEvent, UnityAction endPathEvent,
            UnityAction
                exitEvent)
        {
            ProfileEvent += profileEvent;
            HelpEvent += helpEvent;
            SettingsEvent += settingsEvent;
            CheckPathEvent += checkPathEvent;
            StartPathEvent += startPathEvent;
            ContinuePathEvent += continuePathEvent;
            EndPathEvent += endPathEvent;
            ExitEvent += exitEvent;
        }
    }
}