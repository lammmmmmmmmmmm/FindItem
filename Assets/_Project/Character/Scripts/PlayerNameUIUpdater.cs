using TMPro;
using UnityEngine;

namespace Character {
    public class PlayerNameUIUpdater : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI playerNameText;

        private void Start() {
            SetPlayerName();
        }

        public void SetPlayerName() {
            var playerName = DataManager.Instance.PlayerData.name;
            playerNameText.text = string.IsNullOrEmpty(playerName) ? "Player" : playerName;
        }
    }
}