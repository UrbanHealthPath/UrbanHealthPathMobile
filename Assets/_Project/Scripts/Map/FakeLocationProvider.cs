using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    public class FakeLocationProvider : ILocationProvider
    { 
        public event Action<Location> LocationUpdated = delegate {};
        
        private float _userHeading = 112;

        private int _accuracy = 5;
        
        private List<String> _latitudeLongitude;
        
        private Location _currentLocation;

        private int _index = -1;
        
        public FakeLocationProvider(List<String> latitudeLongitude)
        {
            _latitudeLongitude = new List<string>();
            if (latitudeLongitude != null && latitudeLongitude.Count!=0)
            {
                _latitudeLongitude.AddRange(latitudeLongitude);
            }
            else
            {
                _latitudeLongitude.Add("50.29416712031348, 18.665418882944635");//neptun
            }
        }
        
        public Location GetLocation()
        {
            PollLocation();
            return _currentLocation;
        }
        
        private void PollLocation()
        {
            _index++;
            if (_index >= _latitudeLongitude.Count)
            {
                _index = 0;
            }
            _currentLocation.LatitudeLongitude = Conversions.StringToLatLon(_latitudeLongitude[_index]);
            _currentLocation.Timestamp = UnixTimestampUtils.To(DateTime.UtcNow);
            _currentLocation.IsLocationUpdated = true;
            _currentLocation.IsUserHeadingUpdated = true;
            _currentLocation.UserHeading = _userHeading;
            _currentLocation.Accuracy = _accuracy;
            SendLocation(_currentLocation);
        }
        
        private void SendLocation(Location location)
        {
            LocationUpdated(location);
        }
    }
}
