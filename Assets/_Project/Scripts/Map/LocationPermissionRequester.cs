using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.Android;

namespace PolSl.UrbanHealthPath.Navigation
{
    
    public static class LocationPermissionRequester
    {
        public static bool RequestPermission()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
            return Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        }
    }
}
