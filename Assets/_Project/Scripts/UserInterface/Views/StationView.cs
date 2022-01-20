using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class StationView : MonoBehaviour, IInitializableView, IPopupable
    {
        public RectTransform PopupArea => _popupArea;
        public ChangingButton RightButton => _rightButton;
        public ChangingButton MiddleButton => _middleButton;
        public ChangingButton LeftButton => _leftButton;

        [SerializeField] private ChangingButton _rightButton;
        [SerializeField] private ChangingButton _middleButton;
        [SerializeField] private ChangingButton _leftButton; 
        
        [FormerlySerializedAs("mainMenuButton")] [SerializeField]
        private Button _mainMenuButton;

        [FormerlySerializedAs("returnButton")] [SerializeField]
        private Button _returnButton;

        [SerializeField] private ButtonWithAudio _buttonWithAudio;

        [SerializeField] private HeaderPanel _headerPanel;
        
        [FormerlySerializedAs("informationAboutStation")] [SerializeField]
        private TextMeshProUGUI _informationAboutStation;

        [FormerlySerializedAs("popupArea")] [SerializeField]
        private RectTransform _popupArea;
        
        private UnityAction<ChangingButton> _historicButtonAction;
        private UnityAction<ChangingButton> _motorialButtonAction;
        private UnityAction<ChangingButton> _sensoricButtonAction;
        
        private bool _isPlaying = false;
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is StationViewInitializationParameters init)
            {
                if (init.MotorialEvent != null)
                    _middleButton.Button.onClick.AddListener(() =>
                        init.MotorialEvent?.Invoke(_middleButton));
                else
                    _middleButton.SetInteractable(false);

                if (init.SensorialEvent != null)
                    _rightButton.Button.onClick.AddListener(() =>
                        init.SensorialEvent?.Invoke(_rightButton));
                else
                    _rightButton.SetInteractable(false);

                if (init.HistoricInfoEvent != null)
                    _leftButton.Button.onClick.AddListener(() =>
                        init.HistoricInfoEvent?.Invoke(_leftButton));
                else
                    _leftButton.SetInteractable(false);

                if (init.AudioClip != null)
                {
                    _buttonWithAudio.Initialize(init.AudioClip, init.AudioButtonInitialized);
                    _buttonWithAudio.Button.onClick.AddListener(()=>init.PlayAction?.Invoke(_buttonWithAudio));
                }
                else
                {
                    _buttonWithAudio.Button.interactable = false;
                }

                _mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _headerPanel.Initialize(init.HeaderText);
                _informationAboutStation.text = init.InformationAboutStation;
            }
        }

        public void ResetActiveButtons()
        {
            if (_rightButton.Button.IsInteractable())
            {
                _rightButton.SetDefaultAppearance();
            }

            if (_middleButton.Button.IsInteractable())
            {
                _middleButton.SetDefaultAppearance();
            }

            if (_leftButton.Button.IsInteractable())
            {
                _leftButton.SetDefaultAppearance();
            }
        }

        public void OnDisable()
        {
            _rightButton.Button.onClick.RemoveAllListeners();
            _middleButton.Button.onClick.RemoveAllListeners();
            _leftButton.Button.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
            _buttonWithAudio.Button.onClick.RemoveAllListeners();
        }
        
    }
}