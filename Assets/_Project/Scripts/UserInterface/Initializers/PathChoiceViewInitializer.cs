using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class PathChoiceViewInitializer : Initializer
    {
        public List<ListElement> Elements { get; }
        public UnityAction MainMenuEvent { get; }

        public PathChoiceViewInitializer(List<ListElement> elements, UnityAction mainMenuEvent)
        {
            Elements = elements;
            MainMenuEvent += mainMenuEvent;
        }
    }
}