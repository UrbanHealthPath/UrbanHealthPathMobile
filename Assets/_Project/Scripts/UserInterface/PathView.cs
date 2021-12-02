using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using TMPro;
using UnityEditor.Il2Cpp;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    
    public class PathView : MonoBehaviour, IView, IPopupable
    {
        private RectTransform _view;
        
        [SerializeField] private Button endPathButton, nextStationInfoButton, helpButton, imHereButton; 

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

        private void FinishPath()
        {
            Debug.Log("end patch");
            
        }

        private void DisplayHelpMenu()
        {
            Debug.Log("help");
            
        }

        private void DisplayNextStationInfo()
        {
            Debug.Log("next station info");
            RectTransform rectTransform = ViewManager.GetInstance().CurrentView.GetComponent<IPopupable>().PopupArea;
            PopupPayload payload = new PopupPayload(rectTransform.anchoredPosition, rectTransform.sizeDelta);
            PopupManager.GetInstance().OpenPopup(PopupType.WithContent, payload);
        }

        private void ConfirnArrival()
        {
            Debug.Log("confirm arrival");

            ViewManager.GetInstance().OpenView(ViewType.Station);

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
