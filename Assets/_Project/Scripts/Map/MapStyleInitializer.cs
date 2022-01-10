using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using PolSl.UrbanHealthPath.PathData;


namespace PolSl.UrbanHealthPath.Map
{
    public class MapStyleInitializer 
    {
        public void SetMapImageLayer(AbstractMap map, string styleURL, Coordinates coordinates, int zoom)
        {
            SetImageLayer(map, styleURL);
            map.Initialize(coordinates, zoom);
        }

        public void UpdateMapImageLayer(AbstractMap map, string styleURL)
        {
            SetImageLayer(map, styleURL);
            map.UpdateMap();
        }

        public void UpdateMapImageLayer(AbstractMap map, string styleURL,Coordinates coordinates, int zoom)
        {
            SetImageLayer(map, styleURL);
            map.UpdateMap(coordinates, zoom);
        }

        private void SetImageLayer(AbstractMap map, string styleURL)
        {
            map.ImageLayer.SetProperties(ImagerySourceType.Custom, true, false, false);
            map.ImageLayer.SetLayerSource(styleURL);
        }
    }
}
