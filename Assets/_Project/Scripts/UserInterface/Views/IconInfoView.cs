using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class IconInfoView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button backButton, forwardButton;
        [SerializeField] private Header header;
        
        public void Awake()
        {
            header.Initialize("Wyjaśnienie ikon");
        }
        public void Initialize(Initializer initializer)
        {
            if (initializer is ApplicationInfoViewInitializer init)
            {
                backButton.onClick.AddListener(() => init.GoBack?.Invoke());
                forwardButton.onClick.AddListener(() => init.GoForward?.Invoke());
            }
        }
        public void Display()
        {
        }

        public void StopDisplay()
        {
        }
        
        public void OnDisable()
        {
            backButton.onClick.RemoveAllListeners();
            forwardButton.onClick.RemoveAllListeners();
        }
    }
}
