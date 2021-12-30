using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class ProfileViewInitializer : Initializer
    {
        public UnityAction StatisticsEvent { get; }
        public UnityAction AchievementsEvent { get; }
        public UnityAction ShareEvent { get; }
        public UnityAction ReturnEvent { get; }
        public string Header { get; }

        public ProfileViewInitializer(UnityAction statisticsEvent, UnityAction achievementsEvent,
            UnityAction shareEvent, UnityAction returnEvent, string header = "Twój profil")
        {
            StatisticsEvent += statisticsEvent;
            AchievementsEvent += achievementsEvent;
            ShareEvent += shareEvent;
            ReturnEvent += returnEvent;
            Header = header;
        }
    }
}