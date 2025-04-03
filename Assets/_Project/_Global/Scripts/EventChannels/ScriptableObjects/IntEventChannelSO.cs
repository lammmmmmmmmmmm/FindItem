using UnityEngine;

namespace _Global.EventChannels.ScriptableObjects {
    /// <summary>
    /// This is a ScriptableObject-based event that takes an integer as a payload.
    /// </summary> 
    [CreateAssetMenu(menuName = "Events/IntEventChannelSO", fileName = "IntEventChannel")]
    public class IntEventChannelSO : GenericEventChannelSO<int> {
    }
}