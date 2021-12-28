using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using PolSl.UrbanHealthPath.Navigation;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using ILocationProvider = PolSl.UrbanHealthPath.Navigation.ILocationProvider;

namespace PolSl.UrbanHealthPath.Player
{
    public class LocationProviderRotator : MonoBehaviour
    {
        [SerializeField] private LocationFactory _locationFactory;
        
        private Quaternion _targetRotation;
        
        private ILocationProvider _locationProvider;

        void Start()
        {
            _locationProvider = _locationFactory.LocationProvider;
            _locationProvider.OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }
        
        void Update()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime * 2f);
        }

        private void OnDestroy()
        {
            if (_locationProvider != null)
            {
                _locationProvider.OnLocationUpdated -= LocationProvider_OnLocationUpdated;
            }
        }

        void LocationProvider_OnLocationUpdated(Location location)
        {

            float rotationAngle = location.UserHeading * -1f;
            if (location.IsUserHeadingUpdated)
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(rotationAngle));
            }
        }

        private Vector3 GetNewEulerAngles(float newAngle)
        {
            Quaternion localRotation = transform.localRotation;
            Vector3 currentEuler = localRotation.eulerAngles;
            Vector3 euler = Mapbox.Unity.Constants.Math.Vector3Zero;
            euler.y = -newAngle;
            euler.x = currentEuler.x;
            euler.z = currentEuler.z;
            return euler;
        }

    }
}
