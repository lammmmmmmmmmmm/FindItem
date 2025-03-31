using System;
using UnityEngine;

namespace Item {
    public class ItemCarrier : MonoBehaviour {
        [SerializeField] private LayerMask itemLayer;
        public bool IsCarrying { get; private set;}
        
        private Rigidbody2D _rb;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (!IsCarrying && itemLayer.Contains(other.gameObject.layer)) {
                IsCarrying = true;
                
                _rb.excludeLayers = _rb.excludeLayers.AddLayerMasks(itemLayer);
                Destroy(other.gameObject);
            }
        }

        public void ResetCarrying() {
            _rb.excludeLayers = _rb.excludeLayers.RemoveLayerMasks(itemLayer);
            IsCarrying = false;
        }
    }
}