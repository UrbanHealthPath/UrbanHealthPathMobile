using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Components;
using PolSl.UrbanHealthPath.UserInterface.Components.List;
using PolSl.UrbanHealthPath.UserInterface.Initializers;
using PolSl.UrbanHealthPath.UserInterface.Interfaces;
using PolSl.UrbanHealthPath.UserInterface.Popups;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class ViewManager : MonoBehaviour
    {
        public ViewType CurrentViewType { get; private set; }
        public GameObject CurrentView { get; private set; }
        public ViewType LastViewType { get; private set; }
        public History History { get; private set; }
        
        [FormerlySerializedAs("viewsWithTypes")] [SerializeField] private View[] _viewsWithTypes;

        private Dictionary<ViewType, GameObject> _views;
        
        public void Initialize()
        {
            CurrentView = null;

            _views = new Dictionary<ViewType, GameObject>();

            foreach (var view in _viewsWithTypes)
            {
                _views.Add(view.GetViewType(), view.GetViewObject());
            }

            History = new History();
        }
        
        public GameObject OpenView(ViewType viewType, IViewInitializationParameters initializationParameters = null)
        {
            LastViewType = CurrentViewType;
            CurrentViewType = viewType;

            CurrentView.Destroy();

            if (viewType != ViewType.None)
            {
                CurrentView = Instantiate(_views[viewType]);
                History.AddToHistory(viewType, initializationParameters);
                InitializeCurrentView(initializationParameters);
            }

            return CurrentView;
        }

        public void InitializeCurrentView(IViewInitializationParameters initializationParameters)
        {
            if (initializationParameters != null)
            {
                IInitializableView initializableView = CurrentView.GetComponent<IInitializableView>();
                initializableView?.Initialize(initializationParameters);
            }
        }
    }
}