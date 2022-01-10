using System;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class LoginInView : MonoBehaviour, IDisplayable, IInitializable
    {
        [SerializeField] private Button loginButton;
        [SerializeField] private Button continueWithoutLoginButton;
        
        public void Initialize(Initializer initializer)
        {
            if (initializer is LogInViewInitializer init)
            {
                loginButton.onClick.AddListener(() => init.LogInEvent?.Invoke());
                continueWithoutLoginButton.onClick.AddListener(() => init.ContinueWithoutLogInEvent?.Invoke());
            }
        }

        public void Display()
        {
        }

        public void StopDisplay()
        {
        }

        private void OnDisable()
        {
            loginButton.onClick.RemoveAllListeners();
            continueWithoutLoginButton.onClick.RemoveAllListeners();
        }
    }
}