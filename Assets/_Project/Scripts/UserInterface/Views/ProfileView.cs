using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class ProfileView : MonoBehaviour, IInitializable, IDisplayable
    {
        [SerializeField] private Button returnButton, statisticsButton, achievementsButton, shareButton;
        [SerializeField] private Header header;

        public void Awake()
        {
            header.Initialize("Twój profil");
        }

        public void Initialize(Initializer initializer)
        {
            if (initializer is ProfileViewInitializer init)
            {
                returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                statisticsButton.onClick.AddListener(() => init.StatisticsEvent?.Invoke());
                achievementsButton.onClick.AddListener(() => init.AchievementsEvent?.Invoke());
                shareButton.onClick.AddListener(() => init.ShareEvent?.Invoke());

                header.Initialize(init.Header);
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
            statisticsButton.onClick.RemoveAllListeners();
            achievementsButton.onClick.RemoveAllListeners();
            shareButton.onClick.RemoveAllListeners();
        }
    }
}