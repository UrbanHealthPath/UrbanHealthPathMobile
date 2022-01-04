using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    public class MapStyleInitializer 
    {
        public void SetMapImageLayer(AbstractMap map, string styleURL, string latLong, int zoom)
        {
            map.ImageLayer.SetProperties(ImagerySourceType.Custom, true, false, false);
            map.ImageLayer.SetLayerSource(styleURL);
            map.Initialize(Conversions.StringToLatLon(latLong), zoom);
        }

        public void UpdateMapImageLayer(AbstractMap map, string styleURL)
        {
            map.ImageLayer.SetProperties(ImagerySourceType.Custom, true, false, false);
            map.ImageLayer.SetLayerSource(styleURL);
            map.UpdateMap();
        }

        public void UpdateMapImageLayer(AbstractMap map, string styleURL,string latLong, int zoom)
        {
            map.ImageLayer.SetProperties(ImagerySourceType.Custom, true, false, false);
            map.ImageLayer.SetLayerSource(styleURL);
            map.UpdateMap(Conversions.StringToLatLon(latLong), zoom);
        }
    }
}
