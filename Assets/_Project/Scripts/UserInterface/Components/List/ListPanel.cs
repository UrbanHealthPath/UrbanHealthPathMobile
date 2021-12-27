using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    public class ListPanel : MonoBehaviour
    {
        [SerializeField] private GameObject page;
        [SerializeField] private Button backButton, forwardButton;
        [SerializeField] private GameObject frameBackButton, frameForwardButton, buttonsPanel;
        private int _pageIndex = 0;
        private List<GameObject> _pages = new List<GameObject>();

        public void Initialize(List<ListElement> elements)
        {
            int pages = elements.Count / 3;
            int j = 0;

            for (int i = 0; i < pages; i++)
            {
                var newPage = Instantiate(page);
                _pages.Add(newPage);

                List<ListElement> list = new List<ListElement>();

                for (int k = 0; k < 3; k++)
                {
                    if (j < elements.Count)
                        list.Add(elements[j]);
                    j++;
                }

                newPage.GetComponent<ListPage>().Initialize(list);
                
                list.Clear();
            }

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            backButton.onClick.AddListener(GoBack);
            forwardButton.onClick.AddListener(GoForward);

            backButton.gameObject.SetActive(false);
            frameBackButton.SetActive(false);

            bool isNextPage = (_pages.Count > 1 ? true : false);

            forwardButton.gameObject.SetActive(isNextPage);
            frameForwardButton.SetActive(isNextPage);
            buttonsPanel.SetActive(isNextPage);
        }

        private void GoBack()
        {
            forwardButton.gameObject.SetActive(true);
            frameForwardButton.SetActive(true);

            _pages[_pageIndex].SetActive(false);
            _pageIndex--;
            _pages[Mathf.Clamp(_pageIndex, 0, _pages.Count - 1)].SetActive(true);

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

            _pages[_pageIndex].SetActive(false);
            _pageIndex++;
            _pages[Mathf.Clamp(_pageIndex, 0, _pages.Count - 1)].SetActive(true);

            if (_pageIndex == _pages.Count - 1)
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