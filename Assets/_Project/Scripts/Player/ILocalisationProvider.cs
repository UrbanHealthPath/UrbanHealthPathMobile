using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEditor.UIElements;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Map
{
    public interface ILocalizationProvider
    {
        public Vector2d GetLocalization();
    }
}
