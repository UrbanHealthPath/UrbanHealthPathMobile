using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class PathView : MonoBehaviour, IInitializable, IPopupable, IDisplayable
    {
        [SerializeField] private Button endPathButton, nextStationInfoButton, helpButton, mainMenuButton;
        [SerializeField] private Header header;
        [SerializeField] private RectTransform popupArea;
        public RectTransform PopupArea => popupArea;
        
        public void Initialize(Initializer initializer)
        {
            if (initializer is PathViewInitializer init)
            {
                header.Initialize(init.HeaderText);
                endPathButton.onClick.AddListener(() => init.EndPathEvent?.Invoke());
                helpButton.onClick.AddListener(()=>init.HelpEvent?.Invoke());
                nextStationInfoButton.onClick.AddListener(()=>init.NextStationInfoEvent.Invoke());
                mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
            }
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

        // private void DisplayConfirmationPopup()
        // {
        //     RectTransform rectTransform =
        //         ViewManager.GetInstance().CurrentView.GetComponent<IPopupable>().PopupArea;
        //
        //     PopupPayload payload = new PopupPayload(rectTransform.transform.position, rectTransform.sizeDelta);
        //     GameObject popup = PopupManager.GetInstance()
        //         .OpenPopup(PopupType.Confirmation, CreateInitializerForConfirmationPopup(), payload);
        // }

        // private ConfirmationPopupInitializer CreateInitializerForConfirmationPopup()
        // {
        //   //  return new ConfirmationPopupInitializer("Czy na pewno chcesz zakończyć ścieżkę?", 2,
        //        // new string[] {"Tak", "Nie"}, new UnityAction[] {FinishPath, ContinuePath});
        // }
        
        // private void DisplayNextStationInfo()
        // {
        //     if (PopupManager.GetInstance().CurrentPopupType == PopupType.WithTextImageAndButton)
        //     {
        //         PopupManager.GetInstance().ClosePopup();
        //     }
        //     else
        //     {
        //         RectTransform rectTransform =
        //             ViewManager.GetInstance().CurrentView.GetComponent<IPopupable>().PopupArea;
        //         PopupPayload payload = new PopupPayload(rectTransform.transform.position, rectTransform.sizeDelta);
        //         PopupManager.GetInstance().OpenPopup(PopupType.WithTextImageAndButton, null, payload);
        //     }
        // }
        
        public void OnDisable()
        {
            endPathButton.onClick.RemoveAllListeners();
            helpButton.onClick.RemoveAllListeners();
            nextStationInfoButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.RemoveAllListeners();
        }


    }
}