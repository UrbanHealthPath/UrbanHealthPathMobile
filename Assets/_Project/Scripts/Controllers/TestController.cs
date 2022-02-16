using System;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using UnityEngine.Events;


namespace PolSl.UrbanHealthPath.Controllers
{
    public class TestController : BaseController
    {
        private readonly CoroutineManager _coroutineManager;
        private readonly Settings _settings;

        private UnityAction _backToMainMenu;
        private UnityAction<Exercise> _exerciseStartStop;
    
        private TestProgress _currentTestProgress;
        private TestButtonGroup _currentTestButtonGroup;

        private bool _startStopState = false;
        
        public TestController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            Settings settings, UnityAction backToMainMenu) : base(viewManager, popupManager)
        {
            _backToMainMenu = backToMainMenu;
            _coroutineManager = coroutineManager;
            _settings = settings;
        }

        private void ShowMainTest()
        {
            ViewManager.OpenView(ViewType.TestView);
        }

        public void ShowTestIntroduction()
        {
            TestIntroductionInitializationParameters initParams =
                new TestIntroductionInitializationParameters(()=>ShowMainTest(), _backToMainMenu, ReturnToPreviousView);
            ViewManager.OpenView(ViewType.TestIntroduction, initParams);
        }

        private void ShowTestSummary()
        {
            TestSummaryInitializationParameters initParams =
                new TestSummaryInitializationParameters(() => FinishTestAction());
            ViewManager.OpenView(ViewType.TestSummary, initParams);
        }

        public void ConfigureButtonGroup(bool enableStartStop,
            Action<Exercise> exerciseStartStop, Action<Exercise> repeatingExercise,
            Action testFinished, TestButtonGroup buttons)
        {
            buttons.AddListenerToRepeatButton(() => RepeatButtonClicked(repeatingExercise));
            buttons.RepeatButton.SetInteractable(false);
            buttons.AddListenerToNextButton(()=>NextButtonClicked());
            buttons.NextButton.SetInteractable(false);
            buttons.AddListenerToTimerButton(()=>StartStopButtonClicked(exerciseStartStop));
            buttons.TimerButton.SetInteractable(true);
            _currentTestButtonGroup = buttons;
        }

        private void RepeatButtonClicked(Action<Exercise> repeatingExercise)
        {
            
        }

        private void StartStopButtonClicked(Action<Exercise> exerciseStarting)
        {
            
            if (!_startStopState)
            {
                //@todo start timer and update the text on the Test View every second
                
            }
            else
            {
                //@todo stop timer 
            }
        }

        private void NextButtonClicked()
        {
            //@todo add test summary to _currentTestProgress or show summary popup
        }

        private void FinishTestAction()
        {
            //@todo save test progress into a file
            _backToMainMenu.Invoke();
        }
    }
}
