using DG.Tweening;
using UnityEngine;

namespace Character {
    public class Hide : MonoBehaviour {
        private PlayerMovement _playerMovement;
        private Tween _hideTween;
        private bool _isHidden;
        
        private const float HIDE_DURATION = 0.5f;

        private void Awake() {
            _playerMovement = GetComponent<PlayerMovement>();
            GetComponent<Collider2D>();
        }

        public void ToggleHiding() {
            if (_hideTween != null && _hideTween.IsActive()) {
                Debug.Log("Hiding in progress, cannot toggle.");
                return;
            }
            
            if (_isHidden) {
                StopHiding();
            } else {
                StartHiding();
            }
        }

        private void StartHiding() {
            _playerMovement.enabled = false;

            _hideTween = DOVirtual.DelayedCall(HIDE_DURATION, () => {
                //TODO: hide player from monster

                _playerMovement.enabled = true;
                _playerMovement.SetMoveSpeed(2f);

                _isHidden = true;
            }).SetLink(gameObject);
        }

        private void StopHiding() {
            _hideTween = DOVirtual.DelayedCall(HIDE_DURATION, () => {
                //TODO: reveal player to monster

                _playerMovement.enabled = true;
                _playerMovement.SetMoveSpeed(10f);

                _isHidden = false;
            }).SetLink(gameObject);
        }
        
        public void CancelHiding() {
            if (_hideTween == null) return;
            _hideTween.Kill();
            _hideTween = null;
        }
    }
}