using System;
using System.Collections;
using UnityEngine;

namespace _Global {
    public class TargetFinder : MonoBehaviour {
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField] private float radius = 10f;
        [SerializeField] private float targetDetectionInterval = 1f;
        
        [Header("Debug")]
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private Color radiusColor = Color.red;
        [SerializeField] private Color lineColor = Color.green;

        private WaitForSeconds _waitForSeconds;
        public Action<GameObject> OnNewTargetFound;
        public Action<GameObject> OnTargetInRange;
        public Action OnTargetLost; 
        
        public Transform Target { get; private set; }
        
        private bool _isTargetInRange;
        
        private void Start() {
            _waitForSeconds = new WaitForSeconds(targetDetectionInterval);
            
            StartCoroutine(FindTarget());
        }
        
        public void SetRadius(float newRadius) {
            radius = newRadius;
        }
        
        private IEnumerator FindTarget() {
            while (true) {
                var targetCollider = Physics2D.OverlapCircle(transform.position, radius, targetLayerMask);
                
                if (targetCollider && targetCollider.gameObject.transform != Target) {
                    Target = targetCollider.transform;
                    OnNewTargetFound?.Invoke(targetCollider.gameObject);
                }
                
                if (Target) {
                    _isTargetInRange = true;
                    OnTargetInRange?.Invoke(Target.gameObject);
                }
                
                // must use _isTargetInRange instead of Target because Target could be destroyed before this check
                // making this event not fired
                if (!targetCollider && _isTargetInRange) {
                    Target = null;
                    _isTargetInRange = false;
                    OnTargetLost?.Invoke();
                }

                yield return _waitForSeconds;
            }
        }

        private void OnDrawGizmos() {
            if (!showGizmos) return;
            
            Gizmos.color = radiusColor;
            Gizmos.DrawWireSphere(transform.position, radius);
            
            if (Target) {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(transform.position, Target.position);
            }
        }
    }
}