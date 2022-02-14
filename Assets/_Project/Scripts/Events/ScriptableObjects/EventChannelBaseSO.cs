using UnityEngine;
namespace PolSl.UrbanHealthPath.Events.ScriptableObjects
{
    /// <summary>
    /// A class that represents a base EventChannel scriptable object.
    /// </summary>
    public class EventChannelBaseSO: ScriptableObject
    {
        [TextArea] public string description;
    }
}