using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using PolSl.UrbanHealthPath.Navigation;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class LocationProviderMapUpdater : MonoBehaviour
    {
        [SerializeField] private AbstractMap _map;

        [SerializeField] private LocationFactory _locationFactory;
        
        [SerializeField] private float _lerpTime = 1f;
        
        private bool _isMapInitialized = false;

        private bool _isLerping;

        private Vector3 _startPosition;

        private Vector3 _endPosition;

        private Vector2d _startLatLong;

        private Vector2d _endLatLong;

        private float _lerpStartTime;

        void Start()
        {
            _locationFactory.GetProvider().OnLocationUpdated+=LocationProvider_OnFirstLocationUpdate;
        }
        
        void LocationProvider_OnFirstLocationUpdate(Location location)
        {
            _locationFactory.GetProvider().OnLocationUpdated -= LocationProvider_OnFirstLocationUpdate;
            _map.OnInitialized += () =>
            {
                _isMapInitialized = true;
                _locationFactory.GetProvider().OnLocationUpdated += LocationProvider_OnLocationUpdated;
            };
            _map.Initialize(location.LatitudeLongitude, _map.AbsoluteZoom);
        }
        
        void LocationProvider_OnLocationUpdated(Location location)
        {
            if (_isMapInitialized && location.IsLocationUpdated)
            {
                StartLerping(location);
            }
        }
        
        void StartLerping(Location location)
        {
            _isLerping = true;
            _lerpStartTime = Time.time;
            _lerpTime = Time.deltaTime;
            _startLatLong = _map.CenterLatitudeLongitude;
            _endLatLong = location.LatitudeLongitude;
            _startPosition = _map.GeoToWorldPosition(_startLatLong, false);
            _endPosition = _map.GeoToWorldPosition(_endLatLong, false);
        }
        
        void LateUpdate()
        {
            if (_isMapInitialized && _isLerping)
            {
                float timeSinceStarted = Time.time - _lerpStartTime;
                float percentageComplete = timeSinceStarted / _lerpTime;
                _startPosition = _map.GeoToWorldPosition(_startLatLong, false);
                _endPosition = _map.GeoToWorldPosition(_endLatLong, false);
                var position = Vector3.Lerp(_startPosition, _endPosition, percentageComplete);
                var latLong = _map.WorldToGeoPosition(position);
                _map.UpdateMap(latLong, _map.Zoom);
                if (percentageComplete >= 1.0f)
                {
                    _isLerping = false;
                }
            }
        }
    }
}
