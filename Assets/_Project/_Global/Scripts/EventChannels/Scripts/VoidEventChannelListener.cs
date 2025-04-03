using System.Collections;
using _Global.EventChannels.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace _Global.EventChannels {
    /// <summary>
    ///     This runs a UnityEvent in response to receiving a specific event channel.
    ///     Use it as a simple means of creating codeless interactivity.
    /// </summary>
    public class VoidEventChannelListener : MonoBehaviour {
        [Header("Listen to Event Channels")]
        [Tooltip("The signal to listen for")]
        [SerializeField] private VoidEventChannelSO eventChannel;
        [Space]
        [Tooltip("Responds to receiving signal from Event Channel")]
        [SerializeField] private UnityEvent response;
        [SerializeField] private float delay;

        private void OnEnable() {
            if (eventChannel)
                eventChannel.OnEventRaised += OnEventRaised;
        }

        private void OnDisable() {
            if (eventChannel)
                eventChannel.OnEventRaised -= OnEventRaised;
        }

        private void OnEventRaised() {
            StartCoroutine(RaiseEventDelayed(delay));
        }

        private IEnumerator RaiseEventDelayed(float delay) {
            yield return new WaitForSeconds(delay);
            response?.Invoke();
        }
    }
}