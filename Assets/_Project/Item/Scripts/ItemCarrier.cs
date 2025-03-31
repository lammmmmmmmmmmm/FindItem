using UnityEngine;

namespace Item {
    public class ItemCarrier : MonoBehaviour {
        [SerializeField] private LayerMask itemLayer;
        public bool IsCarrying { get; private set;}

        private void OnCollisionEnter2D(Collision2D other) {
            if (!IsCarrying && itemLayer.Contains(other.gameObject.layer)) {
                IsCarrying = true;
                Destroy(other.gameObject);
            }
        }

        public void ResetCarrying() {
            IsCarrying = false;
        }
    }
}