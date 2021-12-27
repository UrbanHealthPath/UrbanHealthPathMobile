using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class LoginInView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button loginButton; 
        
        [SerializeField] private Button continueWithoutLoginButton;
        
       // [SerializeField] private Header header;
        public void Start()
        {
            loginButton.onClick.AddListener(LoginWithGoogle);
            continueWithoutLoginButton.onClick.AddListener(ContinueWithoutLogin);
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
        
        private void ContinueWithoutLogin()
        {
            Debug.Log("Login without google");
            
            ViewManager.GetInstance().OpenView(ViewType.AppInfo);
        }

        private void LoginWithGoogle()
        {
            Debug.Log("Login with google");
            
            ViewManager.GetInstance().OpenView(ViewType.AppInfo);
        }
    }
}
