using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class SettingsView : MonoBehaviour, IDisplayable, IInitializable
    {
        [SerializeField] private Button revertButton;
        [SerializeField] private Button returnButton;
        [SerializeField] private Button fontButton;
        [SerializeField] private Button themeButton; 
        [SerializeField] private Button audioButton;
        [SerializeField] private Header header;

        public void Awake()
        {
            header.Initialize("Ustawienia");
        }

        public void Initialize(Initializer initializer)
        {
            if (initializer is SettingsInitializer init)
            {
                returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                revertButton.onClick.AddListener(() => init.RevertEvent?.Invoke());
                fontButton.onClick.AddListener(() => init.FontEvent?.Invoke());
                themeButton.onClick.AddListener(() => init.ThemeEvent?.Invoke());
                audioButton.onClick.AddListener(() => init.AudioEvent?.Invoke());

                header.Initialize(init.HeaderText);
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
            returnButton.onClick.RemoveAllListeners();
            revertButton.onClick.RemoveAllListeners();
            fontButton.onClick.RemoveAllListeners();
            themeButton.onClick.RemoveAllListeners();
            audioButton.onClick.RemoveAllListeners();
        }
    }
}