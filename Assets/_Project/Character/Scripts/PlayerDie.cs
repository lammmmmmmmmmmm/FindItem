using Bot;
using DG.Tweening;
using Item;
using UnityEngine;
using UnityEngine.Events;

namespace Character {
    public class PlayerDie : MonoBehaviour, IDie {
        public UnityEvent onPlayerDieEvent;
        private PlayerMovement _playerMovement;
        private ItemCarrier _itemCarrier;

        private void Awake() {
            _playerMovement = GetComponent<PlayerMovement>();
            _itemCarrier = GetComponent<ItemCarrier>();
        }

        public void Die(Vector3 position) {
            if (_itemCarrier.IsCarrying) {
                _itemCarrier.DropItem();
            }
            
            _playerMovement.CurrentMoveSpeed = 0;
            transform.position = position;

            DOVirtual.DelayedCall(0.5f, () => {
                GameManager.Instance.OnKilled();
                onPlayerDieEvent.Invoke();
                gameObject.SetActive(false);
            });
        }
    }
}