using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class ProfileViewInitializationParameters : IViewInitializationParameters
    {
        public UnityAction ReturnEvent { get; }
        public string Header { get; }
        public List<ListElement> ListElements { get; }

        public ProfileViewInitializationParameters(List<ListElement> listElements,
            UnityAction returnEvent, string header = "Twój profil")
        {
            ListElements = listElements;
            ReturnEvent += returnEvent;
            Header = header;
        }
    }
}