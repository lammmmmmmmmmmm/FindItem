using System;
using TMPro;
using UnityEngine;

namespace Survivor.Gameplay
{
    public class GameTimer : MonoBehaviour
    {
        public Action OnEndTime;

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI textTime;

        [Header("Config")]
        [SerializeField] private float timePlay = 50f;

        private float timeRemaining;
        private bool isPlaying;

        private void Start()
        {
            OnEndTime = null; // Clear Callback

            timeRemaining = timePlay;
            SetPlaying(false);
            UpdateTimeUI();
        }

        private void Update()
        {
            if (!isPlaying)
                return;

            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                SetPlaying(false);
                timeRemaining = 0;
                OnEndTime?.Invoke();
            }
            UpdateTimeUI();
        }

        private void UpdateTimeUI()
        {
            textTime.text = TextUtils.TimeToMMSS(timeRemaining);
        }

        public void SetPlaying(bool isPlaying)
        {
            this.isPlaying = isPlaying;
        }

        public void AddTime()
        {

        }
    }
}
