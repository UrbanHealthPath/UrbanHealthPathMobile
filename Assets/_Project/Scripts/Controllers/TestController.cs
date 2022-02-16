using System;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.TestData;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;
using UnityEngine;
using UnityEngine.Events;


namespace PolSl.UrbanHealthPath.Controllers
{
    public class TestController : BaseController
    {
        private readonly CoroutineManager _coroutineManager;
        private readonly Settings _settings;

        private UnityAction _backToMainMenu;

        private TestProgress _currentTestProgress;
        private Test _test;
        private TestButtonGroup _currentTestButtonGroup;

        private TestExerciseSummary _currentSummary;

        private bool _startStopState = false;

        private bool _nextButtonState = false;
        
        public TestController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            Settings settings, UnityAction backToMainMenu, IList<Test> tests) : base(viewManager, popupManager)
        {
            _backToMainMenu = backToMainMenu;
            _coroutineManager = coroutineManager;
            _settings = settings;
            if (tests != null && tests.Count != 0)
            {
                _test = tests[0];//there is only one test in the file and it is the one that we want.
                                 //dont want to write a new data reader just for that one thingy
            }
            else
            {
                _test = null;
            }
        }

        private void ShowMainTest()
        {
            TestViewInitializationParameters initParams = new TestViewInitializationParameters(buttons =>
                ConfigureButtonGroup(
                    buttons), () => ShowConfirmation("Czy na pewno chcesz zakończyć Test?",
                () => _backToMainMenu.Invoke()), () =>
            {
                PopupManager.CloseCurrentPopup();
                ReturnToPreviousView();
            }, "Test Sprawnościowy");
            _currentTestProgress = new TestProgress();
            ViewManager.OpenView(ViewType.TestView, initParams);
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

        private void ConfigureButtonGroup(TestButtonGroup buttons)
        {
            buttons.AddListenerToRepeatButton(() => RepeatButtonClicked());
            buttons.RepeatButton.SetInteractable(false);
            buttons.AddListenerToNextButton(()=>NextButtonClicked());
            buttons.NextButton.SetInteractable(false);
            buttons.AddListenerToTimerButton(()=>StartStopButtonClicked());
            if (_test != null)
            {
                buttons.TimerButton.SetInteractable(true);
            }
            else
            {
                buttons.TimerButton.SetInteractable(false);
            }
            buttons.NextButton.SetButtonText("Podsumuj ćwiczenie", Vector4.zero);//quick fix because i have no idea why
                                                                                 //the text is different
            _currentTestButtonGroup = buttons;
        }

        private void RepeatButtonClicked()
        {
            SetTimerButtonStartText();
            _currentTestButtonGroup.NextButton.SetInteractable(false);
            _nextButtonState = false;
        }

        private void StartStopButtonClicked()
        {
            if (!_startStopState)
            {
                _currentTestButtonGroup.NextButton.SetInteractable(true);
                _currentTestButtonGroup.RepeatButton.SetInteractable(true);
                SetTimerButtonStopText();
                //@todo reset and start timer and update the text on the Test View every second
            }
            else
            {
                SetTimerButtonStartText();
                //@todo stop timer
            }
        }

        private void NextButtonClicked()
        {
            if (!_nextButtonState)
            {
                _currentTestButtonGroup.NextButton.SetButtonText("Następne ćwiczenie", Vector4.zero);
                _currentTestButtonGroup.TimerButton.SetInteractable(false);
                _currentTestButtonGroup.RepeatButton.SetInteractable(false);
                //@todo stop timer
                //@todo close exercise popup
                //@todo open TestPartialSummaryPopup
            }
            else
            {
                _currentTestButtonGroup.NextButton.SetButtonText("Podsumuj ćwiczenie", Vector4.zero);
                _currentTestButtonGroup.TimerButton.SetInteractable(true);
                SetTimerButtonStartText();
                //@todo reset timer
                //@todo add new summary to testProgress
                //@todo open next exercise popup
            }

            _nextButtonState = !_nextButtonState;
        }

        private void FinishTestAction()
        {
            //@todo save test progress into a file
            _backToMainMenu.Invoke();
        }

        private void OpenExercisePopup()
        {
            PopupManager.OpenPopup(PopupType.WithTextAndAudio);
        }

        private void OpenSummaryPopup()
        {
            PopupManager.OpenPopup(PopupType.TestPartialSummary);
        }

        private void SetTimerButtonStopText()
        {
            _currentTestButtonGroup.TimerButton.SetButtonText("Zatrzymaj licznik", Vector4.zero);
            _startStopState = true;
        }

        private void SetTimerButtonStartText()
        {
            _currentTestButtonGroup.TimerButton.SetButtonText("Rozpocznij ćwiczenie", Vector4.zero);
            _startStopState = false;
        }
    }
}
