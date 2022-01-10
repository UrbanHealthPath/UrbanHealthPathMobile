using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    public class ListElement
    {
        public string ButtonText { get; }
        public Sprite Icon { get; }
        public string IconText { get; }
        public UnityAction Action { get; }

        public ListElement(string buttonText, Sprite icon, string iconText, UnityAction action)
        {
            ButtonText = buttonText ?? "";
            Icon = icon;
            IconText = iconText ?? "";
            Action += action;
        }
    }
}