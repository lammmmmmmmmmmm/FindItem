using _Global;
using Pathfinding;
using UnityEngine;

namespace HumanBot.States {
    public class GoToTargetState : IState {
        private GameObject _currentTarget;
        private readonly IAstarAI _ai;
        private bool _isInState;
        
        public GoToTargetState(IAstarAI ai) {
            _ai = ai;
        }
        
        public void OnEnter() {
            _isInState = true;
        }

        public void Tick() {
        }

        public void OnExit() {
            _isInState = false;
            _currentTarget = null;
        }
        
        public void SetTarget(GameObject target) {
            if (!_isInState) return;
            if (!target || target == _currentTarget) return;
            
            _currentTarget = target;
            _ai.destination = _currentTarget.transform.position;
            _ai.SearchPath();
        }
    }
}