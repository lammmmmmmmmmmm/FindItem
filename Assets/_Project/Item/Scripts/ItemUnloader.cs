using _Global.ExtensionMethods;
using Survivor.Patterns;
using UnityEngine;
using UnityEngine.Events;

namespace Item {
    public class ItemUnloader : Singleton<ItemUnloader> {
        [SerializeField] private LayerMask humanLayerMask;
        [SerializeField] private GameObject[] spots;
        
        public UnityEvent onItemUnloadedEvent;

        private bool[] _isTaken;

        private void Start() {
            _isTaken = new bool[spots.Length];
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (humanLayerMask.Contains(other.gameObject.layer)) {
                ItemCarrier itemCarrier = other.GetComponent<ItemCarrier>();
                
                if (!itemCarrier.IsCarrying) return;
                
                itemCarrier.ResetCarrying();

                AddItemToFreeSpot();
            }
        }

        private void AddItemToFreeSpot() {
            for (int i = 0; i < _isTaken.Length; i++) {
                if (_isTaken[i]) continue;
                    
                _isTaken[i] = true;
                spots[i].SetActive(true);
                
                //TODO: add event when player unloads item
                onItemUnloadedEvent.Invoke();
                
                return;
            }
        }
    }
}