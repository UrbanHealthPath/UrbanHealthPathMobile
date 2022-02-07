using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class TestSummaryInitializationParameters : IViewInitializationParameters
    {
        public UnityAction FinishTestAction;

        public TestSummaryInitializationParameters(UnityAction finishTestAction)
        {
            FinishTestAction = finishTestAction;
        }
    }
}
