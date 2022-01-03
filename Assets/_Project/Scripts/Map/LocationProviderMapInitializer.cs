using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
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
            _locationProvider.LocationUpdated += LocationProviderLocationUpdated;
        }

        private void LocationProviderLocationUpdated(Location location)
        {
            _locationProvider.LocationUpdated -= LocationProviderLocationUpdated;
            _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
        }
    }
}
