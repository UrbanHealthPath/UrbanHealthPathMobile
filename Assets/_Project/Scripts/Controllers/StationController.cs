using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.MediaAccess;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
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
        private readonly AudioSource _audioSource;

        private Station _currentStation;
        private StationProgress _currentStationProgress;

        public StationController(ViewManager viewManager, PopupManager popupManager, CoroutineManager coroutineManager,
            IPathProgressManager pathProgressManager, AudioSource audioSource) : base(viewManager, popupManager)
        {
            _coroutineManager = coroutineManager;
            _pathProgressManager = pathProgressManager;
            _audioSource = audioSource;
        }

        public void ShowNextStationConfirmation(Station nextStation, Action confirmed)
        {
            _coroutineManager.StartCoroutine(ShowNextStationConfirmationPopup(nextStation, confirmed));
        }

        public void ShowStation(Station station, Action<Exercise> exerciseStarting, Action<Exercise> exerciseEnding, Action stationFinished)
        {
            SetCurrentStation(station);

            ViewManager.OpenView(ViewType.Station);

            UnityAction<ChangingButton> sensorialEvent = null;
            UnityAction<ChangingButton> motoricalEvent = null;
            UnityAction<ChangingButton> gameEvent = null;

            Dictionary<ChangingButton, bool> buttonsStates = new Dictionary<ChangingButton, bool>();

            if (_currentStationProgress.GetCurrentExercise(ExerciseCategory.Game) != null)
            {
                gameEvent = (btn) =>
                    ConfigureExerciseButton(buttonsStates, btn, exerciseStarting, exerciseEnding, ExerciseCategory.Game);
            }

            if (_currentStationProgress.GetCurrentExercise(ExerciseCategory.Motorical) != null)
            {
                motoricalEvent = (btn) => ConfigureExerciseButton(buttonsStates, btn, exerciseStarting, exerciseEnding, ExerciseCategory.Motorical);
            }

            if (_currentStationProgress.GetCurrentExercise(ExerciseCategory.Sensorial) != null)
            {
                sensorialEvent = (btn) => ConfigureExerciseButton(buttonsStates, btn, exerciseStarting, exerciseEnding, ExerciseCategory.Sensorial);
            }

            StationViewInitializationParameters initParams =
                new StationViewInitializationParameters(sensorialEvent, motoricalEvent,
                    gameEvent, () =>
                    {
                        
                        stationFinished.Invoke();
                    }, () =>
                    {
                        if (PopupManager.CurrentPopupType != PopupType.None)
                        {
                            PopupManager.CloseCurrentPopup();
                        }
                        
                        ReturnToPreviousView();
                    },
                    station.DisplayedName, station.Introduction);

            ViewManager.InitializeCurrentView(initParams);
            
            PlayIntroductionAudio(station.IntroductionAudio);
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
            _currentStation = station;
            _currentStationProgress = new StationProgress(station);
        }

        private void PlayIntroductionAudio(MediaFile introductionAudio)
        {
            _audioSource.clip = new AudioFileAccessor(introductionAudio).GetMedia();
            _audioSource.Play();
        }

        private void StopIntroductionAudio()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }
    }
}