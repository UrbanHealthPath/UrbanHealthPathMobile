using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Popups
{
    public class QuizWithTextsPopup : MonoBehaviour, IPopup, IInitializable
    {
        public RectTransform PopupArea => popupArea;

        [SerializeField] private TextMeshProUGUI question;
        [SerializeField] private RectTransform popupArea;
        [SerializeField] private TextMeshProUGUI text1;
        [SerializeField] private TextMeshProUGUI text2;
        [SerializeField] private TextMeshProUGUI text3;
        [SerializeField] private TextMeshProUGUI text4;
        [SerializeField] private Button button1;
        [SerializeField] private Button button2;
        [SerializeField] private Button button3;
        [SerializeField] private Button button4;

        public void Initialize(Initializer initializer)
        {
            if (initializer is QuizWithTextPopupInitializer init)
            {
                InitSizeAndPosition(init.Payload);
                
                question.text = init.Question;
                text1.text = init.Text1;
                text2.text = init.Text2;
                text3.text = init.Text3;
                text4.text = init.Text4;

                button1.onClick.AddListener(() => init.ButtonText1Action?.Invoke());
                button2.onClick.AddListener(() => init.ButtonText2Action?.Invoke());
                button3.onClick.AddListener(() => init.ButtonText3Action?.Invoke());
                button4.onClick.AddListener(() => init.ButtonText4Action?.Invoke());
            }
        }
        public void InitSizeAndPosition(PopupPayload payload)
        {
            PopupArea.sizeDelta = new Vector2(payload.Size.x, payload.Size.y);
            PopupArea.transform.position = new Vector2(payload.Position.x, payload.Position.y);
        }

        private void OnDisable()
        {
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
            button4.onClick.RemoveAllListeners();
        }
    }
}