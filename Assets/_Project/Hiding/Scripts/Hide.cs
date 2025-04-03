using DG.Tweening;
using UnityEngine;

namespace Hiding {
    public abstract class Hide : MonoBehaviour {
        [SerializeField] private LayerMask humanHidingLayer;
        [SerializeField] private LayerMask humanLayer;
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
            EnableMovement(0.5f);
            
            _hideTween = DOVirtual.DelayedCall(HIDE_DURATION, () => {
                //TODO: find a better way to hide human from monster
                int layerIndex = Mathf.RoundToInt(Mathf.Log(humanHidingLayer.value, 2));
                gameObject.layer = layerIndex;

                _isHidden = true;
            }).SetLink(gameObject);
        }

        private void StopHiding() {
            _hideTween = DOVirtual.DelayedCall(HIDE_DURATION, () => {
                int layerIndex = Mathf.RoundToInt(Mathf.Log(humanLayer.value, 2));
                gameObject.layer = layerIndex;

                EnableMovement(1f);

                _isHidden = false;
            }).SetLink(gameObject);
        }

        protected abstract void EnableMovement(float speedMultiplier);
        
        public void CancelHiding() {
            EnableMovement(1f);
            
            if (_hideTween == null) return;
            _hideTween.Kill();
            _hideTween = null;
        }
    }
}