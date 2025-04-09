using _Global.ExtensionMethods;
using Survivor.Patterns;
using UnityEngine;
using UnityEngine.Events;

namespace Item {
    public class ItemUnloader : Singleton<ItemUnloader> {
        [SerializeField] private LayerMask humanLayerMask;
        [SerializeField] private GameObject[] spots;
        private int _currentSpotIndex;
        
        public int TotalItems => spots.Length;
        
        public UnityEvent<ItemUnloadPayload> onItemUnloadedEvent;
        public UnityEvent<ItemUnloadPayload> onAllItemsUnloadedEvent;
        public UnityEvent<ItemUnloadPayload> onItemUnloadedEventWithPlayer;

        private void OnTriggerEnter2D(Collider2D other) {
            if (humanLayerMask.Contains(other.gameObject.layer)) {
                ItemCarrier itemCarrier = other.GetComponent<ItemCarrier>();
                
                if (!itemCarrier.IsCarrying) return;
                
                itemCarrier.ResetCarrying();

                AddItemToFreeSpot();
                
                if (other.CompareTag("Player")) {
                    var payload = new ItemUnloadPayload {
                        CurrentSpotIndex = _currentSpotIndex,
                        UnloadPosition = transform.position
                    };
                    onItemUnloadedEventWithPlayer.Invoke(payload);
                }
            }
        }

        private void AddItemToFreeSpot() {
            if (_currentSpotIndex >= spots.Length) return;
            
            spots[_currentSpotIndex].SetActive(true);
            _currentSpotIndex++;
            
            var payload = new ItemUnloadPayload {
                CurrentSpotIndex = _currentSpotIndex,
                UnloadPosition = transform.position
            };
            
            if (_currentSpotIndex >= spots.Length) {
                onAllItemsUnloadedEvent.Invoke(payload);
            }
            
            onItemUnloadedEvent.Invoke(payload);
        }
    }
}