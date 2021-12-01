using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    public class LoginView : MonoBehaviour, IView
    {
        private RectTransform _view;
        
        [Tooltip("Button for login with Google. ")]
        public Button loginButton; 
        
        [Tooltip("Button for continuing without login. ")]
        public Button continueWithoutLoginButton;
        
        
        public void Start()
        {
            _view = GetComponent<RectTransform>();
            loginButton.onClick.AddListener(LoginWithGoogle);
            continueWithoutLoginButton.onClick.AddListener(ContinueWithoutLogin);
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

        public void ContinueWithoutLogin()
        {
            Debug.Log("Login without google");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
        public void LoginWithGoogle()
        {
            Debug.Log("Login with google");
            
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        
    }
}
