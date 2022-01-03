using PolSl.UrbanHealthPath.Map;
using UnityEngine;


namespace PolSl.UrbanHealthPath.Player
{
    public class LocationProviderRotator : MonoBehaviour
    {
        private Quaternion _targetRotation;
        
        private ILocationProvider _locationProvider;

        private bool _initialized = false;

        public void Initialize(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _initialized = true;
        }

        void Start()
        {
            if (_initialized)
            {
                _locationProvider.LocationUpdated += LocationProviderLocationUpdated;
            }
        }
        
        void Update()
        {
            if (_initialized)
            {
                transform.localRotation =
                    Quaternion.Lerp(transform.localRotation, _targetRotation, Time.deltaTime * 2f);
            }
        }

        private void OnDestroy()
        {
            if (_locationProvider != null)
            {
                _locationProvider.LocationUpdated -= LocationProviderLocationUpdated;
            }
        }

        void LocationProviderLocationUpdated(Mapbox.Unity.Location.Location location)
        {

            float rotationAngle = location.UserHeading * -1f;
            if (location.IsUserHeadingUpdated)
            {
                _targetRotation = Quaternion.Euler(GetNewEulerAngles(rotationAngle));
            }
        }

        private Vector3 GetNewEulerAngles(float newAngle)
        {
            Quaternion localRotation = transform.localRotation;
            Vector3 currentEuler = localRotation.eulerAngles;
            Vector3 euler = Mapbox.Unity.Constants.Math.Vector3Zero;
            euler.y = -newAngle;
            euler.x = currentEuler.x;
            euler.z = currentEuler.z;
            return euler;
        }

    }
}
