using GameState;
using Map;
using UnityEngine;

namespace _Global.EventChannels.ScriptableObjects {
    /// <summary>
    /// General Event Channel that broadcasts and carries MapSpawnArea payload.
    /// </summary>
    /// 
    [CreateAssetMenu(menuName = "Events/MapSpawnAreaEventChannelSO", fileName = "MapSpawnAreaEventChannel")]
    public class MapSpawnAreaEventChannelSO : GenericEventChannelSO<MapSpawnArea> {
    }
}