using UnityEngine;

namespace PolSl.UrbanHealthPath
{
    public class CameraPlayerFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset, 0.2f);
        }
    }
}