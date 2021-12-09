using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath
{
    public class HelpView : MonoBehaviour, IDisplayable
    {
        [SerializeField] private Button backButton, forwardButton, menuButton, returnButton;

        [SerializeField] private List<GameObject> pages;

        private int _pageIndex = 0;

        private Image _fadeButtonBack, _fadeButtonForward;
        
        
        public void Start()
        {
            backButton.onClick.AddListener(GoBack);
            forwardButton.onClick.AddListener(GoForward);
            returnButton.onClick.AddListener(Return);
            menuButton.onClick.AddListener(GoToMainMenu);

            _fadeButtonBack = backButton.transform.GetChild(backButton.transform.childCount - 1).GetComponent<Image>();
            _fadeButtonForward = forwardButton.transform.GetChild(forwardButton.transform.childCount - 1).GetComponent<Image>();
            
            backButton.enabled = false;
            _fadeButtonBack.enabled = true;
            
            forwardButton.enabled = (pages.Count > 1 ? true :  false);
            _fadeButtonForward.enabled = !forwardButton.enabled;
        }
        public void Display()
        {
            this.enabled = true;
            this.gameObject.SetActive(true);
        }

        public void StopDisplay()
        {
            this.gameObject.SetActive(false);
        }

        private void GoBack()
        {
            forwardButton.enabled = true;
            _fadeButtonForward.enabled = false;
            
            pages[_pageIndex].SetActive(false);
            _pageIndex--;
            pages[Mathf.Clamp(_pageIndex, 0, pages.Count-1)].SetActive(true);

            if (_pageIndex == 0)
            {
                backButton.enabled = false;
                _fadeButtonBack.enabled = true;
            }
        }

        private void GoForward()
        {
            backButton.enabled = true;
            _fadeButtonBack.enabled = false;
            
            pages[_pageIndex].SetActive(false);
            _pageIndex++;
            pages[Mathf.Clamp(_pageIndex, 0, pages.Count-1)].SetActive(true);

            if (_pageIndex == pages.Count-1)
            {
                forwardButton.enabled = false;
                _fadeButtonForward.enabled = true;
            }
        }
        
        private void Return()
        {
            ViewManager.GetInstance().OpenView(ViewManager.GetInstance().LastViewType);
        }

        private void GoToMainMenu()
        {
            ViewManager.GetInstance().OpenView(ViewType.Main);
        }
        public void OnDestroy()
        {
            backButton.onClick.RemoveListener(GoBack);
            forwardButton.onClick.RemoveListener(GoForward);
            returnButton.onClick.RemoveListener(Return);
            menuButton.onClick.RemoveListener(GoToMainMenu);
        }
    }
}
