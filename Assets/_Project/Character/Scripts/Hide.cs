using DG.Tweening;
using UnityEngine;

namespace Character {
    public abstract class Hide : MonoBehaviour {
        private Tween _hideTween;
        private bool _isHidden;
        
        private const float HIDE_DURATION = 0.5f;

        public void ToggleHiding() {
            bool hidingInProgress = _hideTween != null && _hideTween.IsActive();
            if (hidingInProgress) return;
            
            if (_isHidden) {
                StopHiding();
            } else {
                StartHiding();
            }
        }

        private void StartHiding() {
            DisableMovement();

            _hideTween = DOVirtual.DelayedCall(HIDE_DURATION, () => {
                //TODO: hide player from monster

                EnableMovement(0.5f);

                _isHidden = true;
            }).SetLink(gameObject);
        }

        private void StopHiding() {
            _hideTween = DOVirtual.DelayedCall(HIDE_DURATION, () => {
                //TODO: reveal player to monster

                EnableMovement(1f);

                _isHidden = false;
            }).SetLink(gameObject);
        }
        
        protected abstract void DisableMovement();
        protected abstract void EnableMovement(float speedMultiplier);
        
        public void CancelHiding() {
            if (_hideTween == null) return;
            _hideTween.Kill();
            _hideTween = null;
        }
    }
}