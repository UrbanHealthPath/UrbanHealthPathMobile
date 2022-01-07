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
        
        private float _userHeading = 0;

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
                _latitudeLongitude.Add("50.293877701686114, 18.665775076345152");//neptun
                _latitudeLongitude.Add("50.293959890143874, 18.665973026994976");//neptun
                _latitudeLongitude.Add("50.29412972812803, 18.665921153017187");//neptun
                _latitudeLongitude.Add("50.29441236209267, 18.665831086127245");//neptun
                _latitudeLongitude.Add("50.2946097951345, 18.66565155695886");//neptun
                _latitudeLongitude.Add("50.29466866140864, 18.66539969209907");//neptun
                _latitudeLongitude.Add("50.29536639000551, 18.666095456379832");//neptun
                _latitudeLongitude.Add("50.295569520229236, 18.66642735656821");//neptun
                _latitudeLongitude.Add("50.29553157055134, 18.667213172415376");//neptun
                _latitudeLongitude.Add("50.29560730745248, 18.667408155807326");//neptun
                _latitudeLongitude.Add("50.295441927973094, 18.667570201774158");//neptun
                _latitudeLongitude.Add("50.29550468525408, 18.66704212149742");//neptun
                _latitudeLongitude.Add("50.29426620830372, 18.668441971344834");//neptun
                _latitudeLongitude.Add("50.29342704663735, 18.66747600827502");//neptun
                _latitudeLongitude.Add("50.293120518941855, 18.6672573220019");//neptun
                _latitudeLongitude.Add("50.29291427963203, 18.665879989699373");//neptun
                _latitudeLongitude.Add("50.29266077335537, 18.665643962775558");//neptun
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
