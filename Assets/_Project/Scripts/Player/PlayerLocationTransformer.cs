using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Navigation;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Player
{
    public class PlayerLocationTransformer : MonoBehaviour
    {
        [SerializeField] private LocationFactory _locationFactory;
        

        [SerializeField] private AbstractMap _map;

        private bool _initliazed;

        private void Start()
        {
              _map.OnInitialized += () => _initliazed = true;
              _locationFactory.GetProvider().OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }

        void LocationProvider_OnLocationUpdated(Location location)
        {
            if (_initliazed)
            {
                _map.transform.position =
                    _map.GeoToWorldPosition(location.LatitudeLongitude);
            }
        }
    }
}
