using UnityEngine;

namespace PolSl.UrbanHealthPath.Utils
{
    public static class EulerAngles
    {
        public static Vector3 GetNewEulerAnglesY(float newYAngle, Vector3 currentEuler)
        {
           // Vector3 currentEuler = _rotationPoint.localRotation.eulerAngles;
            Vector3 euler = Vector3.zero;

            euler.y = -newYAngle;
            euler.x = currentEuler.x;
            euler.z = currentEuler.z;

            return euler;
        }
    }
}