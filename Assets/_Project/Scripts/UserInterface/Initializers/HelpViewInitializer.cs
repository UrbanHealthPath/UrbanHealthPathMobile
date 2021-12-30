using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Initializers
{
    public class HelpViewInitializer : Initializer
    {
        public List<ListElement> Elements { get; }
        public UnityAction ReturnEvent { get; }

        public HelpViewInitializer(List<ListElement> elements, UnityAction returnEvent)
        {
            Elements = elements;
            ReturnEvent += returnEvent;
        }
    }
}