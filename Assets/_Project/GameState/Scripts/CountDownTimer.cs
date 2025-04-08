using _Global;
using UnityEngine;
using UnityEngine.Events;

namespace GameState {
    public class CountDownTimer : MonoBehaviour {
        public UnityEvent onTimerEndEvent;

        private Timer _timer;
        private bool _running;

        public float CurrentTime => _timer?.CurrentTime ?? 0f;

        private void Update() {
            if (_timer == null || !_running)
                return;

            if (_timer.Tick(Time.deltaTime) && _running) {
                _running = false;
                onTimerEndEvent.Invoke();
                GameManager.Instance.OnLose();
            }
        }
        
        public void SetTimeToWait(float time) {
            if (_timer == null) {
                _timer = new Timer(time);
            } else {
                _timer.SetTime(time);
            }
            _running = true;
        }
    }
}
