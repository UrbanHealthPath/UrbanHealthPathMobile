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

        private Dictionary<ChangingButton, bool> _stationButtonStates;
        private StationProgress _currentStationProgress;

        private ButtonWithAudio _lastAudioButton;

        public StationController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            IPathProgressManager pathProgressManager, Settings settings) : base(viewManager, popupManager)
        {
            _coroutineManager = coroutineManager;
            _pathProgressManager = pathProgressManager;
            _settings = settings;

            _stationButtonStates = new Dictionary<ChangingButton, bool>();
            
            _settings.IsAudioEnabledChanged += SetLastAudioMute; 
        }

        public void ShowNextStationConfirmation(Station nextStation, Action confirmed)
        {
            _coroutineManager.StartCoroutine(ShowNextStationConfirmationPopup(nextStation, confirmed));
        }

        public void ShowStation(Station station, Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding,
            Action stationFinished)
        {
            SetCurrentStation(station);

            ViewManager.OpenView(ViewType.Station);

            UnityAction<ChangingButton> sensorialEvent = null;
            UnityAction<ChangingButton> motoricalEvent = null;
            UnityAction<ChangingButton> gameEvent = null;

            if (_currentStationProgress.GetCurrentExercise(ExerciseCategory.Game) != null)
            {
                gameEvent = (btn) =>
                    ConfigureExerciseButton(_stationButtonStates, btn, exerciseStarting, exerciseEnding,
                        ExerciseCategory.Game);
            }

            if (_currentStationProgress.GetCurrentExercise(ExerciseCategory.Motorical) != null)
            {
                motoricalEvent = (btn) => ConfigureExerciseButton(_stationButtonStates, btn, exerciseStarting,
                    exerciseEnding,
                    ExerciseCategory.Motorical);
            }

            if (_currentStationProgress.GetCurrentExercise(ExerciseCategory.Sensorial) != null)
            {
                sensorialEvent = (btn) => ConfigureExerciseButton(_stationButtonStates, btn, exerciseStarting,
                    exerciseEnding,
                    ExerciseCategory.Sensorial);
            }

            AudioClip introductionAudio = new AudioFileAccessor(station.IntroductionAudio).GetMedia();

            StationViewInitializationParameters initParams =
                new StationViewInitializationParameters(sensorialEvent, motoricalEvent,
                    gameEvent,
                    () => ShowConfirmation("Czy na pewno chcesz zakończyć ćwiczenia na tym punkcie?",
                        () => stationFinished.Invoke()), () =>
                    {
                        PopupManager.CloseCurrentPopup();
                        ReturnToPreviousView();
                    },
                    station.DisplayedName, station.Introduction, AudioButtonInitializedHandler, AudioButtonHandler,
                    introductionAudio);

            ViewManager.InitializeCurrentView(initParams);
        }

        private void ConfigureExerciseButton(Dictionary<ChangingButton, bool> buttonsStates, ChangingButton btn,
            Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding, ExerciseCategory category)
        {
            btn.SetDefaultAppearance();

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

            if (type == ViewType.Station)
            {
                _stationButtonStates.Clear();
            }

            StopLastAudioIntroduction();
        }

        protected override void PopupOpenedHandler(PopupType type)
        {
            base.PopupOpenedHandler(type);

            StopLastAudioIntroduction();
        }

        protected override void PopupClosedHandler(PopupType type)
        {
            base.PopupClosedHandler(type);

            if (ViewManager.CurrentViewType == ViewType.Station && type == PopupType.Confirmation)
            {
                _stationButtonStates.Clear();
                ViewManager.CurrentView.GetComponent<StationView>().ResetActiveButtons();
            }
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