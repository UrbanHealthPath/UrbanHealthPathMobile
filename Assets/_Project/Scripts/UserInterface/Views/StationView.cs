using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class StationView : MonoBehaviour, IInitializableView, IPopupable, IDisplayable
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("rightButton")] [SerializeField] private Button _rightButton;
        
        [FormerlySerializedAs("middleButton")] [SerializeField] private Button _middleButton;
        
        [FormerlySerializedAs("leftButton")] [SerializeField] private Button _leftButton;
        
        [FormerlySerializedAs("mainMenuButton")] [SerializeField] private Button _mainMenuButton;
        
        [FormerlySerializedAs("returnButton")] [SerializeField] private Button _returnButton;
        
        [SerializeField] private HeaderPanel _headerPanel;

        [SerializeField] private Sprite[] _icons;

        [SerializeField] private Image[] _buttonImages;

        [SerializeField] private TextMeshProUGUI[] _buttonTexts;

        [FormerlySerializedAs("informationAboutStation")] [SerializeField] private TextMeshProUGUI _informationAboutStation;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;

        private UnityAction _historicButtonAction;
        private UnityAction _motorialButtonAction;
        private UnityAction _sensoricButtonAction;
        private UnityAction _finish;
        private Button _currentButton;

        private void Awake()
        {
            _historicButtonAction += SetUIForHistoricInfo;
            _motorialButtonAction += SetUIForMotorialExercise;
            _sensoricButtonAction += SetUIForSensorialExercise;
            _finish += FinishExercise;
        }

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is StationViewInitializationParameters init)
            {
                _motorialButtonAction += init.MotorialEvent;
                _sensoricButtonAction += init.SensorialEvent;
                _historicButtonAction += init.HistoricInfoEvent;
                _finish += init.FinishExerciseEvent;
                
                _mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());

                _headerPanel.Initialize(init.HeaderText);
                _informationAboutStation.text = init.InformationAboutStation;

                SetDefaultUI();
            }
        }

        public void Display()
        {
        }

        public void Hide()
        {
        }

        private void SetUIForHistoricInfo()
        {
            SetDefaultUI();
            _leftButton.onClick.RemoveAllListeners();
            _buttonTexts[(int)ButtonTypes.Left].text = "Zatwierdź";
            _buttonTexts[(int)ButtonTypes.Left].margin = new Vector4(0, 0, 0, 30);
            _buttonImages[(int)ButtonTypes.Left].sprite = _icons[(int)ButtonTypes.Clicked];
            _leftButton.onClick.AddListener(() => _finish?.Invoke());
            _currentButton = _leftButton;
        }

        private void SetUIForMotorialExercise()
        {
            SetDefaultUI();
            _middleButton.onClick.RemoveAllListeners();
            _buttonTexts[(int)ButtonTypes.Middle].text = "Zatwierdź";
            _buttonTexts[(int)ButtonTypes.Middle].margin = new Vector4(0, 0, 0, 30);
            _buttonImages[(int)ButtonTypes.Middle].sprite = _icons[(int)ButtonTypes.Clicked];
            _middleButton.onClick.AddListener(() => _finish?.Invoke());
            _currentButton = _middleButton;
        }

        private void SetUIForSensorialExercise()
        {
            SetDefaultUI();
            _rightButton.onClick.RemoveAllListeners();
            _buttonTexts[(int)ButtonTypes.Right].text = "Zatwierdź";
            _buttonTexts[(int)ButtonTypes.Right].margin = new Vector4(0, 0, 0, 30);
            _buttonImages[(int)ButtonTypes.Right].sprite = _icons[(int)ButtonTypes.Clicked];
            _rightButton.onClick.AddListener(() => _finish?.Invoke());
            _currentButton = _rightButton;
        }
        
        private void FinishExercise()
        {
            _currentButton.interactable = false;
            SetDefaultUI();
        }
        
        private void SetDefaultUI()
        {
            _buttonImages[(int)ButtonTypes.Left].sprite = _icons[(int)ButtonTypes.Left];
            _buttonImages[(int)ButtonTypes.Middle].sprite = _icons[(int)ButtonTypes.Middle];
            _buttonImages[(int)ButtonTypes.Right].sprite = _icons[(int)ButtonTypes.Right];

            _buttonTexts[(int)ButtonTypes.Left].text = "Gra    miejska";
            _buttonTexts[(int)ButtonTypes.Left].margin = new Vector4(0, 0, 0, 0);
            _buttonTexts[(int)ButtonTypes.Middle].text = "Ruch";
            _buttonTexts[(int)ButtonTypes.Middle].margin = new Vector4(0, 0, 0, 30);
            _buttonTexts[(int)ButtonTypes.Right].text = "Oddech";
            _buttonTexts[(int)ButtonTypes.Right].margin = new Vector4(0, 0, 0, 30);

            _rightButton.onClick.RemoveAllListeners();
            _middleButton.onClick.RemoveAllListeners();
            _leftButton.onClick.RemoveAllListeners();

            _rightButton.onClick.AddListener(() => _sensoricButtonAction?.Invoke());
            _middleButton.onClick.AddListener(() => _motorialButtonAction?.Invoke());
            _leftButton.onClick.AddListener(() => _historicButtonAction?.Invoke());
        }
        
        public void OnDisable()
        {
            _rightButton.onClick.RemoveAllListeners();
            _middleButton.onClick.RemoveAllListeners();
            _leftButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
        }
    }
}