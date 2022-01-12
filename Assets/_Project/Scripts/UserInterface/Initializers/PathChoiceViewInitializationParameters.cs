using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class PathChoiceViewInitializationParameters : IViewInitializationParameters
    {
        public List<ListElement> Elements { get; }
        public UnityAction MainMenuEvent { get; }

        public PathChoiceViewInitializationParameters(List<ListElement> elements, UnityAction mainMenuEvent)
        {
            Elements = elements;
            MainMenuEvent += mainMenuEvent;
        }
    }
}