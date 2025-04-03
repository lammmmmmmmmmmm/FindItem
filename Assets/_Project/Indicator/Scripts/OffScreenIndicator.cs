using UnityEngine;

namespace Indicator {
    public class OffScreenIndicator {
        private Transform _target;
        private readonly GameObject _indicator;

        private const float INDICATOR_WIDTH = 2f;
        private const float INDICATOR_HEIGHT = 2f;

        private readonly Camera _mainCamera;
    
        public OffScreenIndicator(Transform target, GameObject indicator) {
            _target = target;
            _indicator = indicator;
            _mainCamera = Camera.main;
        }

        public void UpdateIndicator() {
            if (!_target) {
                _indicator.SetActive(false);
                return;
            }
            
            var targetViewportPos = _mainCamera.WorldToViewportPoint(_target.position);
            var isOffScreen = targetViewportPos.x < 0 || targetViewportPos.x > 1 ||
                              targetViewportPos.y < 0 || targetViewportPos.y > 1;

            if (isOffScreen) {
                _indicator.SetActive(true);

                var one = _mainCamera.WorldToViewportPoint(new Vector3(INDICATOR_WIDTH, INDICATOR_HEIGHT, 0));
                var two = _mainCamera.WorldToViewportPoint(Vector3.zero);

                var indicatorSizeInViewPort = one - two;
                targetViewportPos.x = Mathf.Clamp(targetViewportPos.x, indicatorSizeInViewPort.x, 1 - indicatorSizeInViewPort.x);
                targetViewportPos.y = Mathf.Clamp(targetViewportPos.y, indicatorSizeInViewPort.y, 1 - indicatorSizeInViewPort.y);
            
                var worldPos = _mainCamera.ViewportToWorldPoint(targetViewportPos);
                worldPos.z = 0;
            
                _indicator.transform.position = worldPos;
            
                var dir = _target.position - _indicator.transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                _indicator.transform.rotation = Quaternion.Euler(0, 0, angle);
            } else {
                _indicator.SetActive(false);
            }
        }
        
        public void SetTarget(Transform target) {
            _target = target;
        }
    }
}