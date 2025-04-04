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
        
        public UnityEvent<int> onItemUnloadedEvent;
        public UnityEvent onAllItemsUnloadedEvent;
        public UnityEvent onItemUnloadedEventWithPlayer;

        private void OnTriggerEnter2D(Collider2D other) {
            if (humanLayerMask.Contains(other.gameObject.layer)) {
                ItemCarrier itemCarrier = other.GetComponent<ItemCarrier>();
                
                if (!itemCarrier.IsCarrying) return;
                
                itemCarrier.ResetCarrying();

                AddItemToFreeSpot();
                
                if (other.CompareTag("Player")) {
                    onItemUnloadedEventWithPlayer.Invoke();
                }
            }
        }

        private void AddItemToFreeSpot() {
            if (_currentSpotIndex >= spots.Length) return;
            
            spots[_currentSpotIndex].SetActive(true);
            _currentSpotIndex++;
            
            if (_currentSpotIndex >= spots.Length) {
                onAllItemsUnloadedEvent.Invoke();
            }
            
            //TODO: add event when player unloads item
            onItemUnloadedEvent.Invoke(_currentSpotIndex);
        }
    }
}