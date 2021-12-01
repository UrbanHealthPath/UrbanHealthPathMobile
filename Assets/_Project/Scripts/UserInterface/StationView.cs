using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    public class StationView : MonoBehaviour, IView
    {

        private RectTransform _view;

        public Button sensorialButton, motorialButton, historicInfoButton, mainMenuButton, returnButton;

        public TextMeshProUGUI headerText;
        
        public void Start()
        {
            _view = GetComponent<RectTransform>();
            motorialButton.onClick.AddListener(DisplayMototialExercise);
            sensorialButton.onClick.AddListener(DisplaySensorialExercise);
            historicInfoButton.onClick.AddListener(DisplayHistoricInfo);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            returnButton.onClick.AddListener(Return);
        }
        // public void Initialize()
        // {
        //     
        // }
        public void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void Close()
        {
            this.enabled = false;
            this.gameObject.SetActive(false);
        }

        public void DisplayMototialExercise()
        {
            Debug.Log("Motorial");
            
          //  ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void DisplaySensorialExercise()
        {
            Debug.Log("Sensorial");
            
           // ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void DisplayHistoricInfo()
        {
            Debug.Log("Historic info");
            
          //  ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void Return()
        {
            Debug.Log("return");
            ChangeHeaderText("sheeesh");
            
           // ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void GoToMainMenu()
        {
            Debug.Log("main menu");
            
           // ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        public void ChangeHeaderText(String newText)
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
