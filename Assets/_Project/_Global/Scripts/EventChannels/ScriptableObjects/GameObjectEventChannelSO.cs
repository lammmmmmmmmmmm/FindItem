using UnityEngine;

namespace _Global.EventChannels.ScriptableObjects {
    /// <summary>
    /// This is a ScriptableObject-based event that carries a GameObject as a payload.
    /// </summary>
    [CreateAssetMenu(fileName = "GameObjectChannelSO", menuName = "Events/GameObjectEventChannelSO")]
    public class GameObjectEventChannelSO : GenericEventChannelSO<GameObject> {
    }
}