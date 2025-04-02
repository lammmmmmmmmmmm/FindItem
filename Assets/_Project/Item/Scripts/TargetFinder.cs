using System;
using System.Collections;
using UnityEngine;

namespace Item {
    public class TargetFinder : MonoBehaviour {
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField] private float radius = 10f;
        
        private readonly WaitForSeconds _waitForSeconds = new(1f);
        public Action<GameObject> OnTargetFound;
        
        public Transform Target { get; private set; }
        
        private void Start() {
            StartCoroutine(FindTarget());
        }
        
        private IEnumerator FindTarget() {
            while (true) {
                var targetCollider = Physics2D.OverlapCircle(transform.position, radius, targetLayerMask);
                
                if (targetCollider) {
                    Target = targetCollider.transform;
                    OnTargetFound?.Invoke(targetCollider.gameObject);
                } else {
                    Target = null;
                }

                yield return _waitForSeconds;
            }
        }
    }
}