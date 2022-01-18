using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class PathPresentationView : MonoBehaviour, IInitializableView
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _chooseButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private HeaderPanel _headerPanel;
        [SerializeField] private TextMeshProUGUI _stationCount;
        [SerializeField] private TextMeshProUGUI _distance;
        [SerializeField] private RawImage _mapImage;
        
        public void Initialize(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters is PathPresentationViewInitializationParameters init)
            {
                _mainMenuButton.onClick.AddListener(() => init.MainMenuEvent?.Invoke());
                _returnButton.onClick.AddListener(() => init.ReturnEvent?.Invoke());
                _chooseButton.onClick.AddListener(() => init.ChooseButtonAction?.Invoke());
                _cancelButton.onClick.AddListener(() => init.CancelButtonAction?.Invoke());
                _headerPanel.Initialize(init.HeaderText);
                _stationCount.text = "Liczba punktów do odwiedzenia: " + init.StationCount;
                _distance.text = "Dystans możliwy do przebycia: " + init.PathLength;
                _mapImage.texture = init.MapTexture;
            }
        }
        
        public void OnDisable()
        {
            _chooseButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();
            _mainMenuButton.onClick.RemoveAllListeners();
            _returnButton.onClick.RemoveAllListeners();
        }
    }
}
