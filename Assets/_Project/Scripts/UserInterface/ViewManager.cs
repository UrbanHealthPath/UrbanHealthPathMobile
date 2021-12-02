using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEngine;
using UnityEngine.UIElements;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class ViewManager : MonoBehaviour
    {
        
        [Serializable] public struct View
        {
            public ViewType type;
            public GameObject viewObject;
        }

        [SerializeField] private View[] viewsWithTypes;

        private Dictionary<ViewType, GameObject> _views;

        public GameObject CurrentView { get; private set; } 
    

        //   private RectTransform _currentView = null;

        private static ViewManager _instance = null;

        private void Awake()
        {
            CurrentView = null;
            
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            
            _views = new Dictionary<ViewType, GameObject>();
            foreach (var view in viewsWithTypes)
            {
                _views.Add(view.type, view.viewObject);
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

        public IView OpenView(ViewType viewType)
        {
            CurrentView.Destroy();
            CurrentView = Instantiate(_views[viewType]);
            IView view = CurrentView.GetComponent<IView>();
            view.Display();
            return view;
        }

        // public (float, float, Vector3) GetAreaSize()
        // {
        //     float x, y;
        //     var mapArea = CurrentView.gameObject.transform.Find("SafeArea/MapArea");
        //     var rectTransform = mapArea.GetComponent<RectTransform>();
        //     Debug.Log(" "+ mapArea.name);
        //     y = rectTransform.sizeDelta.y;
        //     x = rectTransform.sizeDelta.x;
        //
        //     Vector3 position = rectTransform.anchoredPosition;
        //
        //        Debug.Log("x = " +x+ " y = "+ y + " pos x" + position.x);
        //     return (x, y, position);
        //     
        //  
        // }
    }
}