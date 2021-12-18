using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class ViewManager : MonoBehaviour
    {
        [Serializable]
        public struct View
        {
            [SerializeField] private ViewType type;
            [SerializeField] private GameObject viewObject;

            public ViewType GetViewType()
            {
                return type;
            }

            public GameObject GetViewObject()
            {
                return viewObject;
            }
        }

        [SerializeField] private View[] viewsWithTypes;

        private Dictionary<ViewType, GameObject> _views;

        private static ViewManager _instance = null;

        public ViewType CurrentViewType{ get; private set; }
        public GameObject CurrentView { get; private set; }

        public ViewType LastViewType { get; private set; }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            _instance.CurrentView = null;

            _instance._views = new Dictionary<ViewType, GameObject>();

            foreach (var view in viewsWithTypes)
            {
                _views.Add(view.GetViewType(), view.GetViewObject());
            }

            OpenView(ViewType.Login);
        }

        public static ViewManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ViewManager>();
            }

            return _instance;
        }

        public IDisplayable OpenView(ViewType viewType)
        {
            _instance.LastViewType = _instance.CurrentViewType;
            _instance.CurrentViewType = viewType;
            
            _instance.CurrentView.Destroy();
            _instance.CurrentView = Instantiate(_views[viewType]);
            
            IDisplayable displayable = CurrentView.GetComponent<IDisplayable>();
            displayable?.Display();
            return displayable;
        }
    }
}