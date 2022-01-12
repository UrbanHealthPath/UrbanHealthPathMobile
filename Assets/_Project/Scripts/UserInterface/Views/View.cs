using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    [Serializable] public struct View
    {
        [FormerlySerializedAs("type")] [SerializeField] private ViewType _type;
        [FormerlySerializedAs("viewObject")] [SerializeField] private GameObject _viewObject;

        public ViewType GetViewType()
        {
            return _type;
        }

        public GameObject GetViewObject()
        {
            return _viewObject;
        }
    }
}
