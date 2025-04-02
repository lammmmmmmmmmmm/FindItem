using System;
using System.Collections;
using UnityEngine;

namespace _Global {
    public class TargetFinder : MonoBehaviour {
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField] private float radius = 10f;
        
        [Header("Debug")]
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private Color radiusColor = Color.red;
        [SerializeField] private Color lineColor = Color.green;
        
        private readonly WaitForSeconds _waitForSeconds = new(1f);
        public Action<GameObject> OnNewTargetFound;
        public Action<GameObject> OnTargetInRange;
        public Action OnTargetLost;
        
        public Transform Target { get; private set; }
        
        private void Start() {
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
                    OnTargetInRange?.Invoke(Target.gameObject);
                }
                
                if (!targetCollider && Target) {
                    Target = null;
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