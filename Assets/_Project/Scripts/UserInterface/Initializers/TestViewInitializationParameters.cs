using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class TestViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction<TestButtonGroup> ButtonGroupInitialized { get; }
        public UnityAction MainMenuEvent { get; }
        public UnityAction ReturnEvent { get; }
        public string HeaderText { get; }

        public TestViewInitializationParameters(UnityAction<TestButtonGroup> buttonGroupInitialized,
            UnityAction mainMenuEvent, UnityAction returnEvent, UnityAction startTestEvent, string headerText)
        {
            ButtonGroupInitialized += buttonGroupInitialized;
            MainMenuEvent += mainMenuEvent;
            ReturnEvent += returnEvent;
            HeaderText = headerText;
        }
    }
}
