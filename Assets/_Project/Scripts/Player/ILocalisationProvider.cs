using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;
using UnityEditor.UIElements;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Player
{
    public interface ILocalisationProvider
    {
        public Vector3 GetMapLocalisation();

        public Vector2d GetRealWorldLocalisation();
    }
}
