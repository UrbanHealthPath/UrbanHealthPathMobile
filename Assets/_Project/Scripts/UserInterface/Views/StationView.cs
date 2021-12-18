using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class StationView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {

        private RectTransform _view;

        [SerializeField] private Button sensorialButton, motorialButton, historicInfoButton, mainMenuButton, returnButton;

        [SerializeField] private TextMeshProUGUI headerText;
        
        [SerializeField]
        private RectTransform _popupArea;
        public RectTransform PopupArea
        {
            get { return _popupArea;  }
        }
        
        public void Awake()
        {
            _view = GetComponent<RectTransform>();
            motorialButton.onClick.AddListener(DisplayMototialExercise);
            sensorialButton.onClick.AddListener(DisplaySensorialExercise);
            historicInfoButton.onClick.AddListener(DisplayHistoricInfo);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            returnButton.onClick.AddListener(Return);
        }
        
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void StopDisplay()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            this.enabled = false;
            this.gameObject.SetActive(false);
        }

        private void DisplayMototialExercise()
        {
            Debug.Log("Motorial");
            
          //  ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        private void DisplaySensorialExercise()
        {
            Debug.Log("Sensorial");
            
           // ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        private void DisplayHistoricInfo()
        {
            Debug.Log("Historic info");
            
          //  ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        private void Return()
        {
            Debug.Log("return");

            ViewManager.GetInstance().OpenView(ViewType.Path);
        }

        private void GoToMainMenu()
        {
            Debug.Log("main menu");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        private void ChangeHeaderText(String newText)
        {
            headerText.text = newText;
        }

        public void OnDestroy()
        {
            sensorialButton.onClick.RemoveListener(DisplaySensorialExercise);
            motorialButton.onClick.RemoveListener(DisplayMototialExercise);
            historicInfoButton.onClick.RemoveListener(DisplayHistoricInfo);
            mainMenuButton.onClick.RemoveListener(GoToMainMenu);
            returnButton.onClick.RemoveListener(Return);
        }
    }
}
