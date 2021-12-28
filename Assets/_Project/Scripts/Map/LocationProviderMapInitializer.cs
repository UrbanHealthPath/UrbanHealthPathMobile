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

        [SerializeField] private LocationFactory _locationFactory;

        private ILocationProvider _locationProvider;
        
        private void Awake()
        {
            _map.InitializeOnStart = false;
        }
        
        private void Start()
        {
            _locationProvider = _locationFactory.LocationProvider;
            _locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }

        void LocationProvider_OnLocationUpdated(Location location)
        {
            _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
            _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
        }
    }
}
