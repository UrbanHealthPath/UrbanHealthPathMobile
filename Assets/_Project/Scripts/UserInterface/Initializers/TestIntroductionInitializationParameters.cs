using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class TestIntroductionInitializationParameters : IViewInitializationParameters
    {
        public UnityAction StartTestAction { get; }
        
        public UnityAction MainWindowAction { get; }
        
        public UnityAction ReturnAction { get; }

        public TestIntroductionInitializationParameters(UnityAction startTestAction, UnityAction mainWindowAction,
            UnityAction returnAction)
        {
            StartTestAction = startTestAction;
            MainWindowAction = mainWindowAction;
            ReturnAction = returnAction;
        }
    }
}
