using Bot;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Character {
    public class PlayerDie : MonoBehaviour, IDie {
        public UnityEvent onPlayerDieEvent;
        private PlayerMovement _playerMovement;

        private void Awake() {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public void Die(Vector3 position) {
            _playerMovement.CurrentMoveSpeed = 0;
            transform.position = position;

            DOVirtual.DelayedCall(0.5f, () => {
                onPlayerDieEvent.Invoke();
                gameObject.SetActive(false);
            });
        }
    }
}