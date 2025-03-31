using Character;
using UnityEngine;

namespace Character {
    public class Hide : MonoBehaviour {
        private PlayerController _playerController;

        private void Awake() {
            _playerController = GetComponent<PlayerController>();
        }

        public void Test() {
            _playerController.enabled = false;
        }
    }
}