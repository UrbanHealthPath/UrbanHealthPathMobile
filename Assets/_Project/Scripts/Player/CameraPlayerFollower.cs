using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class CameraPlayerFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private Vector3 _offset;

        private void LateUpdate()
        {
            transform.position = _target.position + _offset;
        }
    }
}
