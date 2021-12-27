using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    public class ListElement
    {
        public string ButtonText { get; private set; }
        public Sprite Icon { get; private set; }
        public string IconText { get; private set; }
        
        public UnityAction Action { get; private set; }

        public ListElement(string buttonText, Sprite icon, string iconText, UnityAction action)
        {
            ButtonText = buttonText ?? "";
            Icon = icon;
            IconText = iconText ?? "";
            Action += action;
        }
    }
}