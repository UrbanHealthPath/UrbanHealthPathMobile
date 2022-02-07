using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "UrbanHealthPath/Events/Vector2 Event Channel")]
    public class Vector3EventChannelSO : EventChannelBaseSO
    {
        public UnityAction<Vector3> OnEventRaised;
        public void RaiseEvent(Vector3 value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}