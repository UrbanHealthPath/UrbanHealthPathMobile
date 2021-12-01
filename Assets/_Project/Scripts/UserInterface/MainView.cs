using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    public class MainView : MonoBehaviour, IView
    {
        private RectTransform _view;
        
        public Button profileButton, helpButton, settingsButton, startPathButton, exitButton; 

        public void Start()
        {
            _view = GetComponent<RectTransform>();
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

        public void DisplayProfile()
        {
            Debug.Log("Login without google");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void DisplaySettings()
        {
            Debug.Log("Login with google");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void DisplayHelpMenu()
        {
            Debug.Log("Login with google");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void StartPath()
        {
            Debug.Log("Login with google");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void QuitApplication()
        {
            Debug.Log("Login with google");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
    }
}
