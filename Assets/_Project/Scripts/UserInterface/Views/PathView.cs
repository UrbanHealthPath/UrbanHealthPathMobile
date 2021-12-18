using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    
    public class PathView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {
        [SerializeField] private Button endPathButton, nextStationInfoButton, helpButton, mainMenuButton; 
        
        [SerializeField] private RectTransform popupArea;
        public RectTransform PopupArea => popupArea;

        public void Start()
        {
            endPathButton.onClick.AddListener(DisplayConfirmationPopup);
            helpButton.onClick.AddListener(DisplayHelpMenu);
            nextStationInfoButton.onClick.AddListener(DisplayNextStationInfo);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }

        public void Initialize()
        {
        }
        public void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void StopDisplay()
        {
           this.gameObject.SetActive(false);
        }

        private void DisplayConfirmationPopup()
        {
            RectTransform rectTransform =
                ViewManager.GetInstance().CurrentView.GetComponent<IPopupable>().PopupArea;
            
            PopupPayload payload = new PopupPayload(rectTransform.transform.position, rectTransform.sizeDelta);
            GameObject popup = PopupManager.GetInstance().OpenPopup(PopupType.Confirmation, payload);
            popup.GetComponent<ConfirmationPopup>().Initialize(FinishPath, ContinuePath, "Czy na pewno chcesz zakończyć ścieżkę?");
        }
        
        private void FinishPath()
        {
            PopupManager.GetInstance().ClosePopup();
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        private void ContinuePath()
        {
            PopupManager.GetInstance().ClosePopup();
        }

        private void DisplayHelpMenu()
        {
            ViewManager.GetInstance().OpenView(ViewType.Help);
        }

        private void DisplayNextStationInfo()
        {
            if (PopupManager.GetInstance().CurrentPopupType == PopupType.WithTextImageAndButton)
            {
                PopupManager.GetInstance().ClosePopup();
            }
            else
            {
                RectTransform rectTransform =
                    ViewManager.GetInstance().CurrentView.GetComponent<IPopupable>().PopupArea;
                PopupPayload payload = new PopupPayload(rectTransform.transform.position, rectTransform.sizeDelta);
                PopupManager.GetInstance().OpenPopup(PopupType.WithTextImageAndButton, payload);
            }
        }

        private void GoToMainMenu()
        {
            Debug.Log("main menu");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        public void OnDestroy()
        {
            endPathButton.onClick.RemoveListener(DisplayConfirmationPopup);
            helpButton.onClick.RemoveListener(DisplayHelpMenu);
            nextStationInfoButton.onClick.RemoveListener(DisplayNextStationInfo);
            mainMenuButton.onClick.RemoveListener(GoToMainMenu);
        }
    }
}
