using _Global;
using Pathfinding;
using UnityEngine;

namespace HumanBot.States {
    public class GoToTargetState : IState {
        private GameObject _currentTarget;
        private readonly IAstarAI _ai;
        
        public GoToTargetState(IAstarAI ai) {
            _ai = ai;
        }
        
        public void OnEnter() {
            _ai.destination = _currentTarget.transform.position;
            _ai.SearchPath();
        }

        public void Tick() {
            if (!_ai.pathPending && (_ai.reachedEndOfPath || !_ai.hasPath)) {
                _ai.destination = _currentTarget.transform.position;
                _ai.SearchPath();
            }
            
            if (_ai.reachedDestination) {
                _currentTarget = null;
            }
        }

        public void OnExit() {
            _currentTarget = null;
        }
        
        public bool HasTarget() {
            return _currentTarget;
        }
        
        public void SetTarget(GameObject target) {
            if (!target || target == _currentTarget) return;
            
            _currentTarget = target;
        }
    }
}