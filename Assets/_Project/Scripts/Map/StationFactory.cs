using System;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    public class StationFactory : MonoBehaviour
    {
        [SerializeField] private Transform _statation;

        [SerializeField] private Transform _halo;
        
        private List<Coordinates> _coordinatesList;
        
        private AbstractMap _map;

        private ILocationUpdater _locationUpdater;

        private bool _initialized = false;

        private Vector3 _offset = new Vector3(0, 1, 0);

        private int _currentStationIndex = 0;

        private Transform _currentStationTransform;

        private bool _isSubscribedToLocationUpdatedEvent;

        public void Initialize(AbstractMap map, ILocationUpdater locationUpdater, List<Coordinates> coordinates)
        {
            _map = map;
            _locationUpdater = locationUpdater;
            
            if (coordinates != null && coordinates.Count != 0)
            {
                _coordinatesList = coordinates;
            }

            SubscribeToLocationUpdatedEvent();
            _initialized = true;
        }

        private void OnDestroy()
        {
            if (_isSubscribedToLocationUpdatedEvent)
            {
                UnsubscribeFromLocationUpdatedEvent();
            }
        }

        private void InitializeStations(LocationUpdatedArgs args)
        {
            UnsubscribeFromLocationUpdatedEvent();
            if (_initialized && _coordinatesList != null && _coordinatesList.Count != 0)
            {
                CreateStationHalo();
                foreach(Coordinates coordinates in _coordinatesList)
                {
                    Vector3 worldPosition = _map.GeoToWorldPosition(coordinates, false);
                    Transform newStation = Instantiate(_statation, worldPosition+_offset, _statation.rotation);
                    newStation.SetParent(transform);
                }
            }
        }

        private void SubscribeToLocationUpdatedEvent()
        {
            _locationUpdater.LocationUpdated += InitializeStations;
            _isSubscribedToLocationUpdatedEvent = true;
        }

        private void UnsubscribeFromLocationUpdatedEvent()
        {
            _locationUpdater.LocationUpdated -= InitializeStations;
            _isSubscribedToLocationUpdatedEvent = false;
        }
        
        private void CreateStationHalo()
        {
            Vector3 worldPosition = _map.GeoToWorldPosition(_coordinatesList[_currentStationIndex], false);
            _currentStationTransform =
                Instantiate(_halo, worldPosition + _offset, _halo.rotation);
            _currentStationTransform.SetParent(this.transform);
        }

        //ultimately I think this should just be an event listener of station finished event
        public void MoveStationHalo()
        {
            _currentStationIndex++;
            if (_currentStationTransform != null && _currentStationIndex >= _coordinatesList.Count)
            {
                Destroy(_currentStationTransform.gameObject);
                return;
            }

            _currentStationTransform.position =
                _map.GeoToWorldPosition(_coordinatesList[_currentStationIndex], false) + _offset;
        }
    }
}