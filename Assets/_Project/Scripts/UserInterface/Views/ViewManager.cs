using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class ViewManager : MonoBehaviour
    {
        public ViewType CurrentViewType { get; private set; }
        public GameObject CurrentView { get; private set; }
        public ViewType LastViewType { get; private set; }
        public History History { get; private set; }

        [Serializable] public struct View
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

        private static ViewManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                _instance.CurrentView = null;

                _instance._views = new Dictionary<ViewType, GameObject>();

                foreach (var view in viewsWithTypes)
                {
                    _views.Add(view.GetViewType(), view.GetViewObject());
                }

                History = new History();
            }
        }

        // private void Start()
        // {
        //     _instance.CurrentView = null;
        //
        //     _instance._views = new Dictionary<ViewType, GameObject>();
        //
        //     foreach (var view in viewsWithTypes)
        //     {
        //         _views.Add(view.GetViewType(), view.GetViewObject());
        //     }
        //
        //     History = new History();
        // }

        public static ViewManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ViewManager>();
            }

            return _instance;
        }

        public GameObject OpenView(ViewType viewType, Initializer initializer = null)
        {
            _instance.LastViewType = _instance.CurrentViewType;
            _instance.CurrentViewType = viewType;

            _instance.CurrentView.Destroy();

            if (viewType != ViewType.None)
            {
                _instance.CurrentView = Instantiate(_views[viewType]);
                _instance.History.AddToHistory(viewType, initializer);
                InitializeCurrentView(initializer);
            }

            return _instance.CurrentView;
        }

        public void InitializeCurrentView(Initializer initializer)
        {
            if (initializer != null)
            {
                IInitializable initializable = _instance.CurrentView.GetComponent<IInitializable>();
                initializable?.Initialize(initializer);
            }
        }
    }
}