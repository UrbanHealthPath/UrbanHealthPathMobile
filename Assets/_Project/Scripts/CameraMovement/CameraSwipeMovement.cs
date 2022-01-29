using PolSl.UrbanHealthPath.Events.ScriptableObjects;
using UnityEngine;

namespace PolSl.UrbanHealthPath.CameraMovement
{
    public class CameraSwipeMovement : MonoBehaviour
    {
        [SerializeField] private Vector3EventChannelSO _onMapSwiped;
        [SerializeField] private VoidEventChannelSO _onMapCentered;
        [SerializeField] private UnityEngine.Camera _cam;
        [SerializeField] private CameraPlayerFollower _playerFollower;

        private Vector3 _desiredPosition, _startPosition;
        private bool _shouldMove;

        private void OnEnable()
        {
            _onMapCentered.OnEventRaised += CenterMap;
            _onMapSwiped.OnEventRaised += MoveCamera;
        }


        private void OnDisable()
        {
            _onMapCentered.OnEventRaised -= CenterMap;
            _onMapSwiped.OnEventRaised -= MoveCamera;
        }

        private void CenterMap()
        {
            _shouldMove = false;
            _playerFollower.enabled = true;
        }

        private void MoveCamera(Vector3 pos)
        {
            _playerFollower.enabled = false;
            _startPosition = _cam.transform.position;
            _desiredPosition = new Vector3(_startPosition.x + pos.x, _cam.transform.position.y,
                _startPosition.z + pos.z);
            _shouldMove = true;
        }

        private void FixedUpdate()
        {
            if (Mathf.Approximately(Vector3.Distance(_cam.transform.position, _desiredPosition), 0))
                _shouldMove = false;

            if (_shouldMove)
                _cam.transform.position = Vector3.Lerp(_startPosition, _desiredPosition, 0.01f);
        }
    }
}