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
    
    public class PathView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {
        private RectTransform _view;
        
        [SerializeField] private Button endPathButton, nextStationInfoButton, helpButton, returnButton, mainMenuButton; 

        [SerializeField] private TextMeshProUGUI headerText;
        
        [SerializeField]
        private RectTransform _popupArea;
        public RectTransform PopupArea
        {
            get { return _popupArea;  }
        }
        public void Start()
        {
            _view = GetComponent<RectTransform>();
            endPathButton.onClick.AddListener(FinishPath);
            helpButton.onClick.AddListener(DisplayHelpMenu);
            nextStationInfoButton.onClick.AddListener(DisplayNextStationInfo);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            returnButton.onClick.AddListener(Return);
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
            
        }
        private void FinishPath()
        {
            ViewManager.GetInstance().OpenView(ViewType.Main);
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

        private void Return()
        {
            Debug.Log("return");
 
            
          //  ViewManager.GetInstance().OpenView(ViewType.Path);
        }

        private void GoToMainMenu()
        {
            Debug.Log("main menu");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        public void OnDestroy()
        {
            endPathButton.onClick.RemoveListener(FinishPath);
            helpButton.onClick.RemoveListener(DisplayHelpMenu);
            nextStationInfoButton.onClick.RemoveListener(DisplayNextStationInfo);
            mainMenuButton.onClick.RemoveListener(GoToMainMenu);
            returnButton.onClick.RemoveListener(Return);
        }
    }
}
