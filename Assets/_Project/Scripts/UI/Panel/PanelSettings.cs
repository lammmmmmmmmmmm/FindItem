using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI {
    public class PanelSettings : PanelBase {
        [SerializeField] private Image vibrationImage;
        [SerializeField] private Image musicImage;
        [SerializeField] private Image sfxImage;
        
        [SerializeField] private InputField nameInputField;
        
        public override void Open(PanelData panelData) {
            base.Open(panelData);
            
            nameInputField.text = DataManager.Instance.GetPlayerName();
            
            //TODO: update UI with current settings
        }

        public void ToggleVibration() {
            bool isEnabled = DataManager.Instance.GetVibration();
            DataManager.Instance.SetVibration(!isEnabled);
            
            //TODO: update UI
        }

        public void ToggleMusic() {
            bool isEnabled = DataManager.Instance.GetMusic();
            DataManager.Instance.SetMusic(!isEnabled);
            
            //TODO: update UI
        }

        public void ToggleSfx() {
            bool isEnable = DataManager.Instance.GetSound();
            DataManager.Instance.SetSound(!isEnable);
            
            //TODO: update UI
        }

        public void UpdateName(string playerName) {
            if (string.IsNullOrEmpty(playerName)) return;
            
            DataManager.Instance.SetPlayerName(playerName);
        }
    }
}