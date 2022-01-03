using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.Android;

namespace PolSl.UrbanHealthPath.Map
{
    
    public class LocationPermissionRequester
    {
        private LocationPermissionRequester _instance;

        public LocationPermissionRequester()
        {
            _instance = this;
        }

        public bool RequestPermission()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
            return Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        }
    }
}
