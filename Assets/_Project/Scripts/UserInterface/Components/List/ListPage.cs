using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Components.List
{
    public class ListPage : MonoBehaviour
    {
        [SerializeField] private List<ListPanelElement> panelElements = new List<ListPanelElement>();

        private const int MaxElementsCount = 3;

        private void Awake()
        {
            foreach (ListPanelElement panelElem in panelElements)
            {
                if (panelElem.gameObject.activeSelf)
                    panelElem.gameObject.SetActive(false);
            }
        }

        public void Initialize(List<ListElement> elements)
        {
            for (int i = 0; i < MaxElementsCount; i++)
            {
                if (i < elements.Count)
                {
                    ListElement element = elements[i];
                    ListPanelElement panelElem = panelElements[i];

                    panelElem.gameObject.SetActive(true);
                    panelElem.SetValues(element.ButtonText, element.IconText, element.Action, element.Icon);
                }
            }
        }
    }
}