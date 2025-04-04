using UnityEngine;
using UnityEngine.Events;

namespace GameState {
    public class CountDownTimer : MonoBehaviour {
        [SerializeField] private float timeToWait = 5f;
        [SerializeField] private bool isActive = true;
        public UnityEvent onTimerEndEvent;

        public float CurrentTime { get; private set; }

        private void Start() {
            CurrentTime = timeToWait;
        }

        private void Update() {
            if (!isActive)
                return;

            CurrentTime -= Time.deltaTime;

            if (CurrentTime <= 0) {
                isActive = false;
                onTimerEndEvent.Invoke();
            }
        }
    }
}