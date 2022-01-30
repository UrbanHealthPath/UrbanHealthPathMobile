using System;
using System.Collections;
using System.Collections.Generic;
using PolSl.UrbanHealthPath.Events.ScriptableObjects;
using UnityEngine;

namespace PolSl.UrbanHealthPath.CameraMovement
{
    public class MapCameraZoom : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _onMapZoomOut;
        [SerializeField] private VoidEventChannelSO _onMapZoomIn;
        [SerializeField] private VoidEventChannelSO _onMaxZoomInAchieved;
        [SerializeField] private VoidEventChannelSO _onMaxZoomOutAchieved;
        [SerializeField] private UnityEngine.Camera _cam;

        [SerializeField] private int _maxZoomCount = 3;
        [SerializeField] private float _zoomDelta = 5;

        private float _initialZoom;
        private int _index;
        private float _currentZoom;

        private void OnEnable()
        {
            _initialZoom = _cam.fieldOfView;
            _currentZoom = _initialZoom;
            _onMapZoomOut.OnEventRaised += ZoomOut;
            _onMapZoomIn.OnEventRaised += ZoomIn;
        }

        private void OnDisable()
        {
            _onMapZoomOut.OnEventRaised -= ZoomOut;
            _onMapZoomIn.OnEventRaised -= ZoomIn;
        }

        public void SetInitialZoom()
        {
            _index = 0;
            _currentZoom = _initialZoom;
            _onMaxZoomInAchieved.RaiseEvent();

            StartCoroutine(ZoomLerp(_initialZoom, 0.5f));
        }

        private void ZoomOut()
        {
            _index++;

            if (_index > _maxZoomCount - 1)
            {
                _index--;
                return;
            }

            if (_index == _maxZoomCount - 1)
                _onMaxZoomOutAchieved.RaiseEvent();

            _currentZoom = _currentZoom + _zoomDelta;

            StartCoroutine(ZoomLerp(_currentZoom, 0.5f));
        }

        private void ZoomIn()
        {
            _index--;

            if (_index < 0)
            {
                _index = 0;
                return;
            }

            if (_index == 0)
                _onMaxZoomInAchieved.RaiseEvent();

            _currentZoom = _currentZoom - _zoomDelta;

            StartCoroutine(ZoomLerp(_currentZoom, 0.5f));
        }

        IEnumerator ZoomLerp(float endValue, float duration)
        {
            float time = 0;
            float startValue = _cam.fieldOfView;

            while (time < duration)
            {
                float t = time / duration;
                t = t * t * (3f - 2f * t);
                _cam.fieldOfView = Mathf.Lerp(startValue, endValue, t);
                time += Time.deltaTime;
                yield return null;
            }

            _cam.fieldOfView = endValue;
        }
    }
}