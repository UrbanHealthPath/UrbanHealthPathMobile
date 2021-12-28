using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Navigation
{
    public class FakeLocationProvider : MonoBehaviour, ILocationProvider
    {
        [SerializeField] [Range(0, 359)] private float _userHeading = 112;

        [SerializeField] private int _accuracy = 5;
        
        [SerializeField] private string[] _latituteLongitude;
        
        private Location _currentLocation;

        private int _index = -1;

        private void Start()
        {
            PollLocation();
        }
        
        private void PollLocation()
        {
            _index++;
            if (_index >= _latituteLongitude.Length)
            {
                _index = 0;
            }
            _currentLocation.LatitudeLongitude = Conversions.StringToLatLon(_latituteLongitude[_index]);
            _currentLocation.Timestamp = UnixTimestampUtils.To(DateTime.UtcNow);
            _currentLocation.IsLocationUpdated = true;
            _currentLocation.IsUserHeadingUpdated = true;
            _currentLocation.UserHeading = _userHeading;
            _currentLocation.Accuracy = _accuracy;
            SendLocation(_currentLocation);
        }
        
        public event Action<Location> OnLocationUpdated = delegate {};

        private void SendLocation(Location location)
        {
            OnLocationUpdated(location);
        }

        public Location GetLocation()
        {
            PollLocation();
            return _currentLocation;
        }

        public void CallLocationQuery()
        {
            PollLocation();
        }
    }
}
