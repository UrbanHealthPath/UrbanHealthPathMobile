using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    public class ListPanelElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TextMeshProUGUI iconText;
        [SerializeField] private Button button;
        [SerializeField] private Image icon;

        public void SetValues(string buttonText, string iconText, UnityAction action, Sprite icon = null)
        {
            this.buttonText.text = buttonText ?? "";
            this.iconText.text = iconText ?? "";
            this.button.onClick.AddListener(() => action?.Invoke());

            if (icon)
            {
                this.icon.sprite = icon;
            }
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}