using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class ProfileView : MonoBehaviour
    {
        [SerializeField] private Button  menuButton, returnButton, statisticsButton, achievementsButton, shareButton;
        
        public void Start()
        {
            returnButton.onClick.AddListener(Return);
            menuButton.onClick.AddListener(GoToMainMenu);
            statisticsButton.onClick.AddListener(ShowStatistics);
            achievementsButton.onClick.AddListener(ShowAchievements);
            shareButton.onClick.AddListener(ShareStatistics);
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
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        private void GoToMainMenu()
        {
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }

        private void ShowStatistics()
        {
        }

        private void ShowAchievements()
        {
        }

        private void ShareStatistics()
        {
        }

        public void OnDestroy()
        {
            returnButton.onClick.RemoveListener(Return);
            menuButton.onClick.RemoveListener(GoToMainMenu);
            statisticsButton.onClick.RemoveListener(ShowStatistics);
            achievementsButton.onClick.RemoveListener(ShowAchievements);
            shareButton.onClick.RemoveListener(ShareStatistics);
        }
    }
}
