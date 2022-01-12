using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class QuizWithTextsPopup : MonoBehaviour, IPopup, IInitializablePopup
    {
        public RectTransform PopupArea => _popupArea;

        [FormerlySerializedAs("question")] [SerializeField] private TextMeshProUGUI _question;
        [FormerlySerializedAs("popupArea")] [SerializeField] private RectTransform _popupArea;
        [FormerlySerializedAs("text1")] [SerializeField] private TextMeshProUGUI _text1;
        [FormerlySerializedAs("text2")] [SerializeField] private TextMeshProUGUI _text2;
        [FormerlySerializedAs("text3")] [SerializeField] private TextMeshProUGUI _text3;
        [FormerlySerializedAs("text4")] [SerializeField] private TextMeshProUGUI _text4;
        [FormerlySerializedAs("button1")] [SerializeField] private Button _button1;
        [FormerlySerializedAs("button2")] [SerializeField] private Button _button2;
        [FormerlySerializedAs("button3")] [SerializeField] private Button _button3;
        [FormerlySerializedAs("button4")] [SerializeField] private Button _button4;

        public void Initialize(IPopupInitializationParameters initializationParameters)
        {
            if (initializationParameters is QuizWithTextPopupInitializationParameters init)
            {
                InitSizeAndPosition(init.Payload);
                
                _question.text = init.Question;
                _text1.text = init.Text1;
                _text2.text = init.Text2;
                _text3.text = init.Text3;
                _text4.text = init.Text4;

                _button1.onClick.AddListener(() => init.ButtonText1Action?.Invoke());
                _button2.onClick.AddListener(() => init.ButtonText2Action?.Invoke());
                _button3.onClick.AddListener(() => init.ButtonText3Action?.Invoke());
                _button4.onClick.AddListener(() => init.ButtonText4Action?.Invoke());
            }
        }
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        private void OnDisable()
        {
            _button1.onClick.RemoveAllListeners();
            _button2.onClick.RemoveAllListeners();
            _button3.onClick.RemoveAllListeners();
            _button4.onClick.RemoveAllListeners();
        }
    }
}