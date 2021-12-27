using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using PolSl.UrbanHealthPath.Navigation;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Player
{
    public class LocationProviderRotator : MonoBehaviour
    {
        [SerializeField] private LocationFactory _locationFactory;
        
        private Quaternion _targetRotation;
        
        void Start()
        {
            _locationFactory.GetProvider().OnLocationUpdated += LocationProvider_OnLocationUpdated;
        }
        
        void Update()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime * 2f);
        }
        
        void LocationProvider_OnLocationUpdated(Location location)
        {

            float rotationAngle = location.UserHeading * -1f;
            if (location.IsUserHeadingUpdated)
            {
                _targetRotation = Quaternion.Euler(getNewEulerAngles(rotationAngle));
            }
        }

        private Vector3 getNewEulerAngles(float newAngle)
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
