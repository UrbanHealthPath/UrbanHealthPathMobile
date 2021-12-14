using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PolSl.UrbanHealthPath.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("An object that determines camera rotation.")]
        private Transform _trackedObject;

        [SerializeField]
        [Tooltip("The minimum angle between rotation of the trackedObject and  x or z axis, that will cause camera 90 degrees rotation.")]
        [Range(10.0f, 40.0f)]
        private float _minAngle = 25.0f;
        
        private Transform _rotationPoint;
        
        private Quaternion _targetRotation;
        
        private float _rotationSpeed = 2.5f;

        public void Start()
        {
            _rotationPoint = this.transform.parent;
            _targetRotation = Quaternion.Euler(GetNewEulerAngles(0.0f));
        }

        void Update()
        {
            RotateCamera();
            _rotationPoint.localRotation =
                Quaternion.Slerp(_rotationPoint.localRotation, _targetRotation , Time.deltaTime * _rotationSpeed);
        }

        private void RotateCamera()
        {
            Quaternion zeroAngle = Quaternion.Euler(0f, 180f, 0f);
            float trackedObjectRotation = _trackedObject.localRotation.eulerAngles.y;
            float angle = Mathf.DeltaAngle(0.0f, trackedObjectRotation);

            if (angle > 90 - _minAngle && angle < 90 + _minAngle &&
                !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 270.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(270.0f));
            }
            else if ((angle > 180 - _minAngle && angle < 180) || angle < -(180 - _minAngle) &&
                !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 180.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(180.0f));
            }
            else if (angle > -90 - _minAngle && angle < -90 + _minAngle &&
                     !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 90.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(90.0f));
            }
            else if ((angle > -_minAngle && angle < 0) || (angle > 0 && angle < _minAngle) &&
                !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 0.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(0.0f));
            }
        }
        
        private Vector3 GetNewEulerAngles(float newAngle)
        {
            Vector3 currentEuler = _rotationPoint.localRotation.eulerAngles;
            Vector3 euler = Vector3.zero;

            euler.y = -newAngle;
            euler.x = currentEuler.x;
            euler.z = currentEuler.z;

            return euler;
        }
    }
}