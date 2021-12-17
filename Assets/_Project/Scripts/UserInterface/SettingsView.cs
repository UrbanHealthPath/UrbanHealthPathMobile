using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class SettingsView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button revertButton, menuButton, returnButton, fontButton, themeButton, audioButton;


        public void Start()
        {
            returnButton.onClick.AddListener(Return);
            menuButton.onClick.AddListener(GoToMainMenu);
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

        private void GoToMainMenu()
        {
            ViewManager.GetInstance().OpenView(ViewType.Main);
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
            menuButton.onClick.RemoveListener(GoToMainMenu);
            revertButton.onClick.RemoveListener(RevertChanges);
            fontButton.onClick.RemoveListener(AdjustFontSize);
            themeButton.onClick.RemoveListener(AdjustTheme);
            audioButton.onClick.RemoveListener(AdjustAudioDescription);
        }
    }
}