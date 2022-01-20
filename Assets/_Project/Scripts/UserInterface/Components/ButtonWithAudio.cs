using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    public class ButtonWithAudio : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TextMeshProUGUI _buttonText;

        public AudioClip Clip { get; private set; }
        public Button Button => _button;
        public bool IsPlaying { get; private set; }
        public AudioSource AudioSource
        {
            get { return _audioSource; }
        }

        public void Initialize(AudioClip clip, UnityAction<ButtonWithAudio> initialized = null)
        {
            Clip = clip;
            initialized?.Invoke(this);
        }

        public void ToggleState()
        {
            if (IsPlaying)
            {
                StopAudio();
            }
            else
            {
                PlayAudio();
            }
        }

        public void ForceStop()
        {
            StopAudio();
        }

        private void PlayAudio()
        {
            AudioSource.clip = Clip;
            AudioSource.time = 0;
            AudioSource.Play();
            AudioSource.loop = false;
            _buttonText.text = "Wyłącz ciekawostkę";
            IsPlaying = true;
        }
        
        private void StopAudio()
        {
            AudioSource.Stop();
            _buttonText.text = "Włącz ciekawostkę";
            IsPlaying = false;
        }
    }
}
