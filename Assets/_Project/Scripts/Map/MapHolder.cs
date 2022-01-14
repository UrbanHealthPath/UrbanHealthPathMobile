using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.Camera;
using PolSl.UrbanHealthPath.Map;
using PolSl.UrbanHealthPath.Navigation;
using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.Player;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class MapHolder : MonoBehaviour
    {
        [SerializeField] private StationFactory _stationFactory;
        [SerializeField] private NavigationPointProvider _navigationPointProvider;
        [SerializeField] private PlayerLocationTransformer _playerLocationTransformer;
        [SerializeField] private LocationProviderRotator _locationProviderRotator;
        [SerializeField] private AbstractMap _map;
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private RenderTexture _renderTexture;

        private bool _isInitialized;

        public StationFactory StationFactory => _stationFactory;
        public NavigationPointProvider NavigationPointProvider => _navigationPointProvider;
        public PlayerLocationTransformer PlayerLocationTransformer => _playerLocationTransformer;
        public LocationProviderRotator LocationProviderRotator => _locationProviderRotator;
        public AbstractMap Map => _map;

        public void Initialize(ILocationProvider locationProvider, List<Coordinates> stationsCoordinates)
        {
            _locationProviderRotator.Initialize(locationProvider);
            new LocationProviderMapInitializer(_map, locationProvider);
            _playerLocationTransformer.Initialize(_map, locationProvider);
            _navigationPointProvider.Initialize(_map, stationsCoordinates[0]);
            _stationFactory.Initialize(_map, locationProvider, stationsCoordinates);
            _camera.targetTexture = _renderTexture;
            _camera.enabled = true;
            _isInitialized = true;
        }

        public void OnDestroy()
        {
            if (_isInitialized)
            {
                _camera.enabled = false;
                _camera.targetTexture = null;
            }
        }
    }
}