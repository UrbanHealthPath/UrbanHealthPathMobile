using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using PolSl.UrbanHealthPath.Map;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class FakeLocationProvider : AbstractEditorLocationProvider, ILocalizationProvider
    {
        // <summary>
        /// The mock "latitude, longitude" location, respresented with a string.
        /// You can search for a place using the embedded "Search" button in the inspector.
        /// This value can be changed at runtime in the inspector.
        /// </summary>
        [SerializeField]
        [Geocode]
        string[] _latitudeLongitude;

        /// <summary>
        /// The mock heading value.
        /// </summary>
        [SerializeField]
        [Range(0, 359)]
        float _heading;


        private int idx = -1;

        public Vector2d GetLocalization()
        {

            idx++;
            // reset index to keep looping through the location array
            if (idx >= _latitudeLongitude.Length)
            {
                idx = 0;
            }

            return Conversions.StringToLatLon(_latitudeLongitude[idx]);

        }

        protected override void SetLocation()
        {
            _currentLocation.UserHeading = _heading;
            _currentLocation.LatitudeLongitude = GetLocalization();
            _currentLocation.Accuracy = _accuracy;
            _currentLocation.Timestamp = UnixTimestampUtils.To(DateTime.UtcNow);
            _currentLocation.IsLocationUpdated = true;
            _currentLocation.IsUserHeadingUpdated = true;
        }
    }
}
