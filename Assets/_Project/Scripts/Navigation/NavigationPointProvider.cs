using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class NavigationPointProvider : MonoBehaviour
    {
        [SerializeField] private Transform _point;
        [SerializeField] private string _logituteLatitiute;
        [SerializeField] private AbstractMap _map;

        public void PutOnMap()
        {
            _point.position = _map.GeoToWorldPosition(Conversions.StringToLatLon(_logituteLatitiute), 
                                              false);
        }
    }
}
