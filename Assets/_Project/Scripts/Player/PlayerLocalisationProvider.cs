using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Map;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Player
{
    public class PlayerLocalisationProvider : MonoBehaviour,ILocalisationProvider
    {
        [SerializeField] private Transform _player;

        [SerializeField] private AbstractMap _map;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public Vector3 GetMapLocalisation()
        {
            return _player.position;
        }

        public Vector2d GetRealWorldLocalisation()
        {
            return _map.WorldToGeoPosition(_player.position);
        }
    }
}
