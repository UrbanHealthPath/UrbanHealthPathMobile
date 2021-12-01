using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    
    public class PathView : MonoBehaviour, IView
    {
        private RectTransform _view;
        
        public Button endPathButton, nextStationInfoButton, helpButton, imHereButton; 

        public TextMeshProUGUI headerText;
        public void Start()
        {
            _view = GetComponent<RectTransform>();
            endPathButton.onClick.AddListener(FinishPath);
            helpButton.onClick.AddListener(DisplayHelpMenu);
            nextStationInfoButton.onClick.AddListener(DisplayNextStationInfo);
            imHereButton.onClick.AddListener(ConfirnArrival);
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

        public void FinishPath()
        {
            Debug.Log("end patch");
            
        }
        
        public void DisplayHelpMenu()
        {
            Debug.Log("help");
            
        }
        
        public void DisplayNextStationInfo()
        {
            Debug.Log("next station info");
            
        }
        
        public void ConfirnArrival()
        {
            Debug.Log("confirm arrival");
            
        }

        private bool DisplayConfirmActionPopup()
        {
            return true;
        }

        public void OnDestroy()
        {
            endPathButton.onClick.RemoveListener(FinishPath);
            helpButton.onClick.RemoveListener(DisplayHelpMenu);
            nextStationInfoButton.onClick.RemoveListener(DisplayNextStationInfo);
            imHereButton.onClick.RemoveListener(ConfirnArrival);
        }
    }
}
