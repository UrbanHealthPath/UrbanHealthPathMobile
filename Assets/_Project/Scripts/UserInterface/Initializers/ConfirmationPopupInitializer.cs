using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class ConfirmationPopupInitializer : Initializer
    {
        public string Information { get; private set; }
        public int NumberOfButtons { get; private set; }
        public string[] ButtonTexts { get; private set; }
        public UnityAction[] Actions { get; private set; }

        public ConfirmationPopupInitializer(string information, int numberOfButtons = 0, string[] buttonTexts = null,
            UnityAction[] actions = null)
        {
            Information = information;
            NumberOfButtons = Mathf.Clamp(numberOfButtons, 0, 2);
            ButtonTexts = buttonTexts;
            Actions = actions;
        }
        
    }
}