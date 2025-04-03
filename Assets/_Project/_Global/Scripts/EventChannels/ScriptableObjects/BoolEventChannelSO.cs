using UnityEngine;

namespace _Global.EventChannels.ScriptableObjects {
    /// <summary>
    /// This event channel broadcasts and carries Boolean payload.
    /// </summary>
    [CreateAssetMenu(fileName = "BoolEventChannelSO", menuName = "Events/BoolEventChannelSO")]
    public class BoolEventChannelSO : GenericEventChannelSO<bool> {
    }
}