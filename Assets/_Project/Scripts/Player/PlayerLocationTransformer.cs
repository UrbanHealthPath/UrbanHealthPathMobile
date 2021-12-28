using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Navigation;
using UnityEngine;
using ILocationProvider = PolSl.UrbanHealthPath.Navigation.ILocationProvider;

namespace PolSl.UrbanHealthPath.Player
{
    public class PlayerLocationTransformer : MonoBehaviour
    {
        [SerializeField] private LocationFactory _locationFactory;
        
        [SerializeField] private AbstractMap _map;

        private ILocationProvider _locationProvider;

        private bool _initliazed;

        private void Start()
        {
              _map.OnInitialized += () => _initliazed = true;
              if (_locationProvider == null)
              {
                  _locationProvider = _locationFactory.LocationProvider;
              }
              _locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }

        void LocationProvider_OnLocationUpdated(Location location)
        {
            if (_initliazed)
            {
                transform.position = _map.GeoToWorldPosition(location.LatitudeLongitude);
            }
        }
    }
}
