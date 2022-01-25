using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class ProfileViewInitializationParameters : IViewInitializationParameters
    {
        public List<ListElement> Elements { get; }
        public UnityAction StatisticsEvent { get; }
        public UnityAction AchievementsEvent { get; }
        public UnityAction ShareEvent { get; }
        public UnityAction ReturnEvent { get; }
        public string Header { get; }

        public ProfileViewInitializationParameters(List<ListElement> elements, UnityAction statisticsEvent, UnityAction achievementsEvent,
            UnityAction shareEvent, UnityAction returnEvent, string header = "Twój profil")
        {
            Elements = elements;
            StatisticsEvent += statisticsEvent;
            AchievementsEvent += achievementsEvent;
            ShareEvent += shareEvent;
            ReturnEvent += returnEvent;
            Header = header;
        }
    }
}