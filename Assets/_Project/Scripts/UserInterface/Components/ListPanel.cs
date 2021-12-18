using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components
{
    public class ListPanel : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pages;
        [SerializeField] private Button backButton, forwardButton;
        [SerializeField] private GameObject frameBackButton, frameForwardButton, buttonsPanel;
        private int _pageIndex = 0;
        
        public void Awake()
        {
            backButton.onClick.AddListener(GoBack);
            forwardButton.onClick.AddListener(GoForward);
            
            backButton.gameObject.SetActive(false);
            frameBackButton.SetActive(false);
            
            bool isNextPage = (pages.Count > 1 ? true :  false);

            forwardButton.gameObject.SetActive(isNextPage);
            frameForwardButton.SetActive(isNextPage);
            buttonsPanel.SetActive(isNextPage);
        }
        
        private void GoBack()
        {
            forwardButton.gameObject.SetActive(true);
            frameForwardButton.SetActive(true);
            
            pages[_pageIndex].SetActive(false);
            _pageIndex--;
            pages[Mathf.Clamp(_pageIndex, 0, pages.Count-1)].SetActive(true);

            if (_pageIndex == 0)
            {
                backButton.gameObject.SetActive(false);
                frameBackButton.SetActive(false);
            }
        }

        private void GoForward()
        {
            backButton.gameObject.SetActive(true);
            frameBackButton.SetActive(true);
            
            pages[_pageIndex].SetActive(false);
            _pageIndex++;
            pages[Mathf.Clamp(_pageIndex, 0, pages.Count-1)].SetActive(true);

            if (_pageIndex == pages.Count-1)
            {
                forwardButton.gameObject.SetActive(false);
                frameForwardButton.SetActive(false);
            }
        }
        public void OnDestroy()
        {
            backButton.onClick.RemoveListener(GoBack);
            forwardButton.onClick.RemoveListener(GoForward);
        }
    }
}
