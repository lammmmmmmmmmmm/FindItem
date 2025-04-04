using UnityEngine;

namespace _Global {
    public class VibrationWrapper : MonoBehaviour {
        [SerializeField] private bool isEnabled = true;
        
        private void Start() {
            isEnabled = DataManager.Instance.GetVibration();
            
            if (isEnabled) {
                Vibration.Init();
            }
        }

        public void Vibrate() {
            if (!isEnabled) return;
            Vibration.Vibrate();
        }
    }
}