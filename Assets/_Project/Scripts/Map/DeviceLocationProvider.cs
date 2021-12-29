using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEditor;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Navigation
{
    public class DeviceLocationProvider : MonoBehaviour, ILocationProvider
    {
        [SerializeField] private float _desiredAccuracyInMeters = 1.0f;

        [SerializeField] private float _updateDistanceInMeters = 0.0f;
        
        [SerializeField] private AngleSmoothingAbstractBase _deviceOrientationSmoothing;

        private Location _currentLocation;

        private IMapboxLocationService _locationService;

        private Vector2d _lastPosition;

        private double _lastLocationTimeStamp;

        private WaitForSeconds _waitUpdateTime;

        private Coroutine _locationCoroutine;

        public void Awake()
        {
            _locationService = new MapboxLocationServiceUnityWrapper();
            _currentLocation.Provider = "unity";
            _currentLocation.IsLocationServiceEnabled = true;
            _locationService.Start(_desiredAccuracyInMeters, _updateDistanceInMeters);
            Input.compass.enabled = true;
            _waitUpdateTime = new WaitForSeconds(1f);
        }

        public void Start()
        {
            _locationCoroutine = StartCoroutine(PollLocation());
        }

        private IEnumerator PollLocation()
        {
            System.Globalization.CultureInfo invariantCulture = System.Globalization.CultureInfo.InvariantCulture;
            while (true)
            {
                IMapboxLocationInfo lastData = _locationService.lastData;
                _currentLocation.IsLocationServiceEnabled = _locationService.status == LocationServiceStatus.Running ||
                                                            lastData.timestamp > _lastLocationTimeStamp;
                _deviceOrientationSmoothing.Add(Input.compass.trueHeading);
                _currentLocation.UserHeading = (float) _deviceOrientationSmoothing.Calculate();
                _currentLocation.IsUserHeadingUpdated = true;
                double latitude = double.Parse(lastData.latitude.ToString("R", invariantCulture), invariantCulture);
                double longitude = double.Parse(lastData.longitude.ToString("R", invariantCulture), invariantCulture);
                _lastPosition = _currentLocation.LatitudeLongitude;
                _currentLocation.LatitudeLongitude = new Vector2d(latitude, longitude);
                _currentLocation.Accuracy = (float) Math.Floor(lastData.horizontalAccuracy);
                _currentLocation.Timestamp = lastData.timestamp;
                _currentLocation.IsLocationUpdated = _currentLocation.Timestamp > _lastLocationTimeStamp ||
                                                     !_currentLocation.LatitudeLongitude.Equals(_lastPosition);
                _lastLocationTimeStamp = _currentLocation.Timestamp;
                SendLocation(_currentLocation);
                yield return _waitUpdateTime;
            }
        }

        public Location GetLocation()
        {
            return _currentLocation;
        }
        
        public event Action<Location> OnLocationUpdated = delegate {};

        private void SendLocation(Location location)
        {
            OnLocationUpdated(location);
        }
    }
}
