using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.Systems;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using PolSl.UrbanHealthPath.Utils.CoroutineManager;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Controllers
{
    public class StationController : BaseController
    {
        private readonly CoroutineManager _coroutineManager;
        private readonly IPathProgressManager _pathProgressManager;
        private readonly Settings _settings;

        private StationProgress _currentStationProgress;

        private ButtonWithAudio _lastAudioButton;

        public StationController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            IPathProgressManager pathProgressManager, Settings settings) : base(viewManager, popupManager)
        {
            _coroutineManager = coroutineManager;
            _pathProgressManager = pathProgressManager;
            _settings = settings;

            _settings.IsAudioEnabledChanged += SetLastAudioMute;
        }

        public void ShowNextStationConfirmation(Station nextStation, Action confirmed)
        {
            _coroutineManager.StartCoroutine(ShowNextStationConfirmationPopup(nextStation, confirmed));
        }

        public void ShowStation(Station station, Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding,
            Action<Station> stationFinished)
        {
            SetCurrentStation(station);

            ViewManager.OpenView(ViewType.Station);

            UnityAction<ChangingButton> sensorialEvent = null;
            UnityAction<ChangingButton> motoricalEvent = null;
            UnityAction<ChangingButton> gameEvent = null;

            bool isAnyGameExercise = _currentStationProgress.GetCurrentExercise(ExerciseCategory.Game) != null;
            bool isAnyMotoricalExercise =
                _currentStationProgress.GetCurrentExercise(ExerciseCategory.Motorical) != null;
            bool isAnySensorialExercise =
                _currentStationProgress.GetCurrentExercise(ExerciseCategory.Sensorial) != null;

            AudioClip introductionAudio = null;

            if (station.IntroductionAudio.Value != null)
            {
                introductionAudio = new AudioFileAccessor(station.IntroductionAudio)?.GetMedia();
            }

            StationViewInitializationParameters initParams =
                new StationViewInitializationParameters(
                    buttons => ConfigureButtonGroup(isAnyGameExercise, isAnyMotoricalExercise, isAnySensorialExercise,
                        exerciseStarting, exerciseEnding, buttons),
                    () => ShowConfirmation("Czy na pewno chcesz zakończyć ćwiczenia na tym punkcie?",
                        () => stationFinished.Invoke(station)), () =>
                    {
                        PopupManager.CloseCurrentPopup();
                        ReturnToPreviousView();
                    },
                    station.DisplayedName, "", AudioButtonInitializedHandler, AudioButtonHandler,
                    introductionAudio);

            ViewManager.InitializeCurrentView(initParams);
        }

        private void ConfigureButtonGroup(bool isAnyGame, bool isAnyMotorical, bool isAnySensorial,
            Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding, StationButtonGroup buttons)
        {
            buttons.RegisterToStationProgressEvents(_currentStationProgress);
            buttons.RegisterToPopupEvents(PopupManager, ViewManager);

            if (!isAnyGame)
            {
                buttons.GameButton.SetInteractable(false);
            }
            else
            {
                buttons.GameButton.Button.onClick.AddListener(() => ExerciseButtonClicked(ExerciseCategory.Game, buttons, exerciseStarting, exerciseEnding));
            }

            if (!isAnyMotorical)
            {
                buttons.MotoricalButton.SetInteractable(false);
            }
            else
            {
                buttons.MotoricalButton.Button.onClick.AddListener(() => ExerciseButtonClicked(ExerciseCategory.Motorical, buttons, exerciseStarting, exerciseEnding));
            }

            if (!isAnySensorial)
            {
                buttons.SensorialButton.SetInteractable(false);
            }
            else
            {
                buttons.SensorialButton.Button.onClick.AddListener(() => ExerciseButtonClicked(ExerciseCategory.Sensorial, buttons, exerciseStarting, exerciseEnding));
            }
        }

        private void ExerciseButtonClicked(ExerciseCategory category, StationButtonGroup buttons,
            Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding)
        {
            ChangingButton button = buttons.GetButtonForCategory(category);
            Exercise currentExercise = _currentStationProgress.GetCurrentExercise(category);

            if (buttons.IsActivated(button))
            {
                _currentStationProgress.CompleteCurrentExercise(category);
                exerciseEnding.Invoke(currentExercise);
            }
            else
            {
                buttons.ActivateCategoryButton(category);
                exerciseStarting?.Invoke(currentExercise);
            }
        }

        private void ConfigureExerciseButton(Dictionary<ChangingButton, bool> buttonsStates, ChangingButton btn,
            Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding, ExerciseCategory category)
        {
            //ViewManager.CurrentView.GetComponent<StationView>().ResetActiveButtons();
            buttonsStates.Clear();

            buttonsStates[btn] = buttonsStates.ContainsKey(btn) ? !buttonsStates[btn] : true;
            Exercise currentExercise = _currentStationProgress.GetCurrentExercise(category);

            if (buttonsStates[btn])
            {
                SetConfirmExerciseButton(btn);
                exerciseStarting.Invoke(currentExercise);
                return;
            }

            _currentStationProgress.CompleteCurrentExercise(category);

            if (_currentStationProgress.IsCategoryFinished(category))
            {
                SetCategoryFinishedButton(btn);
            }

            exerciseEnding.Invoke(currentExercise);
        }

        private void SetCategoryFinishedButton(ChangingButton btn)
        {
            btn.SetInteractable(false);
        }

        private void SetConfirmExerciseButton(ChangingButton btn)
        {
            btn.SetButtonText("Zatwierdź", new Vector4(10, 10, 10, 10));
        }

        private IEnumerator ShowNextStationConfirmationPopup(Station nextStation, Action confirmed)
        {
            yield return new WaitForEndOfFrame();
            Texture2D texture = new TextureFileAccessor(nextStation.Image).GetMedia();
            RectTransform transform = ViewManager.CurrentView.GetComponent<PathView>().PopupArea;

            PopupManager.OpenPopup(PopupType.ConfirmArrival,
                new PopupConfirmArrivalInitializationParameters(() =>
                    {
                        PopupManager.CloseCurrentPopup();
                        confirmed.Invoke();
                    },
                    "Czy jesteś tutaj?", texture,
                    new PopupPayload(transform)));
        }

        private void SetCurrentStation(Station station)
        {
            _currentStationProgress = new StationProgress(station);
        }

        private void AudioButtonInitializedHandler(ButtonWithAudio btn)
        {
            _lastAudioButton = btn;
            SetLastAudioMute(_settings.IsAudioEnabled);
            btn.ToggleState();
        }

        private void AudioButtonHandler(ButtonWithAudio btn)
        {
            _lastAudioButton = btn;
            btn.ToggleState();
        }

        protected override void ViewOpenedHandler(ViewType type)
        {
            base.ViewOpenedHandler(type);

            StopLastAudioIntroduction();
        }

        protected override void PopupOpenedHandler(PopupType type)
        {
            base.PopupOpenedHandler(type);

            StopLastAudioIntroduction();
        }

        private void StopLastAudioIntroduction()
        {
            if (_lastAudioButton != null)
            {
                _lastAudioButton.ForceStop();
            }
        }

        private void SetLastAudioMute(bool isEnabled)
        {
            if (_lastAudioButton != null)
            {
                _lastAudioButton.AudioSource.mute = !isEnabled;
            }
        }
    }
}