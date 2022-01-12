using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class ProfileView : MonoBehaviour, IInitializableView, IDisplayable
    {
        [FormerlySerializedAs("returnButton")] [SerializeField] private Button _returnButton;
        [FormerlySerializedAs("statisticsButton")] [SerializeField] private Button _statisticsButton;
        [FormerlySerializedAs("achievementsButton")] [SerializeField] private Button _achievementsButton;
        [FormerlySerializedAs("shareButton")] [SerializeField] private Button _shareButton;
        [FormerlySerializedAs("_header")] [FormerlySerializedAs("header")] [SerializeField] private HeaderPanel headerPanel;

        private void Awake()
        {
            headerPanel.Initialize("Twój profil");
        }

        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is ProfileViewInitializationParameters init)
            {
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _statisticsButton.onClick.AddListener(() => init.StatisticsEvent?.Invoke());
                _achievementsButton.onClick.AddListener(() => init.AchievementsEvent?.Invoke());
                _shareButton.onClick.AddListener(() => init.ShareEvent?.Invoke());

                headerPanel.Initialize(init.Header);
            }
        }

        public void Display()
        {
        }

        public void Hide()
        {
        }
        
        public void OnDisable()
        {
            _returnButton.onClick.RemoveAllListeners();
            _statisticsButton.onClick.RemoveAllListeners();
            _achievementsButton.onClick.RemoveAllListeners();
            _shareButton.onClick.RemoveAllListeners();
        }
    }
}