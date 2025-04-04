using System;
using _Global.ExtensionMethods;
using UnityEngine;
using UnityEngine.Events;

namespace Item {
    public class ItemCarrier : MonoBehaviour {
        [SerializeField] private LayerMask itemLayer;
        public bool IsCarrying { get; private set; }
        public Action OnItemPickedUp;
        public UnityEvent onItemPickedUpEvent;

        private Rigidbody2D _rb;
        private GameObject _carriedItem;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (!IsCarrying && itemLayer.Contains(other.gameObject.layer)) {
                IsCarrying = true;
                
                _carriedItem = other.gameObject;
                _carriedItem.SetActive(false);

                _rb.excludeLayers = _rb.excludeLayers.AddLayerMasks(itemLayer);

                onItemPickedUpEvent.Invoke();
                OnItemPickedUp?.Invoke();
            }
        }

        public void ResetCarrying() {
            _rb.excludeLayers = _rb.excludeLayers.RemoveLayerMasks(itemLayer);
            IsCarrying = false;
            
            if (_carriedItem) {
                Destroy(_carriedItem);
                _carriedItem = null;
            }
        }
        
        public void DropItem() {
            if (_carriedItem) {
                _carriedItem.transform.position = transform.position;
                _carriedItem.SetActive(true);
                _carriedItem = null;
            }
            
            ResetCarrying();
        }
    }
}