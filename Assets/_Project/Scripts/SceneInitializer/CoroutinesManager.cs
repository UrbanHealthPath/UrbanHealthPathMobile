using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolSl.UrbanHealthPath.Map;

namespace PolSl.UrbanHealthPath.SceneInitializer
{
    public class CoroutinesManager : MonoBehaviour
    {
        private ILocationProvider _locationProvider;

        private bool _initialized = false;

        private bool _runLocationCoroutine;

        private Coroutine _locationCoroutine;

        public void Initialize(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _initialized = true;
        }

        public void StartCoroutines()
        {
            _runLocationCoroutine = true;
            _locationCoroutine = StartCoroutine(PollLocation());
        }

        public void StopLocationCoroutine()
        {
            _runLocationCoroutine = false;
            StopCoroutine(_locationCoroutine);
        }
        private IEnumerator PollLocation()
        {
            while (_initialized && _runLocationCoroutine)
            {
                Mapbox.Unity.Location.Location location = _locationProvider.GetLocation();
                yield return new WaitForSeconds(1.0f);
            }
        }        
    }
    
    
}
