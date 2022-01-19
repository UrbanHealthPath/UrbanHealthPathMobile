using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    public class ButtonWithAudio : MonoBehaviour
    {
        public Button Button {get;}
        
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private TextMeshProUGUI _buttonText;

        public AudioSource AudioSource
        {
            get { return _audioSource; }
        }
        
        private bool _isPlaying = false;
        
        public void ChangeAudioState(AudioClip clip)
        {
            if (!_isPlaying)
            {
                PlayAudio(clip);
            }
            else
            {
                StopAudio();
            }
        }
        
        private void PlayAudio(AudioClip clip)
        {
            AudioSource.clip = clip;
                AudioSource.time = 0;
                AudioSource.Play();
                AudioSource.loop = false;
                _buttonText.text = "Wyłącz ciekawostkę";
                _isPlaying = true;
        }
        
        private void StopAudio()
        {
            AudioSource.Stop();
            _buttonText.text = "Włącz ciekawostkę";
            _isPlaying = false;
        }
    }
}
