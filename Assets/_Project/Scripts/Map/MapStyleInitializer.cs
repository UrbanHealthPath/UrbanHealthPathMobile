using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class MapStyleInitializer : MonoBehaviour
    {
        [SerializeField] private string _styleURL;

        [SerializeField] private AbstractMap _map;

        [SerializeField] private string _latLong;

        private bool _mapInitialized = false;

        private void Awake()
        {
            _map.InitializeOnStart = false;
        }

        private void SetMapImageLayer()
        {

            _map.ImageLayer.SetProperties(ImagerySourceType.Custom, true, false, false);
            _map.ImageLayer.SetLayerSource(_styleURL);
            if (!_mapInitialized)
            {
                _map.Initialize(Conversions.StringToLatLon(_latLong), _map.AbsoluteZoom);
                _mapInitialized = true;
            }
            else
            {
                _map.UpdateMap();            }
        }

        public void ButtonClicked_SetMapImageLayer()
        {
            SetMapImageLayer();
        }
    }
}
