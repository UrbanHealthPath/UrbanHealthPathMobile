using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class SettingsView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button revertButton, returnButton, fontButton, themeButton, audioButton;
        [SerializeField] private Header header;

        public void Start()
        {
            returnButton.onClick.AddListener(Return);
            revertButton.onClick.AddListener(RevertChanges);
            fontButton.onClick.AddListener(AdjustFontSize);
            themeButton.onClick.AddListener(AdjustTheme);
            audioButton.onClick.AddListener(AdjustAudioDescription);
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

        private void Return()
        {
            ViewManager.GetInstance().OpenView(ViewManager.GetInstance().LastViewType);
        }
        private void RevertChanges()
        {
        }

        private void AdjustFontSize()
        {
        }

        private void AdjustTheme()
        {
        }

        private void AdjustAudioDescription()
        {
        }

        public void OnDestroy()
        {
            returnButton.onClick.RemoveListener(Return);
            revertButton.onClick.RemoveListener(RevertChanges);
            fontButton.onClick.RemoveListener(AdjustFontSize);
            themeButton.onClick.RemoveListener(AdjustTheme);
            audioButton.onClick.RemoveListener(AdjustAudioDescription);
        }
    }
}