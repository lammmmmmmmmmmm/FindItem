using System;
using TMPro;
using UnityEngine;

namespace GameState {
    public class TimerUIUpdater : MonoBehaviour {
        [SerializeField] private CountDownTimer countDownTimer;
        [SerializeField] private TextMeshProUGUI timerText;

        private void Update() {
            if (countDownTimer.CurrentTime > 0) {
                TimeSpan timeSpan = TimeSpan.FromSeconds(countDownTimer.CurrentTime);
                timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            } else {
                timerText.text = "00:00";
            }
        }
    }
}