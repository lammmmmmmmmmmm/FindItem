using UnityEngine;
using UnityEngine.Events;

namespace _Global.EventChannels.ScriptableObjects {
    /// <summary>
    /// General Event Channel that carries no extra data.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/VoidEventChannelSO", fileName = "VoidEventChannel")]
    public class VoidEventChannelSO : EventChannelSOBase {
        [Tooltip("The action to perform")]
        public UnityAction OnEventRaised;

        public void RaiseEvent() {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}