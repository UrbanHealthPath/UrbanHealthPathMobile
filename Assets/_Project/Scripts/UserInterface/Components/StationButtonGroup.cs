using System;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using PolSl.UrbanHealthPath.UserInterface.Views;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    public class StationButtonGroup : MonoBehaviour
    {
        [SerializeField] private ChangingButton _sensorialButton;
        [SerializeField] private ChangingButton _motoricalButton;
        [SerializeField] private ChangingButton _gameButton;
        [SerializeField] private Sprite _activeIcon;
        
        private Action _unregisterFromStationEvents;
        private Action _unregisterFromPopupAndViewEvents;
        private ChangingButton _lastActivatedButton;
        
        public ChangingButton SensorialButton => _sensorialButton;
        public ChangingButton MotoricalButton => _motoricalButton;
        public ChangingButton GameButton => _gameButton;
    

        public void Initialize(UnityAction<StationButtonGroup> initialized)
        {
            initialized?.Invoke(this);
        }

        private void OnDisable()
        {
            RemoveAllListeners();
            _unregisterFromStationEvents?.Invoke();
        }

        public void RegisterToStationProgressEvents(StationProgress progress)
        {
            _unregisterFromStationEvents?.Invoke();

            progress.CategoryCompleted += DisableButtonBasedOnCategory;
            progress.ExerciseCompleted += HandleExerciseCompleted;

            _unregisterFromStationEvents = () =>
            {
                progress.CategoryCompleted -= DisableButtonBasedOnCategory;
                progress.ExerciseCompleted -= HandleExerciseCompleted;
            };
        }

        public void RegisterToPopupEvents(PopupManager popupManager, ViewManager viewManager)
        {
            _unregisterFromPopupAndViewEvents?.Invoke();

            popupManager.PopupClosed += HandlePopupClosed;

            _unregisterFromPopupAndViewEvents = () =>
            {
                popupManager.PopupClosed -= HandlePopupClosed;

            };
        }
        
        public bool IsActivated(ChangingButton button)
        {
            return _lastActivatedButton == button;
        }

        public void ActivateCategoryButton(ExerciseCategory category)
        {
            DeactivateButton(_lastActivatedButton);
            
            ChangingButton button = GetButtonForCategory(category);

            button.SetButtonText("Zatwierdź", new Vector4(10, 10, 10, 10));
            button.SetSprite(_activeIcon);
            _lastActivatedButton = button;
        }

        public void DeactivateCategoryButton(ExerciseCategory category)
        {
            ChangingButton button = GetButtonForCategory(category);

            if (IsActivated(button))
            {
                DeactivateButton(button);
            }
        }

        private void DisableButtonBasedOnCategory(Station station, ExerciseCategory category)
        {
            ChangingButton button = GetButtonForCategory(category);
            
            button.SetDefaultAppearance();
            button.SetInteractable(false);
        }

        private void DeactivateButton(ChangingButton button)
        {
            if (button is null)
            {
                return;
            }
            
            button.SetDefaultAppearance();
            _lastActivatedButton = null;
        }

        private void HandleExerciseCompleted(Station station, Exercise exercise)
        {
            DeactivateCategoryButton(exercise.Category);
        }

        private void HandlePopupClosed(PopupType type)
        {
            if (type == PopupType.Confirmation)
            {
                _gameButton.SetDefaultAppearance();
                _motoricalButton.SetDefaultAppearance();
                _sensorialButton.SetDefaultAppearance();
                _lastActivatedButton = null;
            }
        }

        public ChangingButton GetButtonForCategory(ExerciseCategory category)
        {
            return category switch
            {
                ExerciseCategory.Game => _gameButton,
                ExerciseCategory.Motorical => _motoricalButton,
                ExerciseCategory.Sensorial => _sensorialButton,
                _ => throw new ArgumentException("Invalid exercise category!", nameof(category))
            };
        }

        private void RemoveAllListeners()
        {
            _sensorialButton.Button.onClick.RemoveAllListeners();
            _motoricalButton.Button.onClick.RemoveAllListeners();
            _gameButton.Button.onClick.RemoveAllListeners();
        }
    }
}
