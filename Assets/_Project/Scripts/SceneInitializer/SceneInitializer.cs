using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.Player;
using UnityEngine;

namespace PolSl.UrbanHealthPath.SceneInitializer
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private LocationProviderMapUpdater _locationProviderMapUpdater;

        [SerializeField] private LocationProviderRotator _locationProviderRotator;

        [SerializeField] private PlayerLocationTransformer _playerLocationTransformer;

        [SerializeField] private LocationFactoryMode _mode;

        private void Awake()
        {
            _locationProviderMapUpdater.Initialize(_mode);
            _locationProviderRotator.Initialize(_locationProviderMapUpdater.LocationFactory.LocationProvider);
            _playerLocationTransformer.Initialize(_locationProviderMapUpdater.LocationFactory.LocationProvider);
        }
    }
}
