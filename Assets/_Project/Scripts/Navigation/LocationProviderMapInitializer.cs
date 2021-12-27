using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Navigation
{
    public class LocationProviderMapInitializer : MonoBehaviour
    {
        [SerializeField] private AbstractMap _map;

        [SerializeField] private LocationFactory _factory;
        
        private void Awake()
        {
            _map.InitializeOnStart = false;
        }
        
        private void Start()
        {
            _factory.GetProvider().OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }

        void LocationProvider_OnLocationUpdated(Location location)
        {
            _factory.GetProvider().OnLocationUpdated -= LocationProvider_OnLocationUpdated;
            _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
        }
    }
}
