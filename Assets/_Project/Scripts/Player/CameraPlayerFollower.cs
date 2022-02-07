using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class CameraPlayerFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _xOffset;
        [SerializeField] private float _zOffset;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, 
                new Vector3(_target.position.x + _xOffset, transform.position.y, _target.position.z + _zOffset), 0.2f);
        }
    }
}