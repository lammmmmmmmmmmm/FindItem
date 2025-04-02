using UnityEngine;
using UnityEngine.Events;

namespace _Global.EventChannels.ScriptableObjects {
    public abstract class GenericEventChannelSO<T> : EventChannelSOBase {
        [Tooltip("The action to perform; Listeners subscribe to this UnityAction")]
        public UnityAction<T> OnEventRaised;

        public void RaiseEvent(T parameter) {
            OnEventRaised?.Invoke(parameter);
        }
    }

    // To create addition event channels, simply derive a class from GenericEventChannelSO
    // filling in the type T. Leave the concrete implementation blank. This is a quick way
    // to create new event channels.

    // For example:
    //[CreateAssetMenu(menuName = "Events/Float EventChannel", fileName = "FloatEventChannel")]
    //public class FloatEventChannelSO : GenericEventChannelSO<float> {}

    // Define additional GenericEventChannels if you need more than one parameter in the payload.
}