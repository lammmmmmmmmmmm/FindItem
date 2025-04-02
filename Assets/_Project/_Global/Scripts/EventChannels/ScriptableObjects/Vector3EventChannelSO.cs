using UnityEngine;

namespace _Global.EventChannels.ScriptableObjects {
    /// <summary>
    /// General event channel that broadcasts and carries Vector3 payload.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Vector3EventChannelSO", fileName = "Vector3EventChannel")]
    public class Vector3EventChannelSO : GenericEventChannelSO<Vector3> {
    }
}