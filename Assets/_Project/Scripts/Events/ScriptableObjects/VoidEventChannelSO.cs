using UnityEngine;
using UnityEngine.Events;

namespace PolSl.UrbanHealthPath.Events.ScriptableObjects
{
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