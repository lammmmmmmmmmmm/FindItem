using DG.Tweening;
using UnityEngine;

namespace Indicator {
    public class ItemIndicator : MonoBehaviour {
        [SerializeField] private LayerMask itemLayerMask;
        [SerializeField] private float detectionRadius = 10f;
        [SerializeField] private int numberOfItems = 5;
        [SerializeField] private float delayTimeInSeconds = 5f;
        [SerializeField] private GameObject indicatorPrefab;
        
        private Collider2D[] _items;
        private OffScreenIndicator[] _offScreenIndicators;
        
        private Tween _delayFindItems;
        
        private void Start() {
            _items = new Collider2D[numberOfItems];
            _offScreenIndicators = new OffScreenIndicator[numberOfItems];
            
            for (int i = 0; i < numberOfItems; i++) {
                _offScreenIndicators[i] = new OffScreenIndicator(null, Instantiate(indicatorPrefab, transform));
            }
        }

        private void Update() {
            foreach (var indicator in _offScreenIndicators) {
                indicator?.UpdateIndicator();
            }
        }
        
        public void DelayFindItems() {
            _delayFindItems = DOVirtual.DelayedCall(delayTimeInSeconds, FindItems);
        }
        
        public void CancelDelayFindItems() {
            if (_delayFindItems == null) return;
            
            _delayFindItems.Kill();
            _delayFindItems = null;
        }

        private void FindItems() {
            var length = Physics2D.OverlapCircleNonAlloc(transform.position, detectionRadius, _items, itemLayerMask);

            for (int i = 0; i < length; i++) {
                _offScreenIndicators[i].SetTarget(_items[i].transform);
            }
        }
    }
}