using System;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManagement;


namespace PolSl.UrbanHealthPath.Controllers
{
    public class TestController : BaseController
    {
        private readonly CoroutineManager _coroutineManager;
        private readonly Settings _settings;
    
        private TestProgress _currentTestProgress;
        private TestButtonGroup _currentTestButtonGroup;

        private bool _startStopState = false;
        
        public TestController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            Settings settings) : base(viewManager, popupManager)
        {
            _coroutineManager = coroutineManager;
            _settings = settings;
        }

        public void ShowTest(Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding,
            Action<Exercise> repeatingExercise,
            Action testFinished)
        {
            _currentTestProgress = new TestProgress();

            ViewManager.OpenView(ViewType.TestView);

            TestViewInitializationParameters initParams = new TestViewInitializationParameters(buttons =>
                    ConfigureButtonGroup(true,
                        exerciseStarting, exerciseEnding, repeatingExercise, testFinished, buttons),
                () => ShowConfirmation("Czy na pewno chcesz zakończyć test?",
                    () => ReturnToPreviousView()), () =>
                {
                    PopupManager.CloseCurrentPopup();
                    ReturnToPreviousView();
                },
                "Test Sprawnościowy");
        }

        public void ConfigureButtonGroup(bool enableStartStop,
            Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding, Action<Exercise> repeatingExercise,
            Action testFinished, TestButtonGroup buttons)
        {
            buttons.AddListenerToRepeatButton(() => RepeatButtonClicked(repeatingExercise));
            buttons.RepeatButton.SetInteractable(false);
            buttons.AddListenerToNextButton(()=>NextButtonClicked());
            buttons.NextButton.SetInteractable(false);
            buttons.AddListenerToTimerButton(()=>StartStopButtonClicked(exerciseStarting, exerciseEnding));
            buttons.TimerButton.SetInteractable(true);
            _currentTestButtonGroup = buttons;
        }

        private void RepeatButtonClicked(Action<Exercise> repeatingExercise)
        {
            
        }

        private void StartStopButtonClicked(Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding)
        {
            
            if (!_startStopState)
            {
                //@todo start timer and update the text on the Test View every second
                
            }
        }

        private void NextButtonClicked()
        {
            
        }
    }
}
