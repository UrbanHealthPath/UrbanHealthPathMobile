using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface
{
    [RequireComponent(typeof(Button))]
    public class AppearanceChangingButton : MonoBehaviour
    {
        [Serializable] public struct ButtonAppearance
        {
            [SerializeField] private Sprite sprite;
            [SerializeField] private String text;
            [SerializeField] private int bottomOffset;
            
            public Sprite  GetSprite()
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
        
        [SerializeField] private List<ButtonAppearance> _buttonAppearances;
        
        [SerializeField] private Image _image;
        
        [SerializeField] private TextMeshProUGUI _text;
        
        [SerializeField] private Button button;
        
        private int _lastAppearanceIndex = 0;

        private void Awake()
        {
            button.onClick.AddListener(ChangeImageAndText);
        }
        
        
        private void ChangeImageAndText()
        {
            if (_lastAppearanceIndex > _buttonAppearances.Count - 1)
            {
                _lastAppearanceIndex = 0;
            }
            
            this._image.sprite = _buttonAppearances[_lastAppearanceIndex].GetSprite();
            this._text.text = _buttonAppearances[_lastAppearanceIndex].GetText();
            this._text.margin = new Vector4(0,0,0,  _buttonAppearances[_lastAppearanceIndex].GetOffset());
            _lastAppearanceIndex++;
        }
        
        private void OnDestroy()
        {
            button.onClick.RemoveListener(ChangeImageAndText);
        }
    }
}
