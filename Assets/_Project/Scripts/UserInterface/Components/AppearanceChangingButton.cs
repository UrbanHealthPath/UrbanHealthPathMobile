using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    [RequireComponent(typeof(Button))]
    public class AppearanceChangingButton : MonoBehaviour
    {
        [Serializable] public struct ButtonAppearance
        {
            [SerializeField] private Sprite sprite;
            [SerializeField] private String text;
            [SerializeField] private int bottomOffset;

            public Sprite GetSprite()
            {
                return sprite;
            }

            public String GetText()
            {
                return text;
            }
            public int GetOffset()
            {
                return bottomOffset;
            }
        }

        [Tooltip("List with button appearances, that changes every time user click on the button. " +
                 "First list element is a default button appearance. ")]
        [FormerlySerializedAs("_buttonAppearances")] 
        [SerializeField] private List<ButtonAppearance> buttonAppearances;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;

        private int _lastAppearanceIndex = 1;

        private void Awake()
        {
            button.onClick.AddListener(ChangeImageAndText);
        }
        
        public void SetDefaultAppearance()
        {
            if (buttonAppearances != null)
            {
                image.sprite = buttonAppearances[0].GetSprite();
                text.text = buttonAppearances[0].GetText();
                text.margin = new Vector4(0, 0, 0, buttonAppearances[0].GetOffset());
                _lastAppearanceIndex++;
            }
        }

        private void ChangeImageAndText()
        {
            if (_lastAppearanceIndex > buttonAppearances.Count - 1)
            {
                _lastAppearanceIndex = 0;
            }

            image.sprite = buttonAppearances[_lastAppearanceIndex].GetSprite();
            text.text = buttonAppearances[_lastAppearanceIndex].GetText();
            text.margin = new Vector4(0, 0, 0, buttonAppearances[_lastAppearanceIndex].GetOffset());
            _lastAppearanceIndex++;
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(ChangeImageAndText);
        }
    }
}