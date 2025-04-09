using _Global.EventChannels.ScriptableObjects;
using UnityEngine;

namespace Map {
    /// <summary>
    /// General Event Channel that broadcasts and carries MapSpawnArea payload.
    /// </summary>
    /// 
    [CreateAssetMenu(menuName = "Events/MapSpawnAreaEventChannelSO", fileName = "MapSpawnAreaEventChannel")]
    public class MapSpawnAreaEventChannelSO : GenericEventChannelSO<MapData> {
    }
}