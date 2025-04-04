using Item;
using Map;
using UnityEngine;

namespace Indicator {
    public class GoalIndicator : MonoBehaviour {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject indicatorPrefab;
    
        private OffScreenIndicator _offScreenIndicator;
    
        private void Awake() {
            var indicator = Instantiate(indicatorPrefab, transform);
            _offScreenIndicator = new OffScreenIndicator(null, indicator);
        }
    
        private void Update() {
            _offScreenIndicator.UpdateIndicator();
        }
        
        public void SetTarget(MapData mapData) {
            target = mapData.itemUnloader.transform;
            _offScreenIndicator.SetTarget(target);
        }
        
        public void EnableIndicator() {
            _offScreenIndicator.SetTarget(target);
        }
        
        public void DisableIndicator() {
            _offScreenIndicator.SetTarget(null);
        }
    }
}