using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Navigation;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Navigation
{
    public enum LocationFactoryMode
    {
        Fake,
        Device
    }
    public class LocationFactory : MonoBehaviour
    {
        [SerializeField] private FakeLocationProvider _fakeLocationProvider;

        [SerializeField] private DeviceLocationProvider _deviceLocationProvider;

        [SerializeField] private LocationFactoryMode _mode = LocationFactoryMode.Fake;

        private ILocationProvider _locationProvider;

        public ILocationProvider LocationProvider
        {
            get
            {
                return _locationProvider;
            }
            set
            {
                _locationProvider = value;
            }
        }
        
        private void Awake()
        { 
            InjectLocationProvider();
        }

        private void InjectLocationProvider()
        {
            if (_mode == LocationFactoryMode.Device && LocationPermissionRequester.RequestPermission())
            {
                _locationProvider = _deviceLocationProvider;
            }
            else
            {
                _locationProvider = _fakeLocationProvider;
            }
        }

        public void ChangeMode(LocationFactoryMode mode)
        {
            _mode = mode;
            InjectLocationProvider();
        }
    }
}
