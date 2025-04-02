using _Global.EventChannels.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace _Global.EventChannels {
    public abstract class GenericEventChannelListener<T> : MonoBehaviour {
        [SerializeField] protected GenericEventChannelSO<T> channel;
        [SerializeField] protected UnityEvent<T> onEventRaised;

        protected virtual void OnEnable() {
            if (channel)
                channel.OnEventRaised += onEventRaised.Invoke;
        }

        protected virtual void OnDisable() {
            if (channel)
                channel.OnEventRaised -= onEventRaised.Invoke;
        }
    }
}