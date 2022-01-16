using System;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath
{
    public class PopupWithTextAndAudio : MonoBehaviour, IPopup, IInitializablePopup
    {
        public RectTransform PopupArea => _popupArea;
        
        [SerializeField] private TextMeshProUGUI _text;
        
        [SerializeField] private RectTransform _popupArea;
            
        [SerializeField] private Button _button;
        
        [SerializeField] private TextMeshProUGUI _buttonText;

        [SerializeField] private AudioSource _audioSource;

        private string _buttonOffStateText;

        private string _buttonOnStateText;

        private bool _isPlaying = false;
        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is PopupWithTextAndAudioInitializationParameters init)
            {
                InitSizeAndPosition(init.Payload);
                _text.text = init.HistoricalInformationText;
                _button.onClick.AddListener(OnButtonClicked);
                _buttonOffStateText = init.ButtonTextOffState;
                _buttonOnStateText = init.ButtonTextOnState;
                _buttonText.text = _buttonOffStateText;
                _audioSource.clip = init.Clip;
                _audioSource.loop = false;
                _audioSource.Stop();
            }
        }
        
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        public void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        void OnButtonClicked()
        {
            if (!_isPlaying)
            {
                _buttonText.text = _buttonOnStateText;
                _audioSource.Play();
                _isPlaying = true;
            }
            else
            {
                _buttonText.text = _buttonOffStateText;
                _audioSource.Stop();
                _isPlaying = false;
            }
        }
    }
}
