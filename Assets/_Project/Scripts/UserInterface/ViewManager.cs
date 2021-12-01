using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface;
using UnityEngine;

namespace PolSl.UrbanHealthPath.UserInterface
{
    public class ViewManager : MonoBehaviour
    {
        public Dictionary<ViewType, GameObject> views;

        private GameObject _currentView = null;

        private static ViewManager _instance = null;
        private void Start()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else 
            {
                _instance = this;
            }
        }

        public static ViewManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ViewManager>();
            }

            return _instance;
        }

        public void OpenView(ViewType viewType)
        {
            _currentView.Destroy();
            _currentView = Instantiate(views[viewType]);
        }

    }
}
