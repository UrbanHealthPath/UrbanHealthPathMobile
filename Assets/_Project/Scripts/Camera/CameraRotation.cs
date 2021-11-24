using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PolSl.UrbanHealthPath.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraRotation : MonoBehaviour
    {
        [Tooltip("An object that determines camera rotation.")]
        public Transform trackedObject;

        [Tooltip("The minimum angle between rotation of the trackedObject and  x or z axis, that will cause camera 90 degrees rotation.")]
        [Range(10.0f, 40.0f)]
        public float minAngle = 25.0f;

        [Tooltip("Reference to the point around which the camera should rotate. ")]
        public Transform rotationPoint;
        
        private Quaternion _targetRotation;
        
        private float _rotationSpeed = 5.0f;

        public void Start()
        {
            _targetRotation = Quaternion.Euler(GetNewEulerAngles(0.0f));
        }

        void Update()
        {
            RotateCamera();
            rotationPoint.localRotation =
                Quaternion.Slerp(rotationPoint.localRotation, _targetRotation , Time.deltaTime * _rotationSpeed);
        }

        private void RotateCamera()
        {
            Quaternion zeroAngle = Quaternion.Euler(0f, 180f, 0f);
            var trackedObjectRotation = trackedObject.localRotation.eulerAngles.y;
            var angle = Mathf.DeltaAngle(0.0f, trackedObjectRotation);

            if (angle > 90 - minAngle && angle < 90 + minAngle &&
                !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 270.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(270.0f));
            }
            else if ((angle > 180 - minAngle && angle < 180) || angle < -(180 - minAngle) &&
                !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 180.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(180.0f));
            }
            else if (angle > -90 - minAngle && angle < -90 + minAngle &&
                     !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 90.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(90.0f));
            }
            else if ((angle > -minAngle && angle < 0) || (angle > 0 && angle < minAngle) &&
                !Mathf.Approximately(this.transform.localRotation.eulerAngles.z, 0.0f))
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(0.0f));
            }
        }
        
        private Vector3 GetNewEulerAngles(float newAngle)
        {
            var currentEuler = rotationPoint.localRotation.eulerAngles;
            var euler = Vector3.zero;

            euler.y = -newAngle;
            euler.x = currentEuler.x;
            euler.z = currentEuler.z;

            return euler;
        }
    }
}