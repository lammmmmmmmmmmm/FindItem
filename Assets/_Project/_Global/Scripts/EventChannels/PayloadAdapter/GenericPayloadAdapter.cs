using _Global.EventChannels.ScriptableObjects;
using UnityEngine;

namespace _Global.EventChannels.PayloadAdapter {
    public abstract class GenericPayloadAdapter<T> : MonoBehaviour {
        [SerializeField] protected GenericEventChannelSO<T> channel;
        protected virtual void OnEnable() {
            if (channel)
                channel.OnEventRaised += Handle;
        }

        protected virtual void OnDisable() {
            if (channel)
                channel.OnEventRaised -= Handle;
        }
        
        protected abstract void Handle(T payload);
    }
}