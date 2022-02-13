using System;
using UnityEngine;

namespace PolSl.UrbanHealthPath.CameraMovement
{
    public class CameraRotationPoint : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private void FixedUpdate()
        {
            if(transform.position != _player.position)
                transform.position = _player.position;
        }
    }
}