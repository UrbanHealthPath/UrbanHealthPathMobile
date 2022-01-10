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
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class StationView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {
        public RectTransform PopupArea => popupArea;

        [SerializeField] private Button rightButton;
        [SerializeField] private Button middleButton;
        [SerializeField] private Button leftButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button returnButton;
        [SerializeField] private Header header;

        [SerializeField] private Sprite historicInfoIcon;
        [SerializeField] private Sprite motorialIcon;
        [SerializeField] private Sprite sensorialIcon;
        [SerializeField] private Sprite finishIcon;

        [SerializeField] private Image leftButtonImage;
        [SerializeField] private Image middleButtonImage;
        [SerializeField] private Image rightButtonImage;

        [SerializeField] private TextMeshProUGUI leftButtonText;
        [SerializeField] private TextMeshProUGUI middleButtonText;
        [SerializeField] private TextMeshProUGUI rightButtonText;

        [SerializeField] private TextMeshProUGUI informationAboutStation;
        [SerializeField] private RectTransform popupArea;

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

        public void Initialize(Initializer initializer)
        {
            if (initializer is StationViewInitializer init)
            {
                _motorialButtonAction += init.MotorialEvent;
                _sensoricButtonAction += init.SensorialEvent;
                _historicButtonAction += init.HistoricInfoEvent;
                _finish += init.FinishExerciseEvent;
                
                mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());

                header.Initialize(init.HeaderText);
                informationAboutStation.text = init.InformationAboutStation;

                SetDefaultUI();
            }
        }

        public void Display()
        {
        }

        public void StopDisplay()
        {
        }

        private void SetUIForHistoricInfo()
        {
            SetDefaultUI();
            leftButton.onClick.RemoveAllListeners();
            leftButtonText.text = "Zatwierdź";
            leftButtonText.margin = new Vector4(0, 0, 0, 30);
            leftButtonImage.sprite = finishIcon;
            leftButton.onClick.AddListener(() => _finish?.Invoke());
            _currentButton = leftButton;
        }

        private void SetUIForMotorialExercise()
        {
            SetDefaultUI();
            middleButton.onClick.RemoveAllListeners();
            middleButtonText.text = "Zatwierdź";
            middleButtonText.margin = new Vector4(0, 0, 0, 30);
            middleButtonImage.sprite = finishIcon;
            middleButton.onClick.AddListener(() => _finish?.Invoke());
            _currentButton = middleButton;
        }

        private void SetUIForSensorialExercise()
        {
            SetDefaultUI();
            rightButton.onClick.RemoveAllListeners();
            rightButtonText.text = "Zatwierdź";
            rightButtonText.margin = new Vector4(0, 0, 0, 30);
            rightButtonImage.sprite = finishIcon;
            rightButton.onClick.AddListener(() => _finish?.Invoke());
            _currentButton = rightButton;
        }

        private void FinishExercise()
        {
            _currentButton.interactable = false;
            PopupManager.GetInstance().CloseCurrentPopup();
            SetDefaultUI();
        }
        private void SetDefaultUI()
        {
            leftButtonImage.sprite = historicInfoIcon;
            middleButtonImage.sprite = motorialIcon;
            rightButtonImage.sprite = sensorialIcon;

            leftButtonText.text = "Gra    miejska";
            leftButtonText.margin = new Vector4(0, 0, 0, 0);
            middleButtonText.text = "Ruch";
            middleButtonText.margin = new Vector4(0, 0, 0, 30);
            rightButtonText.text = "Oddech";
            rightButtonText.margin = new Vector4(0, 0, 0, 30);

            rightButton.onClick.RemoveAllListeners();
            middleButton.onClick.RemoveAllListeners();
            leftButton.onClick.RemoveAllListeners();

            rightButton.onClick.AddListener(() => _sensoricButtonAction?.Invoke());
            middleButton.onClick.AddListener(() => _motorialButtonAction?.Invoke());
            leftButton.onClick.AddListener(() => _historicButtonAction?.Invoke());
        }


        public void OnDisable()
        {
            rightButton.onClick.RemoveAllListeners();
            middleButton.onClick.RemoveAllListeners();
            leftButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();
            returnButton.onClick.RemoveAllListeners();
        }
    }
}