using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Events.ScriptableObjects
{
    /// <summary>
    /// A class that represents a Vector3EventChannelSO scriptable object. It invokes an event without params.
    /// </summary>
    [CreateAssetMenu(menuName = "UrbanHealthPath/Events/Void Event Channel")]
    public class VoidEventChannelSO : EventChannelBaseSO
    {
        public UnityAction OnEventRaised;
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}