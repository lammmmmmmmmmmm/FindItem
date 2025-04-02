using UnityEngine;

namespace _Global.EventChannels.ScriptableObjects {
    /// <summary>
    /// General event channel that broadcasts and carries string payload.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/StringEventChannelSO", fileName = "StringEventChannel")]
    public class StringEventChannelSO : GenericEventChannelSO<string> {
    }
}