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
        [SerializeField] private List<GameObject> pages = new List<GameObject>();
        [SerializeField] private GameObject parentForPages;
        private int _pageIndex;


        public void Initialize(List<ListElement> elements)
        {
            int pagesCount = elements.Count / 3 + 1;
            int pagesInstantinated = pages.Count;
            int j = 0;

            Vector3 position = Vector3.zero;
            
            for (int i = 0; i < pagesCount; i++)
            {
                GameObject newPage;

                if (i + 1 > pagesInstantinated)
                {
                    newPage = Instantiate(page);
                    pages.Add(newPage);
                    
                    var rectTrans = newPage.GetComponent<RectTransform>();
                    rectTrans.SetParent(parentForPages.transform);
                    rectTrans.anchoredPosition =position;
                    rectTrans.localScale = new Vector3(1, 1, 1);
                    newPage.SetActive(false);
                }
                else
                {
                    newPage = pages[i];
                    var rectTrans = newPage.GetComponent<RectTransform>();
                    position = rectTrans.anchoredPosition;

                }

               

                List<ListElement> list = new List<ListElement>();

                for (int k = 0; k < 3; k++)
                {
                    if (j < elements.Count)
                    {
                        list.Add(elements[j]);
                        j++;
                    }
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

            bool isNextPage = (pages.Count > 1 ? true : false);

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
            pages[Mathf.Clamp(_pageIndex, 0, pages.Count - 1)].SetActive(true);

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
            pages[Mathf.Clamp(_pageIndex, 0, pages.Count - 1)].SetActive(true);

            if (_pageIndex == pages.Count - 1)
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