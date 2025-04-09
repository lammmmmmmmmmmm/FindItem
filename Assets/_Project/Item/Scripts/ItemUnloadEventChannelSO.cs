using _Global.EventChannels.ScriptableObjects;
using UnityEngine;

namespace Item {
    [CreateAssetMenu(fileName = "ItemUnloadEventChannelSO", menuName = "Events/ItemUnloadEventChannelSO")]
    public class ItemUnloadEventChannelSO : GenericEventChannelSO<ItemUnloadPayload> {
    }
}