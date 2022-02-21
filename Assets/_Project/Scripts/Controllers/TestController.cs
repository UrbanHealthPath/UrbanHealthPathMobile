using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.PathData;
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

        private UnityAction _backToMainMenu;

        private TestProgress _currentTestProgress;
        private Test _currentTest;
        private TestButtonGroup _currentTestButtonGroup;

        private TestExerciseSummary _currentSummary;

        private bool _startStopState = false;
        private bool _nextButtonState = false;

        private float _timer = 0f;
        private bool _runTimer = false;
        private UnityAction<float> TimeUpdatedEvent;

        private float _startingTime;

        private Action<Exercise> _exerciseStarting;
        private Action<Exercise> _exerciseEnding;

        public TestController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            UnityAction backToMainMenu, Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding) : base(
            viewManager, popupManager)
        {
            _backToMainMenu = backToMainMenu;
            _coroutineManager = coroutineManager;
            _exerciseStarting = exerciseStarting;
            _exerciseEnding = exerciseEnding;
        }

        public void ShowTestIntroduction(Test test)
        {
            TestIntroductionInitializationParameters initParams =
                new TestIntroductionInitializationParameters(() => ShowTest(test), _backToMainMenu,
                    ReturnToPreviousView);
            ViewManager.OpenView(ViewType.TestIntroduction, initParams);
        }

        private void ShowTest(Test test)
        {
            CleanTestData();
            _currentTest = test;

            TestViewInitializationParameters initParams = new TestViewInitializationParameters(buttons =>
                ConfigureButtonGroup(
                    buttons), () => ShowConfirmation("Czy na pewno chcesz zakończyć Test?",
                () => _backToMainMenu.Invoke()), () =>
            {
                PopupManager.CloseCurrentPopup();
                ReturnToPreviousView();
            }, TimeUpdatedEvent, "Test Sprawnościowy");
            _currentTestProgress = new TestProgress();
            var testObj = ViewManager.OpenView(ViewType.TestView, initParams);

            TestView testView = testObj.GetComponent<TestView>();

            if (testView)
            {
                TimeUpdatedEvent = testView.TimeUpdated;
            }
        }

        private void CleanTestData()
        {
            _currentTest = null;
        }

        private void ShowTestSummary()
        {
            TestSummaryInitializationParameters initParams =
                new TestSummaryInitializationParameters(() => FinishTestAction());
            ViewManager.OpenView(ViewType.TestSummary, initParams);
        }

        private void ConfigureButtonGroup(TestButtonGroup buttons)
        {
            buttons.AddListenerToRepeatButton(RepeatButtonClicked);
            buttons.RepeatButton.SetInteractable(false);
            buttons.AddListenerToNextButton(NextButtonClicked);
            buttons.NextButton.SetInteractable(false);
            buttons.AddListenerToTimerButton(StartStopButtonClicked);

            if (_currentTest != null)
            {
                buttons.TimerButton.SetInteractable(true);
            }
            else
            {
                buttons.TimerButton.SetInteractable(false);
            }

            buttons.NextButton.SetButtonText("Podsumuj ćwiczenie", Vector4.zero); //quick fix because i have no idea why
            //the text is different
            _currentTestButtonGroup = buttons;
        }

        private void RepeatButtonClicked()
        {
            ResetTimer();
            SetTimerButtonStartState();
            _currentTestButtonGroup.NextButton.SetInteractable(false);
            _nextButtonState = false;
        }

        private void StartStopButtonClicked()
        {
            if (!_startStopState)
            {
                _currentTestButtonGroup.NextButton.SetInteractable(true);
                _currentTestButtonGroup.RepeatButton.SetInteractable(true);
                _exerciseStarting.Invoke(GetCurrentExercise());
                SetTimerButtonStopState();
                _startingTime = Time.time;
                _coroutineManager.StartCoroutine(CountTime());
                //@todo reset and start timer and update the text on the Test View every second
            }
            else
            {
                SetTimerButtonStartState();
                _coroutineManager.StopCoroutine(CountTime());
            }
        }

        private void NextButtonClicked()
        {
            if (!_nextButtonState)
            {
                if (_currentTestProgress.GetFinishedExercisesCount() != _currentTest.Exercises.Count-1)
                {
                    _currentTestButtonGroup.NextButton.SetButtonText("Następne ćwiczenie", Vector4.zero);
                }
                else
                {
                    _currentTestButtonGroup.NextButton.SetButtonText("Zakończ\ntest", Vector4.zero);
                }

                _currentTestButtonGroup.TimerButton.SetInteractable(false);
                _currentTestButtonGroup.RepeatButton.SetInteractable(false);
                
                Exercise currentExercise = GetCurrentExercise();
                _exerciseEnding.Invoke(currentExercise);
                SetTimerButtonStopState();
            }
            else
            {
                _currentTestButtonGroup.NextButton.SetButtonText("Podsumuj ćwiczenie", Vector4.zero);
                _currentTestButtonGroup.TimerButton.SetInteractable(true);

                Exercise currentExercise = GetCurrentExercise();
                _currentTestProgress.AddNewSummary(new TestExerciseSummary(currentExercise.ExerciseId, (int) _timer));
                ResetTimer();
                SetTimerButtonStartState();
                _coroutineManager.StopCoroutine(CountTime());
                
                if (!StartExercise())
                {
                    ShowTestSummary();
                }
            }

            _nextButtonState = !_nextButtonState;
        }

        private bool StartExercise()
        {
            Exercise exercise = GetCurrentExercise();

            if (exercise is null)
            {
                return false;
            }
            
            _exerciseStarting.Invoke(exercise);
            return true;
        }

        private Exercise GetCurrentExercise()
        {
            if (_currentTestProgress is null)
            {
                return null;
            }

            IList<Exercise> exercises = _currentTest.Exercises.Select(x => x.Value).ToList();

            int finishedExercisesCount = _currentTestProgress.GetFinishedExercisesCount();
            int currentExerciseIndex = 0;

            if (finishedExercisesCount > 0)
            {
                string lastExerciseId = _currentTestProgress
                    .ExerciseSummaries[_currentTestProgress.ExerciseSummaries.Count - 1].ExerciseId;
                Exercise lastFinishedExercise = exercises.FirstOrDefault(x => x.ExerciseId == lastExerciseId);
                currentExerciseIndex = exercises.IndexOf(lastFinishedExercise) + 1;
            }

            return currentExerciseIndex < exercises.Count ? exercises[currentExerciseIndex] : null;
        }

        private void FinishTestAction()
        {
            _backToMainMenu.Invoke();
        }

        private void SetTimerButtonStopState()
        {
            _startingTime = Time.time;
            _currentTestButtonGroup.TimerButton.SetButtonText("Zatrzymaj stoper", Vector4.zero);
            _startStopState = true;
            _runTimer = true;
        }

        private void SetTimerButtonStartState()
        {
            _currentTestButtonGroup.TimerButton.SetButtonText("Włącz\nstoper", Vector4.zero);
            _startStopState = false;
            _runTimer = false;
        }

        private IEnumerator CountTime()
        {
            while (_runTimer)
            {
                _timer += Time.time - _startingTime;
                _startingTime = Time.time;
                TimeUpdatedEvent.Invoke(_timer);
                yield return new WaitForSeconds(1);
            }
        }

        private void ResetTimer()
        {
            _timer = 0f;
            TimeUpdatedEvent.Invoke(_timer);
            _startingTime = Time.time;
            TimeUpdatedEvent.Invoke(_timer);
        }
    }
}